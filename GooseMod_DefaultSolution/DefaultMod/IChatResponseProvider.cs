using System;
using System.Collections.Generic;

namespace DefaultMod
{
    /// <summary>
    /// Interface for chat response providers.
    /// Implement this interface to create AI-powered or custom chatbots.
    /// </summary>
    public interface IChatResponseProvider
    {
        /// <summary>
        /// Get a response for the given user message.
        /// </summary>
        /// <param name="userMessage">The message from the user</param>
        /// <param name="conversationHistory">Previous messages in the conversation</param>
        /// <returns>The bot's response</returns>
        string GetResponse(string userMessage, List<ChatMessage> conversationHistory);
    }

    /// <summary>
    /// Represents a single message in the conversation.
    /// </summary>
    public class ChatMessage
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public ChatMessage(string sender, string message)
        {
            Sender = sender;
            Message = message;
            Timestamp = DateTime.Now;
        }
    }
}
