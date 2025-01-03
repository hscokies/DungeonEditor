using System.ComponentModel;

namespace DungeonEditor.Enumerations;

public enum Area : ushort
{
	[Description("Pthumeru (1)")]
    Pthumeru = 0x_1D_0A,
	[Description("Central Pthumeru (2)")]
    CentralPthumeru = 0x1D_14,
	[Description("Lower Pthumeru (3)")]
    LowerPthumeru = 0x1D_1E,
	[Description("Defiled Pthumeru (4)")]
    DefiledPthumeru = 0x1D_27,
	[Description("Pthumeru Ihyll (5)")]
    PthumeruIhyll = 0x1D_32,

	[Description("Hintertomb (2)")]
    Hintertomb = 0x1D_15,
	[Description("Lower Hintertomb (3)")]
    LowerHintertomb = 0x1D_1F,	
	
	[Description("Loran (4)")]
    Loran = 0x1D_2A,
	[Description("Lower Loran (5)")]
    LowerLoran = 0x1D_34,
	
	[Description("Isz (5)")]
    Isz = 0x1D_35
}