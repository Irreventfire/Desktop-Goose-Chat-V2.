using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GooseShared;
using SamEngine;

namespace DefaultMod
{
    /// <summary>
    /// A task that opens and manages the chatbot window.
    /// The goose can interact with this window by dragging it around.
    /// </summary>
    public class ChatbotTask : GooseTaskInfo
    {
        // Static reference to the chatbot window so it persists across task instances
        private static ChatbotWindow chatWindow = null;

        // Windows API for window manipulation
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOACTIVATE = 0x0010;

        public ChatbotTask()
        {
            // This task can be picked randomly by the goose
            canBePickedRandomly = true;

            // Task metadata
            shortName = "Chat with user";
            description = "Opens a chatbot window for the user to interact with the goose.";
            taskID = "ChatbotTask";
        }

        /// <summary>
        /// Task data - stores when the task started and the window state.
        /// </summary>
        public class ChatbotTaskData : GooseTaskData
        {
            public float timeStarted;
            public bool windowOpened;
            public bool isDragging;
            public float dragStartTime;
            public Vector2 dragOffset;
            public Vector2 initialWindowPos;
            public float dragPhase; // 0 = approach, 1 = grab, 2 = drag, 3 = release
            public bool taskCompleted; // Flag to indicate task has finished
        }

        public override GooseTaskData GetNewTaskData(GooseEntity goose)
        {
            ChatbotTaskData taskData = new ChatbotTaskData
            {
                timeStarted = Time.time,
                windowOpened = false,
                isDragging = false,
                dragStartTime = 0,
                dragOffset = Vector2.zero,
                initialWindowPos = Vector2.zero,
                dragPhase = 0,
                taskCompleted = false
            };
            return taskData;
        }

