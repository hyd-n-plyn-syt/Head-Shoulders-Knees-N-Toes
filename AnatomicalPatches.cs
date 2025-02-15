using System.Collections.Generic;
using HarmonyLib;
using XRL.World.Anatomy;

namespace HeadShouldersKneesNToes
{
    [HarmonyPatch(typeof(Anatomies), "Init")]
    public static class Anatomies_Init_Patch
    {
        public static void Postfix()
        {
            ReplaceHumanoidAnatomy();
        }

        public static void ReplaceHumanoidAnatomy()
        {
            // Remove the old anatomy entry
            if (Anatomies.AnatomyTable.ContainsKey("Humanoid"))
            {
                Anatomies.AnatomyTable.Remove("Humanoid");
            }

            // Add the new anatomy entry
            Anatomies.AnatomyTable["Humanoid"] = new Anatomy("Humanoid")
            {
                Parts = new List<AnatomyPart>
                {
                    new AnatomyPart(Anatomies.GetBodyPartType("Head"))
                    {
                        Subparts = new List<AnatomyPart>
                        {
                            new AnatomyPart(Anatomies.GetBodyPartType("Face"))
                            {
                                Subparts = new List<AnatomyPart>
                                {
                                    new AnatomyPart(Anatomies.GetBodyPartType("Eyes")),
                                    new AnatomyPart(Anatomies.GetBodyPartType("Mouth"))
                                }
                            }
                        }
                    },
                    new AnatomyPart(Anatomies.GetBodyPartType("Back")),
                    new AnatomyPart(Anatomies.GetBodyPartType("Arm")) { Laterality = 1, Subparts = new List<AnatomyPart> { new AnatomyPart(Anatomies.GetBodyPartType("Hand")) { Laterality = 1, SupportsDependent = "Hands" } } },
                    new AnatomyPart(Anatomies.GetBodyPartType("Arm")) { Laterality = 2, Subparts = new List<AnatomyPart> { new AnatomyPart(Anatomies.GetBodyPartType("Hand")) { Laterality = 2, SupportsDependent = "Hands" } } },
                    new AnatomyPart(Anatomies.GetBodyPartType("Missile Weapon")) { Laterality = 1 },
                    new AnatomyPart(Anatomies.GetBodyPartType("Missile Weapon")) { Laterality = 2 },
                    new AnatomyPart(Anatomies.GetBodyPartType("Hands")) { DependsOn = "Hands" },
                    new AnatomyPart(Anatomies.GetBodyPartType("Feet"))
                }
            };
        }
    }
}
