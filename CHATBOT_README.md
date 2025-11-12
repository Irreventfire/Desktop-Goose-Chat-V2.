# Desktop Goose Chatbot Mod

A chatbot mod for Desktop Goose that allows users to chat with the goose! The goose can open a chat window that you can interact with.

## Features

- ✅ Interactive chat window with the Desktop Goose
- ✅ Basic hardcoded responses with personality
- ✅ Clean, modern UI with Windows Forms
- ✅ Easy to extend with AI capabilities
- ✅ Window can be moved around by the goose
- ✅ Persists across multiple goose interactions

## How It Works

The mod adds a new task called `ChatbotTask` that the goose can randomly choose to perform. When activated:

1. The goose opens a chat window
2. You can type messages and the goose responds
3. The chat window stays open even after the goose moves on to other tasks
4. You can chat with the goose anytime!

## Files Overview

### Core Components

- **`IChatResponseProvider.cs`** - Interface for response providers
  - Defines the contract for any chatbot implementation
  - Makes it easy to swap between different chatbot backends
  
- **`BasicChatResponseProvider.cs`** - Basic keyword-based responses
  - Simple pattern matching for common phrases
  - Easy to extend with more keywords and responses
  - Good starting point for testing
  
- **`ChatbotWindow.cs`** - The chat UI window
  - Windows Forms-based chat interface
  - Clean, modern design
  - Handles user input and displays conversation
  
- **`ChatbotTask.cs`** - Goose AI task
  - Manages when and how the chat window opens
  - Integrates with the Desktop Goose task system
  - Can be triggered randomly by the goose

## Adding AI Capabilities

To add an AI chatbot (like OpenAI GPT, local LLM, etc.), follow these steps:

### Step 1: Create Your AI Provider

Create a new class that implements `IChatResponseProvider`:

```csharp
using System;
using System.Collections.Generic;

namespace DefaultMod
{
    public class AIChatResponseProvider : IChatResponseProvider
    {
        public string GetResponse(string userMessage, List<ChatMessage> conversationHistory)
        {
            // Your AI integration code here
            // For example, call OpenAI API, local LLM, etc.
            
            // Example pseudo-code:
            // var response = await openAIClient.GetCompletion(userMessage, conversationHistory);
            // return response;
            
            return "AI-powered response here!";
        }
    }
}
```

### Step 2: Swap the Provider

In `ChatbotWindow.cs`, change the default provider:

```csharp
public ChatbotWindow()
{
    InitializeComponents();
    conversationHistory = new List<ChatMessage>();
    
    // Option 1: Change directly in constructor
    responseProvider = new AIChatResponseProvider();
    
    // Option 2: Set it later using SetResponseProvider()
    // responseProvider = new BasicChatResponseProvider();
}
```

### Step 3: Add Required Dependencies

If your AI provider needs external libraries:

1. Add NuGet packages or DLL references to `DefaultMod.csproj`
2. Include any required API keys or configuration files

### Example: OpenAI Integration

```csharp
public class OpenAIChatResponseProvider : IChatResponseProvider
{
    private readonly string apiKey;
    
    public OpenAIChatResponseProvider(string apiKey)
    {
        this.apiKey = apiKey;
    }
    
    public string GetResponse(string userMessage, List<ChatMessage> conversationHistory)
    {
        // Build conversation context
        var messages = new List<string>();
        messages.Add("You are a helpful and playful desktop goose. Respond in character with lots of personality and occasional 'honk!'");
        
        foreach (var msg in conversationHistory.TakeLast(10)) // Last 10 messages for context
        {
            messages.Add($"{msg.Sender}: {msg.Message}");
        }
        
        // Call OpenAI API (pseudo-code)
        // var response = CallOpenAI(apiKey, messages);
        // return response;
        
        return "AI response here";
    }
}
```

## Building the Mod

1. Open `GooseMod.sln` in Visual Studio
2. Build the solution (Release configuration recommended)
3. The compiled DLL will be in `DefaultMod/bin/Release/DefaultMod.dll`

## Installing the Mod

1. Build the mod (see above)
2. Copy `DefaultMod.dll` to `Desktop Goose/Assets/Mods/ChatbotMod/`
3. Do NOT copy the `GooseModdingAPI.dll` - it's already in the main application
4. Enable mods in the Desktop Goose config
5. Run Desktop Goose!

## Customization Ideas

### Response Personality
Edit `BasicChatResponseProvider.cs` to add more keywords and responses:

```csharp
private Dictionary<string, string[]> responses = new Dictionary<string, string[]>
{
    { "your_keyword", new[] { "Response 1", "Response 2", "Response 3" } },
    // Add more keywords...
};
```

### UI Styling
Modify `ChatbotWindow.cs` to change colors, fonts, sizes:

```csharp
this.BackColor = Color.YourColor;
chatDisplay.Font = new Font("YourFont", 10f);
```

### Task Frequency
In `ChatbotTask.cs`, adjust when the task can be picked:

```csharp
canBePickedRandomly = true; // Set to false to prevent random activation
```

## Advanced Features

### Manual Activation
Add a hotkey or menu item to manually open the chat:

```csharp
// In your mod's Init() or elsewhere:
API.Goose.setCurrentTaskByID(gooseEntity, "ChatbotTask");
```

### Persistent Chat History
Save conversation history to a file:

```csharp
// Add to ChatbotWindow.cs
private void SaveHistory()
{
    System.IO.File.WriteAllText("chat_history.json", 
        JsonConvert.SerializeObject(conversationHistory));
}
```

### Multiple Response Providers
Switch between providers based on user preference:

```csharp
public void SetResponseMode(string mode)
{
    switch (mode)
    {
        case "basic":
            SetResponseProvider(new BasicChatResponseProvider());
            break;
        case "ai":
            SetResponseProvider(new AIChatResponseProvider());
            break;
    }
}
```

## Contributing

Feel free to extend this mod! Some ideas:

- Add voice input/output
- Integrate with various AI services
- Add emoticons and rich text
- Create themed response packs
- Add mini-games within the chat
- Support for multiple languages

## License

This is a mod for Desktop Goose. Follow the Desktop Goose modding guidelines and license.

## Credits

Created as part of the Desktop Goose modding community! 🦢
