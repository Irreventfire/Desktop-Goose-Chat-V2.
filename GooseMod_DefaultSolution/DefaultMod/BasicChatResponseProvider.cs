using System;
using System.Collections.Generic;
using System.Linq;

namespace DefaultMod
{
    /// <summary>
    /// Basic hardcoded chatbot response provider.
    /// Replace this with an AI-powered provider for more advanced responses.
    /// </summary>
    public class BasicChatResponseProvider : IChatResponseProvider
    {
        private Random random = new Random();

        // Dictionary of keywords to responses
        private Dictionary<string, string[]> responses = new Dictionary<string, string[]>
        {
            { "hello", new[] { "Honk! Hello there!", "Hi! 🦢", "Greetings, human!" } },
            { "hi", new[] { "Honk! Hello there!", "Hi! 🦢", "Greetings, human!" } },
            { "hey", new[] { "Honk! Hello there!", "Hi! 🦢", "Greetings, human!" } },
            { "how are you", new[] { "I'm doing great! Just honking around! 🦢", "Feeling goose-tastic!", "Pretty good, thanks for asking!" } },
            { "what are you", new[] { "I'm a desktop goose! Honk!", "A helpful (and sometimes mischievous) goose!", "Your friendly neighborhood goose! 🦢" } },
            { "who are you", new[] { "I'm a desktop goose! Honk!", "A helpful (and sometimes mischievous) goose!", "Your friendly neighborhood goose! 🦢" } },
            { "help", new[] { "Just type anything and I'll respond! Try asking me how I am!", "I can chat with you! Ask me questions!", "I'm here to keep you company! 🦢" } },
            { "bye", new[] { "Goodbye! Honk honk! 🦢", "See you later!", "Bye! Come back soon!" } },
            { "goodbye", new[] { "Goodbye! Honk honk! 🦢", "See you later!", "Bye! Come back soon!" } },
            { "thank", new[] { "You're welcome! 🦢", "No problem!", "Happy to help! Honk!" } },
            { "goose", new[] { "Yes, that's me! A goose! 🦢", "Honk honk! I love being a goose!", "Geese are the best!" } },
            { "honk", new[] { "HONK HONK! 🦢", "Honk! 🦢", "HONK! That's my language!" } },
        };

        // Default responses when no keyword is matched
        private string[] defaultResponses = new[]
        {
            "Honk! I'm not sure what you mean, but I'm listening! 🦢",
            "Interesting! Tell me more!",
            "Hmm, let me think about that... HONK!",
            "I'm just a goose, but that sounds fascinating!",
            "🦢 Honk honk! (That's goose for 'I hear you!')",
            "I appreciate you chatting with me!",
        };

        public string GetResponse(string userMessage, List<ChatMessage> conversationHistory)
        {
            if (string.IsNullOrWhiteSpace(userMessage))
            {
                return "Honk? Did you say something? 🦢";
            }

            // Convert to lowercase for matching
            string lowerMessage = userMessage.ToLower();

            // Check for keyword matches
            foreach (var kvp in responses)
            {
                if (lowerMessage.Contains(kvp.Key))
                {
                    // Return a random response for this keyword
                    return kvp.Value[random.Next(kvp.Value.Length)];
                }
            }

            // If no keyword matched, return a default response
            return defaultResponses[random.Next(defaultResponses.Length)];
        }
    }
}