        public override void RunTask(GooseEntity goose)
        {
            ChatbotTaskData data = (ChatbotTaskData)goose.currentTaskData;

            // If task is already completed, end immediately to prevent interference
            if (data.taskCompleted)
            {
                API.Goose.setTaskRoaming(goose);
                return;
            }

            // Open the window if not already opened
            if (!data.windowOpened)
            {
                OpenChatWindow();
                data.windowOpened = true;
            }

            // Make sure window exists and is valid
            if (chatWindow == null || chatWindow.IsDisposed)
            {
                API.Goose.setTaskRoaming(goose);
                return;
            }

            // If the window is hidden, show it
            if (!chatWindow.Visible)
            {
                chatWindow.Show();
            }

            float timeSinceStart = Time.time - data.timeStarted;

            // Phase 0: Initial positioning and approach (0-0.5 seconds)
            if (timeSinceStart < 0.5f)
            {
                // Position window near the goose when first opened
                int windowX = (int)goose.position.x + 50;
                int windowY = (int)goose.position.y - 100;

                // Make sure window stays on screen with proper bounds
                windowX = Math.Max(0, Math.Min(windowX, Screen.PrimaryScreen.WorkingArea.Width - chatWindow.Width));
                windowY = Math.Max(0, Math.Min(windowY, Screen.PrimaryScreen.WorkingArea.Height - chatWindow.Height));

                chatWindow.Location = new System.Drawing.Point(windowX, windowY);
                data.initialWindowPos = new Vector2(windowX, windowY);
                
                // Make goose approach the window
                goose.targetPos = new Vector2(windowX + chatWindow.Width / 2, windowY + chatWindow.Height);
                API.Goose.setSpeed(goose, GooseEntity.SpeedTiers.Run);
            }
            // Phase 1: Grab the window (0.5-1.5 seconds) - goose reaches window and "grabs" it
            else if (timeSinceStart >= 0.5f && timeSinceStart < 1.5f)
            {
                if (!data.isDragging)
                {
                    data.isDragging = true;
                    data.dragStartTime = Time.time;
                    
                    // Calculate offset from goose to window top center
                    Vector2 windowTopCenter = new Vector2(
                        chatWindow.Location.X + chatWindow.Width / 2,
                        chatWindow.Location.Y
                    );
                    data.dragOffset = windowTopCenter - goose.position;
                }

                // Goose holds position at the top of the window
                goose.targetPos = new Vector2(
                    chatWindow.Location.X + chatWindow.Width / 2,
                    chatWindow.Location.Y
                );
                API.Goose.setSpeed(goose, GooseEntity.SpeedTiers.Walk);
                goose.extendingNeck = true; // Make goose extend neck as if holding
            }
            // Phase 2: Drag the window (1.5-4.5 seconds) - goose drags window across screen
            else if (timeSinceStart >= 1.5f && timeSinceStart < 4.5f)
            {
                // Calculate target position for dragging (pull window partially across screen)
                float dragProgress = (timeSinceStart - 1.5f) / 3.0f; // 0 to 1 over 3 seconds
                
                // Target: drag window from current position toward center or to the side
                int screenCenterX = Screen.PrimaryScreen.WorkingArea.Width / 2;
                int targetX = (int)SamMath.Lerp(data.initialWindowPos.x, screenCenterX - chatWindow.Width / 2, dragProgress);
                
                // Add some vertical movement for natural dragging
                int targetY = (int)(data.initialWindowPos.y + Math.Sin(dragProgress * Math.PI) * 50);
                
                // Keep on screen - ensure window stays fully visible with proper margin
                targetX = Math.Max(10, Math.Min(targetX, Screen.PrimaryScreen.WorkingArea.Width - chatWindow.Width - 10));
                targetY = Math.Max(10, Math.Min(targetY, Screen.PrimaryScreen.WorkingArea.Height - chatWindow.Height - 10));

                // Move the window
                SetWindowPos(chatWindow.GetWindowHandle(), IntPtr.Zero, targetX, targetY, 0, 0, 
                    SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE);

                // Make goose follow the top of the window
                goose.targetPos = new Vector2(targetX + chatWindow.Width / 2, targetY);
                API.Goose.setSpeed(goose, GooseEntity.SpeedTiers.Run);
                goose.extendingNeck = true;
            }
            // Phase 3: Release and leave (after 4.5 seconds)
            else
            {
                if (data.isDragging)
                {
                    data.isDragging = false;
                    
                    // Activate the window so user can interact with it
                    if (chatWindow != null && !chatWindow.IsDisposed && chatWindow.Visible)
                    {
                        chatWindow.Activate();
                        // Bring window to front and ensure it's focusable
                        chatWindow.BringToFront();
                        chatWindow.TopMost = true;  // Temporarily set to topmost
                        chatWindow.TopMost = false; // Then remove it to avoid always-on-top behavior
                    }
                    
                    // Mark task as completed to prevent further execution
                    data.taskCompleted = true;
                }
                
                // Goose lets go and returns to normal behavior
                API.Goose.setTaskRoaming(goose);
            }
        }

        /// <summary>
        /// Creates or shows the chatbot window.
        /// </summary>
        private void OpenChatWindow()
        {
            // Create the window if it doesn't exist
            if (chatWindow == null || chatWindow.IsDisposed)
            {
                chatWindow = new ChatbotWindow();
            }

            // Show the window (or bring it to front if already visible)
            if (!chatWindow.Visible)
            {
                chatWindow.Show();
            }
            else
            {
                chatWindow.BringToFront();
                chatWindow.Focus();
            }
        }

        /// <summary>
        /// Static method to check if chat window exists and is visible.
        /// Can be used by other parts of the mod.
        /// </summary>
        public static bool IsChatWindowOpen()
        {
            return chatWindow != null && !chatWindow.IsDisposed && chatWindow.Visible;
        }

        /// <summary>
        /// Static method to manually close the chat window.
        /// </summary>
        public static void CloseChatWindow()
        {
            if (chatWindow != null && !chatWindow.IsDisposed)
            {
                chatWindow.Hide();
            }
        }
    }
}
