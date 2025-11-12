# Quick Start Guide - Desktop Goose Chatbot Mod

## Installation Steps

1. **Build the Mod**
   ```bash
   cd GooseMod_DefaultSolution
   # Windows with Visual Studio:
   msbuild GooseMod.sln /p:Configuration=Release
   
   # Linux/Mac with Mono:
   xbuild GooseMod.sln /p:Configuration=Release
   ```

2. **Locate the Built DLL**
   - Find `DefaultMod.dll` in `GooseMod_DefaultSolution/DefaultMod/bin/Release/`

3. **Install to Desktop Goose**
   - Copy `DefaultMod.dll` to: `[Desktop Goose Install]/Assets/Mods/ChatbotMod/`
   - Create the `ChatbotMod` folder if it doesn't exist
   - **Important:** Do NOT copy `GooseModdingAPI.dll` - the main app already has it

4. **Enable Mods**
   - Open Desktop Goose's config file
   - Set `EnableMods=True`
   - Save and restart Desktop Goose

5. **Use the Chatbot**
   - Run Desktop Goose
   - The goose will randomly open the chat window
   - Type messages and chat with the goose!
   - The window stays open even when the goose does other things

## Features

✅ **Basic Chat Interface** - Clean, modern Windows Forms UI
✅ **Keyword Responses** - Responds to greetings, questions, and common phrases
✅ **Easy AI Integration** - Implement `IChatResponseProvider` to add AI
✅ **Persistent Window** - Chat window stays open across goose activities
✅ **Goose-themed Personality** - Responses include "honk!" and goose personality

## Testing the Mod

Try these messages to test the chatbot:
- "hello"
- "how are you"
- "what are you"
- "honk"
- "thank you"
- "bye"

## Adding AI (Quick Example)

Create `AIChatResponseProvider.cs`:

```csharp
using System;
using System.Collections.Generic;

namespace DefaultMod
{
    public class AIChatResponseProvider : IChatResponseProvider
    {
        public string GetResponse(string userMessage, List<ChatMessage> conversationHistory)
        {
            // Your AI API call here
            // Example: OpenAI, Claude, local LLM, etc.
            return "AI-powered response!";
        }
    }
}
```

Then in `ChatbotWindow.cs`, change:
```csharp
responseProvider = new AIChatResponseProvider();
```

See `CHATBOT_README.md` for detailed AI integration guide.

## Troubleshooting

**Mod doesn't load:**
- Check that `EnableMods=True` in config
- Verify DLL is in correct folder structure
- Check Desktop Goose logs for errors

**Chat window doesn't open:**
- Wait for goose to randomly trigger it (may take a few minutes)
- Or manually trigger: Add code to call `API.Goose.setCurrentTaskByID(goose, "ChatbotTask")`

**Build errors:**
- Ensure you have .NET Framework 4.5.2 or higher
- Install Mono (Linux/Mac) or Visual Studio (Windows)
- Check all new files are included in `DefaultMod.csproj`

## Support

For more details, see:
- `CHATBOT_README.md` - Full documentation and customization guide
- `What is this.txt` - General mod development info
- Desktop Goose modding Discord

Happy chatting! 🦢
