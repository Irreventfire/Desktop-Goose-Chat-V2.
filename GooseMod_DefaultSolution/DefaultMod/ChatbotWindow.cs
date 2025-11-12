using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DefaultMod
{
    /// <summary>
    /// A chatbot window that can be dragged by the goose.
    /// This window provides a simple chat interface with the Desktop Goose.
    /// </summary>
    public class ChatbotWindow : Form
    {
        private TextBox chatDisplay;
        private TextBox userInput;
        private Button sendButton;
        private List<ChatMessage> conversationHistory;
        private IChatResponseProvider responseProvider;

        public ChatbotWindow()
        {
            InitializeComponents();
            conversationHistory = new List<ChatMessage>();
            // Use the basic response provider by default
            // You can easily swap this out with an AI-powered provider
            responseProvider = new BasicChatResponseProvider();
        }

        /// <summary>
        /// Set a custom response provider (e.g., AI-powered chatbot).
        /// </summary>
        public void SetResponseProvider(IChatResponseProvider provider)
        {
            responseProvider = provider;
        }

        /// <summary>
        /// Get the window handle for goose interaction.
        /// This allows the goose to drag the window around.
        /// </summary>
        public IntPtr GetWindowHandle()
        {
            return this.Handle;
        }

        private void InitializeComponents()
        {
            // Window properties
            this.Text = "Chat with Goose 🦢";
            this.Size = new Size(400, 500);
            this.MinimumSize = new Size(300, 400);
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Chat display (readonly multiline textbox)
            chatDisplay = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10f),
                BackColor = Color.FromArgb(240, 240, 240),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Input panel (contains textbox and button)
            Panel inputPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                Padding = new Padding(10)
            };

            // User input textbox
            userInput = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10f),
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = 0
            };
            userInput.KeyDown += UserInput_KeyDown;

            // Send button
            sendButton = new Button
            {
                Text = "Send",
                Dock = DockStyle.Right,
                Width = 80,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(100, 150, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TabIndex = 1
            };
            sendButton.FlatAppearance.BorderSize = 0;
            sendButton.Click += SendButton_Click;

            // Add controls to input panel
            inputPanel.Controls.Add(userInput);
            inputPanel.Controls.Add(sendButton);

            // Add controls to form
            this.Controls.Add(chatDisplay);
            this.Controls.Add(inputPanel);

            // Set up event handlers for focus management
            this.Shown += ChatbotWindow_Shown;
            this.Activated += ChatbotWindow_Activated;

            // Initial greeting
            AddMessage("Goose", "Honk! Hello! I'm your Desktop Goose! Type a message and press Send or Enter to chat with me! 🦢");
        }

        private void ChatbotWindow_Shown(object sender, EventArgs e)
        {
            // Set focus to input field when window is first shown
            userInput.Focus();
        }

        private void ChatbotWindow_Activated(object sender, EventArgs e)
        {
            // Set focus to input field when window is activated
            userInput.Focus();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true; // Prevent the "ding" sound
                SendMessage();
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            string message = userInput.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            // Add user message to display and history
            AddMessage("You", message);
            conversationHistory.Add(new ChatMessage("You", message));

            // Clear input
            userInput.Clear();
            
            // Keep focus on input field for continuous chatting
            userInput.Focus();

            // Get response from the provider
            string response = responseProvider.GetResponse(message, conversationHistory);

            // Add goose response to display and history
            AddMessage("Goose", response);
            conversationHistory.Add(new ChatMessage("Goose", response));
        }

        private void AddMessage(string sender, string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm");
            string formattedMessage = $"[{timestamp}] {sender}: {message}\r\n\r\n";
            chatDisplay.AppendText(formattedMessage);

            // Auto-scroll to bottom
            chatDisplay.SelectionStart = chatDisplay.Text.Length;
            chatDisplay.ScrollToCaret();
        }

        /// <summary>
        /// Override to prevent the window from being disposed when closed.
        /// This allows the goose to reopen it later.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
