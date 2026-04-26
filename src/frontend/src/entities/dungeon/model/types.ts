export interface DungeonPreview {
    id: string;
    joinRequirement: JoinRequirement;
}

export interface GetMapsResponse {
    maps: string[];
}

export interface Dungeon {
    map: string;
    joinRequirement: JoinRequirement;
    effect1: number;
    effect2: number;
    effect3: number;
    effect4: number;
    effect5: number;
    effect6: number;
    effect7: number;
    effect8: number;
    effect9: number;
    authorPSN: string;
    authorCharacter: string;
}

export enum JoinRequirement {
    Pthumeru = 0x00_00_17_dd,
    PthumeruRoot = 0x00_00_17_d4,
    CentralPthumeru = 0x00_00_18_41,
    CentralPthumeruRoot = 0x00_00_18_38,
    Hintertomb = 0x00_00_18_4b,
    HintertombRoot = 0x00_00_18_42,
    LowerPthumeru = 0x00_00_18_a5,
    LowerPthumeruRoot = 0x00_00_18_9c,
    LowerPthumeruRootSinister = 0x00_00_18_9e,
    LowerHintertomb = 0x00_00_18_af,
    LowerHintertombRoot = 0x00_00_18_a6,
    LowerHintertombRootSinister = 0x00_00_18_a8,
    DefiledPthumeru = 0x00_00_19_09,
    DefiledPthumeruRoot = 0x00_00_19_01,
    Loran = 0x00_00_19_1d,
    LoranRoot = 0x00_00_19_14,
    PthumeruIhyll = 0x00_00_19_6d,
    PthumeruIhyllRoot = 0x00_00_19_64,
    PthumeruIhyllRootSinister = 0x00_00_19_66,
    LowerLoran = 0x00_00_19_81,
    LowerLoranRoot = 0x00_00_19_78,
    LowerLoranSinister = 0x00_00_19_7a,
    Isz = 0x00_00_19_8b,
    IszRoot = 0x00_00_19_82,
    IszRootSinister = 0x00_00_19_84,

    // Unused
    Pthumeru6RootDefiled = 0x00_00_19_c9,
    Loran6RootDefiled = 0x00_00_19_dd,
    Isz6RootDefiled = 0x00_00_19_e7,
    Pthumeru7RootDefiled = 0x00_00_1a_2d,
    Pthumeru7SinisterRoot = 0x00_00_1a_2e,
    Pthumeru7RootTerribleCurse = 0x00_00_1a_2f,
    Loran6 = 0x00_00_19_e5,
    Pthumeru8 = 0x00_00_1a_99,
    Loran7RootDefiled = 0x00_00_1a_41,
    Loran7SinisterRoot = 0x00_00_1a_42,
    Loran7RootTerribleCurse = 0x00_00_1a_43,
    Isz6 = 0x00_00_19_ef,
    Isz7RootDefiled = 0x00_00_1a_4b,
    Isz7SinisterRoot = 0x00_00_1a_4c,
    Isz7Root = 0x00_00_1a_4d,
    CharacterDebug1 = 0x00_00_1a_f4,
    CharacterDebug2 = 0x00_00_1a_f5,
}

export enum Effect {
    Default = 0xff,

    IszDefault = 0x4f,
    IszBath = 0x50,
    IszBps = 0x51,
    IszPatches = 0x52,
    IszBathBps = 0x53,
    IszPatchesBps = 0x54,

    Bath = 0x1e,
    Bps = 0x1f,
    Patches = 0x20,
    BathBps = 0x21,
    PatchesBps = 0x22,

    CoffinsShared = 0x23,
    CoffinsPthumeru = 0x24,
    CoffinsHintertomb = 0x25,
    CoffinsLoran = 0x26,
    CoffinsIsz = 0x27,

    GemEffectPthumeru = 0x32,
    GemEffectHintertomb = 0x33,
    GemEffectLoran = 0x34,
    GemEffectIsz = 0x35,
    DifficultyUp = 0x36,

    Layer4OpenShared = 0x3c,
    Layer4ClosedPthumeru5 = 0x5c,
    Layer4OpenPthumeru = 0x3d,
    Layer4ClosedPthumeru = 0x3e,
    Layer4OpenUnused1 = 0x3f,
    Layer4ClosedUnused1 = 0x40,
    Layer4OpenUnused2 = 0x41,
    Layer4ClosedUnused2 = 0x42,
    Layer4OpenIsz = 0x43,
    Layer4ClosedIsz = 0x44,

    PoisonousEnvironmentHintertomb = 0x0a,
    PoisonousEnvironmentPthumeru = 0x0d,
    NonPoisonousEnvironmentHintertomb = 0x0e,
    NonPoisonousEnvironmentPthumeru = 0x0f,
    OilyEnvironment = 0x01,

    SharedFixed = 0x2d,
    Sinister = 0x28,
    Fetid = 0x0b,

    Cursed = 0x49,
    CursedPthumeru4 = 0x46,
    TerribleCurse = 0x47,
    TerribleCurse2 = 0x48,

    EyeCollector = 0x14,
    TombProspector = 0x15,
    Ritekeeper = 0x16,
    Marionette1 = 0x17,
    EyeCollectorTombProspector = 0x18,
    EyeCollectorRitekeeper = 0x19,
    Marionette2 = 0x1a,
    TombProspectorRitekeeper = 0x1b,
    Marionette3 = 0x1c,
    Marionette4 = 0x1d,

    SinisterPthumeru3 = 0x55,
    SinisterHintertomb3 = 0x56,
    SinisterPthumeru5DifficultyUp = 0x57,
    SinisterLoran5 = 0x58,
    SinisterIsz5 = 0x59,

    RootChaliceDropPthumeru = 0x5a,
    RootChaliceDropUnknown1 = 0x5b,
    RootChaliceDropUnknown2 = 0x5d,
    RootChaliceDropHintertomb = 0x5e,
    RootChaliceDropUnknown3 = 0x5f,
    RootChaliceDropLoran = 0x60,
    RootChaliceDropUnknown4 = 0x61,
    RootChaliceDropIsz = 0x62,
}
