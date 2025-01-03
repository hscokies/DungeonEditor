using DungeonEditor.Attributes;

namespace DungeonEditor.Enumerations;

public enum Effect : byte
{
    [GemCategory(GemCategory.Random)]
    None1 = 0x_4D,
    [GemCategory(GemCategory.Random)]
    None2 = 0x4E,
    [GemCategory(GemCategory.Random)]
    SharedDefault = 0x_FF,

    #region Special enemy / shop
    [GemCategory(GemCategory.FireBolt)]
    IszDefault = 0x_4F,
    [GemCategory(GemCategory.FireBolt)]
    IszBath = 0x_50,
    [GemCategory(GemCategory.FireBolt)]
    IszBps = 0x_51,
    [GemCategory(GemCategory.FireBolt)]
    IszPatches = 0x_52,
    [GemCategory(GemCategory.FireBolt)]
    IszBathBps = 0x_53,
    [GemCategory(GemCategory.FireBolt)]
    IszPatchesBps = 0x_54,
    [GemCategory(GemCategory.Random)]
    SharedBath = 0x_1E, 
    [GemCategory(GemCategory.Random)]
    SharedBps = 0x_1F,
    [GemCategory(GemCategory.Random)]
    SharedPatches = 0x_20,
    [GemCategory(GemCategory.Random)]
    SharedBathBps = 0x_21,
    [GemCategory(GemCategory.Random)]
    SharedPatchesBps = 0x_22,
    #endregion

    #region Unique items
    [GemCategory(GemCategory.Random)]
    CoffinsShared = 0x_23,
    [GemCategory(GemCategory.Radiant)]
    CoffinsPthumeru = 0x_24,
    [GemCategory(GemCategory.PhysicalUpNearDeath)]
    CoffinsHintertomb = 0x_25,
    [GemCategory(GemCategory.Pulsing)]
    CoffinsLoran = 0x_26,
    [GemCategory(GemCategory.ArcaneNourishing)]
    CoffinsIsz = 0x_27,
    #endregion

    #region Gem category
    [GemCategory(GemCategory.Physical)]
    GemPthumeruDefault = 0x_32,
    [GemCategory(GemCategory.PoisonsHunter)]
    GemHintertomb = 0x_33,
    [GemCategory(GemCategory.FireBolt)]
    GemLoran = 0x_34,
    [GemCategory(GemCategory.Physical)]
    GemIsz = 0x_35,
    [GemCategory(GemCategory.Physical)]
    GemPthumeruDifficultyUp = 0x_36,
    [GemCategory(GemCategory.Pulsing)]
    GemLoranStory = 0x_61,
    [GemCategory(GemCategory.PhysicalUpNearDeath)]
    GemHintertombStory = 0x_5F,
    [GemCategory(GemCategory.OpenFoes)]
    GemPthumeruStory = 0x_5D,
    [GemCategory(GemCategory.Radiant)]
    GemIszStory = 0x_5B,
    #endregion

    #region 4th layer
    [GemCategory(GemCategory.Random)]
    FourthLayerNoGemCategory = 0x_3C,
    [GemCategory(GemCategory.Striking)]
    FourthLayerClosedSinisterPthumeru5 = 0x_5C,
    [GemCategory(GemCategory.Striking)]
    FourthLayerPthumeru = 0x_3D,
    [GemCategory(GemCategory.Striking)]
    FourthLayerClosedPthumeru = 0x_3E,
    [GemCategory(GemCategory.Striking)]
    FourthLayerUnused1 = 0x_3F,
    [GemCategory(GemCategory.Striking)]
    FourthLayerClosedUnused1 = 0x_40,
    [GemCategory(GemCategory.Striking)]
    FourthLayerUnused2 = 0x_41,
    [GemCategory(GemCategory.Striking)]
    FourthLayerClosedUnused2 = 0x_42,
    [GemCategory(GemCategory.Radiant)]
    FourthLayerIsz = 0x_43,
    [GemCategory(GemCategory.Radiant)]
    FourthLayerClosedIsz = 0x_44,
    #endregion

    #region Environment effects
    [GemCategory(GemCategory.Random)]
    HintertombPoison = 0x_0A,
    [GemCategory(GemCategory.OpenFoes)]
    PthumeruPoisons = 0x_0D,
    [GemCategory(GemCategory.OpenFoes)]
    PthumeruNoPoison = 0x_0E,
    [GemCategory(GemCategory.PoisonsHunter)]
    IszNoPoison = 0x_0F,
    [GemCategory(GemCategory.Unknown)]
    UnusedOil = 0x_01,
    #endregion

    #region Rites
    [GemCategory(GemCategory.Random)]
    SharedFixed = 0x_2D,
    [GemCategory(GemCategory.Random)]
    Sinister = 0x_28,
    [GemCategory(GemCategory.Random)]
    Fetid = 0x_0B,
    
    [GemCategory(GemCategory.Random)]
    EyeCollector = 0x_14,
    [GemCategory(GemCategory.Random)]
    TombProspectors = 0x_15,
    [GemCategory(GemCategory.Random)]
    Ritekeeper = 0x_16,
    [GemCategory(GemCategory.Random)]
    EyeCollectorTombProspectors = 0x_18,
    [GemCategory(GemCategory.Random)]
    EyeCollectorRitekeeper = 0x_19,
    [GemCategory(GemCategory.Random)]
    TombProspectorsRitekeeper = 0x_1B,
    [GemCategory(GemCategory.Random)]
    MarionetteUnused1 = 0x_17,
    [GemCategory(GemCategory.Random)]
    MarionetteUnused2 = 0x_1A,
    [GemCategory(GemCategory.Random)]
    MarionetteUnused3 = 0x_1C,
    [GemCategory(GemCategory.Random)]
    MarionetteUnused4 = 0x_1D,

    [GemCategory(GemCategory.Random)]
    CursedDefiled  = 0x_46,
    [GemCategory(GemCategory.Random)]
    TerribleCurse1 = 0x_47,
    [GemCategory(GemCategory.Random)]
    TerribleCurse2 = 0x_48,
    [GemCategory(GemCategory.Random)]
    Cursed = 0x_49,
    
    #endregion

    #region Bytes controlling whether you can buy certain Sinister Chalices from merchants (Baths/Patches)
    [GemCategory(GemCategory.Random)]
    SellSinisterLowerPthumeru = 0x_55,
    [GemCategory(GemCategory.Random)]
    SellSinisterHintertomb = 0x_56,
    [GemCategory(GemCategory.Random)]
    SellSinisterPthumeruIhyll = 0x_57,
    [GemCategory(GemCategory.Random)]
    SellSinisterLowerLoran = 0x_58,
    [GemCategory(GemCategory.Random)]
    SellSinisterIsz = 0x_59,
    #endregion

    #region Root Chalices drop
    [GemCategory(GemCategory.Physical)]
    PthumeruRootChalicesDrop = 0x_5A,
    [GemCategory(GemCategory.PoisonsHunter)]
    HintertombRootChalicesDrop = 0x_5E,
    [GemCategory(GemCategory.FireBolt)]
    LoranRootChalicesDrop = 0x_60,
    [GemCategory(GemCategory.ArcaneNourishing)]
    IszRootChalicesDrop = 0x_62
    #endregion
}