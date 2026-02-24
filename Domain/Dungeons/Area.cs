using System.ComponentModel;

namespace Domain.Dungeons;

public enum Area
{
    [Description("Pthumeru (1)")] 
    Pthumeru1 = 0x01,

    [Description("Pthumeru (2)")] 
    Pthumeru2 = 0x14,

    [Description("Hintertomb (2)")] 
    Hintertomb2 = 0x15,

    [Description("Pthumeru (3)")] 
    Pthumeru3 = 0x1E,

    [Description("Hintertomb (3)")] 
    Hintertomb3 = 0x1F,

    [Description("Pthumeru (4)")]
    Pthumeru4 = 0x28,

    [Description("Loran (4)")]
    Loran4 = 0x2A,

    [Description("Pthumeru 5")]
    Pthumeru5 = 0x32,

    [Description("Loran (5)")]
    Loran5 = 0x34,

    [Description("Isz (5)")] 
    Isz5 = 0x35,

    /***************************************************************
     * Unused dungeons, extracted from CUSA03173/dvdroot_ps4/map *
     ***************************************************************/
    [Description("New Dungeon 0_GD")] 
    UnusedTestSingleInstance = 0x00,

    [Description("For Osawa / Generation tests")]
    UnusedForOsawaCheck = 0x02,

    [Description("Group tour test")] 
    UnusedGroupTourTest = 0x03,

    [Description("Fluorescent flower test")]
    UnusedFluorescentFlowerTest = 0x04,

    [Description("Dungeons for presentation")]
    UnusedPresentation = 0x05,

    [Description("Enemies tests")] 
    UnusedEnemiesTest = 0x09,

    [Description("Sample dungeons")] 
    UnusedSampleDungeon = 0x0A
}