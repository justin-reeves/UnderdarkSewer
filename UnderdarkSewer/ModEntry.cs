using System;
using Harmony;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;

namespace UnderdarkSewer
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
        static void Postfix(ref Color ___waterColor, ref Color ___steamColor, ref NPC ___krobus) 
        {
            ___waterColor = Color.Indigo;
            ___steamColor = new Color(255, 200, 255);
            ___krobus.Sprite = new AnimatedSprite("Characters\\Krobus", 0, 16, 32);
        }
    }
}