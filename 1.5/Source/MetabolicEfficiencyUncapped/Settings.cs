using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace MetabolicEfficiencyUncapped;

public class Settings : ModSettings
{
    public static SimpleCurve MetabolismToFoodConsumptionFactorCurveNew = new SimpleCurve()
    {
        { new CurvePoint(-100000f, 100000f), true },
        { new CurvePoint(-10000f, 10000f), true },
        { new CurvePoint(-1000f, 5000f), true },
        { new CurvePoint(-100f, 200f), true },
        { new CurvePoint(-50f, 50f), true },
        { new CurvePoint(-40f, 40f), true },
        { new CurvePoint(-30f, 30f), true },
        { new CurvePoint(-20f, 20f), true },
        { new CurvePoint(-10f, 5f), true },
        { new CurvePoint(-5f, 2.25f), true },
        { new CurvePoint(0.0f, 1f), true },
        { new CurvePoint(5f, 0.5f), true },
        { new CurvePoint(10f, 0.05f), true },
        { new CurvePoint(100f, 0.005f), true },
        { new CurvePoint(1000f, 0f), true },
    };

    public static SimpleCurve MetabolismToFoodConsumptionFactorCurveOrig = new()
    {
        { new CurvePoint(-5f, 2.25f), true },
        { new CurvePoint(0.0f, 1f), true },
        { new CurvePoint(5f, 0.5f), true },
    };


    public static readonly SimpleCurve ComplexityToCreationHoursCurveNew = new SimpleCurve()
    {
        {
            new CurvePoint(0.0f, 3f),
            true
        },
        {
            new CurvePoint(4f, 5f),
            true
        },
        {
            new CurvePoint(8f, 8f),
            true
        },
        {
            new CurvePoint(12f, 12f),
            true
        },
        {
            new CurvePoint(16f, 17f),
            true
        },
        {
            new CurvePoint(20f, 23f),
            true
        },
        {
            new CurvePoint(50f, 144f),
            true
        },
        {
            new CurvePoint(100f, 300f),
            true
        },
        {
            new CurvePoint(1000f, 5000f),
            true
        },
        {
            new CurvePoint(10000f, 70000f),
            true
        }
    };

    public static readonly SimpleCurve ComplexityToCreationHoursCurveOrig = new SimpleCurve()
    {
        {
            new CurvePoint(0.0f, 3f),
            true
        },
        {
            new CurvePoint(4f, 5f),
            true
        },
        {
            new CurvePoint(8f, 8f),
            true
        },
        {
            new CurvePoint(12f, 12f),
            true
        },
        {
            new CurvePoint(16f, 17f),
            true
        },
        {
            new CurvePoint(20f, 23f),
            true
        }
    };

    public bool EnableExtendedMetabolismMultipliers = true;
    public bool EnableExtendedComplexityCurve = true;
    public bool EnableExtendedBioStatRange = true;

    public void DoWindowContents(Rect wrect)
    {
        Listing_Standard options = new();
        options.Begin(wrect);

        options.CheckboxLabeled("MetabolicEfficiencyUncapped_EnableExtendedMetabolismMultipliers".Translate(), ref EnableExtendedMetabolismMultipliers);
        options.Gap();
        options.CheckboxLabeled("MetabolicEfficiencyUncapped_EnableExtendedComplexityCurve".Translate(), ref EnableExtendedComplexityCurve);
        options.Gap();
        options.CheckboxLabeled("MetabolicEfficiencyUncapped_EnableExtendedBioStatRange".Translate(), ref EnableExtendedBioStatRange);
        options.Gap();

        options.End();
    }

    public override void ExposeData()
    {
        Scribe_Values.Look(ref EnableExtendedMetabolismMultipliers, "EnableExtendedMetabolismMultipliers", true);
        Scribe_Values.Look(ref EnableExtendedComplexityCurve, "EnableExtendedComplexityCurve", true);
        Scribe_Values.Look(ref EnableExtendedBioStatRange, "EnableExtendedBioStatRange", true);

        FieldInfo MetabolismToFoodConsumptionFactorCurveField = AccessTools.Field(typeof(GeneTuning), nameof(GeneTuning.MetabolismToFoodConsumptionFactorCurve));
        FieldInfo ComplexityToCreationHoursCurveField = AccessTools.Field(typeof(GeneTuning), nameof(GeneTuning.ComplexityToCreationHoursCurve));
        FieldInfo BiostatRangeField = AccessTools.Field(typeof(GeneTuning), nameof(GeneTuning.BiostatRange));

        MetabolismToFoodConsumptionFactorCurveField.SetValue(
            null,
            EnableExtendedMetabolismMultipliers ? MetabolismToFoodConsumptionFactorCurveNew : MetabolismToFoodConsumptionFactorCurveOrig
        );

        ComplexityToCreationHoursCurveField.SetValue(
            null,
            EnableExtendedComplexityCurve ? ComplexityToCreationHoursCurveNew : ComplexityToCreationHoursCurveOrig
        );

        BiostatRangeField.SetValue(
            null,
            EnableExtendedBioStatRange ? new IntRange(-100,100) : new IntRange(-5,5)
        );
    }
}
