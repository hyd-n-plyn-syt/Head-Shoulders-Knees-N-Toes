using System.Collections.Generic;
using HarmonyLib;
using XRL;
using XRL.World;
using XRL.World.Parts;

namespace HeadShouldersKneesNToes
{
    [HarmonyPatch(typeof(GameObject), "AddPart")]
    public static class EyewearPartPatches
    {
        private static readonly Dictionary<string, string> EyewearItems = new Dictionary<string, string>
        {
            { "Goggles", "Eyes" },
            { "Mirrorshades", "Eyes" },
            { "Night-vision Goggles", "Eyes" },
            { "Telescopic Monocle", "Eyes" },
            { "Spectacles", "Eyes" },
            { "Telemetric Visor", "Eyes" },
            { "Night-Sight Interpolators", "Eyes" },
            { "VISAGE", "Eyes" }
        };

        public static void Prefix(GameObject __instance, IPart Part)
        {
            if (Part is Armor armor && EyewearItems.ContainsKey(__instance.Blueprint))
            {
                armor.WornOn = EyewearItems[__instance.Blueprint];
            }
        }
    }

    [HarmonyPatch(typeof(GameObjectFactory), "CreateFromBlueprint")] 
    public class Goggles_CreateFromBlueprint_Patch
    {
        public static void Postfix(string Blueprint, GameObject __result)
        {
            if (Blueprint == "Goggles" && __result != null)
            {
                Armor armor = __result.GetPart<Armor>();
                if (armor != null)
                {
                    armor.WornOn = "Eyes";
                }
            }
        }
    }

    [HarmonyPatch(typeof(GameObjectFactory), "CreateFromBlueprint")]
    public class Mirrorshades_CreateFromBlueprint_Patch 
    {
        public static void Postfix(string Blueprint, GameObject __result)
        {
            if (Blueprint == "Mirrorshades" && __result != null)
            {
                Armor armor = __result.GetPart<Armor>();
                if (armor != null)
                {
                    armor.WornOn = "Eyes";
                }
            }
        }
    }

    // ... repeat for each eyewear item
}
