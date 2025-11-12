using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 1. Added the "GooseModdingAPI" project as a reference.
// 2. Compile this.
// 3. Create a folder with this DLL in the root, and *no GooseModdingAPI DLL*
using GooseShared;
using SamEngine;

namespace DefaultMod
{
    public class ModEntryPoint : IMod
    {
        // Gets called automatically, passes in a class that contains pointers to
        // useful functions we need to interface with the goose.
        void IMod.Init()
        {
            // Subscribe to whatever events we want
            InjectionPoints.PostTickEvent += PostTick;
            
            // The ChatbotTask is automatically registered with the goose
            // because it inherits from GooseTaskInfo.
            // The goose will randomly choose to open the chatbot window!
        }

        public void PostTick(GooseEntity g)
        {
            // Do whatever you want here.

            // If we're running our mod's task
            if (g.currentTask == API.TaskDatabase.getTaskIndexByID("FollowMouseDrifty"))
            {
                // Lock our goose facing one direction for some reason?
                g.direction = 0;
            }
            
            // You could also manually trigger the chatbot task here if needed:
            // API.Goose.setCurrentTaskByID(g, "ChatbotTask");
        }
    }
}
