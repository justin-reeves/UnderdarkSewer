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
            var locOriginal = typeof(GameLocation).GetMethod("drawWater");
            var drawPrefix = helper.Reflection.GetMethod(typeof(LocationFix), "Prefix").MethodInfo;
            var constructorPostfix = helper.Reflection.GetMethod(typeof(SewerMapFix), "ConstructorPostfix").MethodInfo;
            var sharedStatePostfix = helper.Reflection.GetMethod(typeof(SewerMapFix), "SharedStatePostfix").MethodInfo;
            harmony.Patch(original, null, new HarmonyMethod(constructorPostfix));
            harmony.Patch(original, null, new HarmonyMethod(sharedStatePostfix));
            harmony.Patch(locOriginal, new HarmonyMethod(drawPrefix), null);
        }
    }

    public static class SewerMapFix
    {
        static void ConstructorPostfix(Sewer __instance) 
        {
            __instance.waterColor.Value = new Color(255, 150, 255);

            var steamColorField = AccessTools.Field(typeof(Sewer), "steamColor");
            steamColorField.SetValue(__instance, new Color(255, 200, 255));
        }

        static void SharedStatePostfix(Sewer __instance)
        {
            __instance.waterColor.Value = new Color(255, 150, 255);
        }
    }

    public static class LocationFix
    {
        static void Prefix(GameLocation __instance)
        {
            if (__instance is Sewer)
            {
                __instance.waterColor.Value = new Color(255, 150, 255);
            }
        }
    }
}