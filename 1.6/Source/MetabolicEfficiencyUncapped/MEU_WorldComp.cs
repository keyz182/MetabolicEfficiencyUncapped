using RimWorld.Planet;
using Verse;

namespace MetabolicEfficiencyUncapped;

public class MEU_WorldComp(World world) : WorldComponent(world)
{
    public override void FinalizeInit(bool fromLoad)
    {
        MetabolicEfficiencyUncappedMod.settings.OverrideSettings();
    }

    public override void WorldComponentTick()
    {
        // Overkill, but low impact
        if (Find.TickManager.TicksGame % 30 == 0)
            MetabolicEfficiencyUncappedMod.settings.OverrideSettings();
    }
}
