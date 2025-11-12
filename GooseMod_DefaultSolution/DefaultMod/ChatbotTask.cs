using System;
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
        }

        public override GooseTaskData GetNewTaskData(GooseEntity goose)
        {
            ChatbotTaskData taskData = new ChatbotTaskData
            {
                timeStarted = Time.time,
                windowOpened = false
            };
            return taskData;
        }

        public override void RunTask(GooseEntity goose)
        {
            ChatbotTaskData data = (ChatbotTaskData)goose.currentTaskData;

            // Open the window if not already opened
            if (!data.windowOpened)
            {
                OpenChatWindow();
                data.windowOpened = true;
            }

            // Keep the window visible and positioned near the goose for a while
            if (chatWindow != null && !chatWindow.IsDisposed)
            {
                // If the window is hidden, show it
                if (!chatWindow.Visible)
                {
                    chatWindow.Show();
                }

                // Position the window near the goose when first opened
                if (Time.time - data.timeStarted < 0.5f)
                {
                    // Position window slightly offset from goose position
                    int windowX = (int)goose.position.x + 50;
                    int windowY = (int)goose.position.y - 100;

                    // Make sure window stays on screen
                    windowX = Math.Max(0, Math.Min(windowX, Screen.PrimaryScreen.WorkingArea.Width - chatWindow.Width));
                    windowY = Math.Max(0, Math.Min(windowY, Screen.PrimaryScreen.WorkingArea.Height - chatWindow.Height));

                    chatWindow.Location = new System.Drawing.Point(windowX, windowY);
                }
            }

            // After a short time, return to normal behavior
            // The window stays open for the user to interact with
            if (Time.time - data.timeStarted > 2f)
            {
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
