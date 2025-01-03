using System.ComponentModel;

namespace DungeonEditor.Dungeons;

public enum JoinRequirements : ushort
{
	[Description("Pthumeru Chalice (1)")]
	Pthumeru = 0x00_00_17_DD,
	[Description("Pthumeru Root Chalice (1)")]
	PthumeruRoot = 0x00_00_17_D4,
	
	[Description("Central Pthumeru Chalice (2)")]
    CentralPthumeru = 0x_00_00_18_41,
	[Description("Central Pthumeru Root Chalice (2)")]
    CentralPthumeruRoot = 0x_00_00_18_38,
    
    [Description("Hintertomb Chalice (2)")]
    Hintertomb = 0x_00_00_18_4B,
    [Description("Hintertomb Root Chalice (2)")]
    HintertombRoot = 0x_00_00_18_42,
    
    [Description("Lower Pthumeru Chalice (3)")]
    LowerPthumeru = 0x_00_00_18_A5,
    [Description("Lower Pthumeru Root Chalice (3)")]
    LowerPthumeruRoot = 0x_00_00_18_9C,
    [Description("Sinister Lower Pthumeru Root Chalice (3)")]
    LowerPthumeruRootSinister = 0x_00_00_18_9E,
    
    [Description("Lower Hintertomb Chalice (3)")]
    LowerHintertomb = 0x_00_00_18_AF,
    [Description("Lower Hintertomb Root Chalice (3)")]
    LowerHintertombRoot = 0x_00_00_18_A6,
    [Description("Sinister Hintertomb Root Chalice (3)")]
    LowerHintertombRootSinister = 0x_00_00_18_A8,

    [Description("Defiled Chalice (4)")]
    DefiledPthumeru = 0x_00_00_19_09,
    [Description("Cursed and Defiled Root Chalice (4)")]
    DefiledPthumeruRoot = 0x_00_00_19_01,
    
    [Description("Ailing Loran Chalice (4)")]
    Loran = 0x_00_00_19_1D,
    [Description("Ailing Loran Root Chalice (4)")]
    LoranRoot = 0x_00_00_19_14,
    
    [Description("Great Pthumeru Ihyll Chalice (5)")]
    PthumeruIhyll = 0x_00_00_19_6D,
    [Description("Pthumeru Ihyll Root Chalice (5)")]
    PthumeruIhyllRoot = 0x_00_00_19_64,
    [Description("Sinister Pthumeru Ihyll Root Chalice (5)")]
    PthumeruIhyllRootSinister = 0x_00_00_19_66,

    [Description("Lower Ailing Loran Chalice (5)")]
    LowerLoran = 0x_00_00_19_81,
    [Description("Lower Ailing Loran Root Chalice (5)")]
    LowerLoranRoot = 0x_00_00_19_78,
    [Description("Sinister Lower Loran Root Chalice (5)")]
    LowerLoranSinister = 0x_00_00_19_7A,
	
    [Description("Great Isz Chalice (5)")]
    Isz = 0x_00_00_19_8B,
    [Description("Isz Root Chalice (5)")]
    IszRoot = 0x_00_00_19_82,
    [Description("Sinister Isz Root Chalice (5)")]
    IszRootSinister = 0x_00_00_19_84,

    // Unused
    Pthumeru6RootDefiled =	0x_00_00_19_C9,
    Pthumeru7RootDefiled = 0x_00_00_1A_2D,
    Pthumeru7SinisterRoot =0x_00_00_1A_2E,
    Pthumeru7Root =	0x_00_00_1A_2F,
    Pthumeru8 =0x_00_00_1A_99,

    Loran6 = 0x_00_00_19_E5,
    Loran6RootDefiled = 0x_00_00_19_DD,
    Loran7RootDefiled =	0x_00_00_1A_41,
    Loran7SinisterRoot =	0x_00_00_1A_42,
    Loran7Root =	0x_00_00_1A_43,
    
    Isz6 = 0x_00_00_19_EF,
    Isz6RootDefiled =	0x_00_00_19E7,
    Isz7RootDefiled =	0x_00_00_1A4B,
    Isz7SinisterRoot =	0x_00_00_1A4C,
    Isz7Root =	0x_00_00_1A4D,
    
    
    CharacterDebug1 = 0x_1AF4,
    CharacterDebug2 = 0x_1AF5
}