using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Harmony;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Locations;

namespace YourProjectName
{
    
    public class ModEntry : Mod
    {
        
        public override void Entry(IModHelper helper)
        {
            var harmony = HarmonyInstance.Create("com.github.kirbylink.underdarksewer");
            var original = typeof(Sewer).GetConstructor(new Type[] { typeof(string), typeof(string) });
            var postfix = helper.Reflection.GetMethod(typeof(SewerMapFix), "Postfix").MethodInfo;
            harmony.Patch(original, null, new HarmonyMethod(postfix));
        }
    }

    public static class SewerMapFix
    {
        static bool Postfix(Sewer __instance, string map, string name) 
        {
            
            return false;
        }
    }
}