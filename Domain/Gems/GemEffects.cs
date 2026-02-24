using System.ComponentModel;

namespace Domain.Gems;

public enum PrimaryGemEffect
{
    [Description("Physical ATK UP %")]
    Tempering,

    [Description("Add rapid poison effect")]
    Dirty,

    [Description("STR scaling")]
    Heavy,

    [Description("Add slow poison effect")]
    Murky,

    [Description("SKL scaling")]
    Sharp,

    [Description("ATK vs beasts UP %")]
    Beasthunters,

    [Description("ATK vs the kin UP %")]
    Kinhunters,

    [Description("Fire ATK UP %")]
    Fire,

    [Description("Arcane ATK UP %")]
    Arcane,

    [Description("Bolt ATK UP %")]
    Bolt,

    [Description("Blunt ATK UP %")]
    AdeptBlunt,

    [Description("Thrust ATK UP %")]
    AdeptThrust,

    [Description("Charge ATK UP %")]
    Striking,

    [Description("HP continues to recover")]
    Pulsing,

    [Description("Reduces stamina costs")]
    Radiant,

    [Description("Phys. UP near death %")]
    Poormans,

    [Description("Blood ATK UP %")]
    Bloodtinge,

    [Description("Arcane scaling")]
    Cold,

    [Description("ATK UP at full HP %")]
    FoolsNourishing,

    [Description("Phys. UP at full HP %")]
    Fools,

    [Description("ATK UP %")]
    Nourishing,

    [Description("ATK UP near death %")]
    PoormansNourishing,

    [Description("Bloodtinge scaling")]
    Warm,
}

public enum SecondaryGemEffect
{
    [Description("Add physical ATK")]
    Tempering,

    [Description("Add rapid poison effect")]
    Dirty,

    [Description("Add slow poison effect")]
    Murky,

    [Description("ATK vs beasts UP %")]
    Beasthunters,

    [Description("Add bolt ATK")]
    Bolt,

    [Description("Add fire ATK")]
    Fire,

    [Description("Boosts rally potential")]
    Lethal,

    [Description("WPN durability UP")]
    Dense,

    [Description("ATK vs open foes UP %")]
    Finestrike,

    [Description("HP continues to recover")]
    Pulsing,

    [Description("Reduces stamina costs")]
    Radiant,

    [Description("Charge ATK UP %")]
    Striking,

    [Description("Phys. UP near death %")]
    Poormans,

    [Description("Add arcane ATK")]
    Arcane,

    [Description("Add blood ATK")]
    Bloodtinge,

    [Description("ATK UP at full HP %")]
    FoolsNourishing,

    [Description("Phys. UP at full HP %")]
    Fools,

    [Description("ATK UP near death %")]
    PoormansNourishing,

    [Description("ATK vs the kin UP %")]
    Kinhunters,
}