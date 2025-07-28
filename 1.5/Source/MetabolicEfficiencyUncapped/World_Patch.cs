using HarmonyLib;
using RimWorld.Planet;
using Verse;

namespace MetabolicEfficiencyUncapped;

[HarmonyPatch(typeof(World))]
public static class World_Patch
{
    [HarmonyPatch(nameof(World.FinalizeInit))]
    [HarmonyPostfix]
    public static void FinalizeInit()
    {
        MetabolicEfficiencyUncappedMod.settings.OverrideSettings();
    }
}
