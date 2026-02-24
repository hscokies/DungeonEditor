using Domain.Dungeons;
using Domain.Gems;

namespace Infrastructure.Gems;

internal static class GemRarities
{
    public static Dictionary<GemCategory, Dictionary<PrimaryGemEffect, double>> Primaries = new Dictionary<GemCategory, Dictionary<PrimaryGemEffect, double>>()
    {
        [GemCategory.SlotMachine] = new()
        {
            [PrimaryGemEffect.Bloodtinge] = 0.01d,
            [PrimaryGemEffect.Warm] = 0.01d,
        },
        
        [GemCategory.Physical] = new()
        {
            [PrimaryGemEffect.Tempering] = 0.9d,
            [PrimaryGemEffect.AdeptBlunt] = 0.05d,
            [PrimaryGemEffect.AdeptThrust] = 0.05d,
            [PrimaryGemEffect.Fools] = 0.01d,
        },
        
        [GemCategory.Radiant] = new()
        {
            [PrimaryGemEffect.Radiant] = 0.03d,
        },
        
        [GemCategory.Striking] = new ()
        {
            [PrimaryGemEffect.Striking] = 0.05d
        },
        
        [GemCategory.OpenFoes] = new ()
        {
            [PrimaryGemEffect.Heavy] = 0.03d,
            [PrimaryGemEffect.Sharp] = 0.03d,
            [PrimaryGemEffect.Poormans] = 0.01d,
        },
        
        [GemCategory.PoisonsHunter] = new ()
        {
            [PrimaryGemEffect.Tempering] = 0.75d,
            [PrimaryGemEffect.Dirty] = 0.03d,
            [PrimaryGemEffect.Murky] = 0.03d,
            [PrimaryGemEffect.Heavy] = 0.03d,
            [PrimaryGemEffect.Sharp] = 0.03d,
            [PrimaryGemEffect.Kinhunters] = 0.01d,
            [PrimaryGemEffect.Beasthunters] = 0.01d,
        },
        
        [GemCategory.PhysicalUpNearDeath] = new ()
        {
            [PrimaryGemEffect.Poormans] = 0.01d,
        },
        
        [GemCategory.FireBolt] = new ()
        {
            [PrimaryGemEffect.Tempering] = 0.3d,
            [PrimaryGemEffect.Bolt] = 0.2d,
            [PrimaryGemEffect.Fire] = 0.4d,
            [PrimaryGemEffect.Dirty] = 0.03d,
            [PrimaryGemEffect.Murky] = 0.03d,
            [PrimaryGemEffect.Beasthunters] = 0.01d,
            [PrimaryGemEffect.Kinhunters] = 0.01d,
        },
        
        [GemCategory.Pulsing] = new ()
        {
            [PrimaryGemEffect.Pulsing] = 0.03d,
        },
        
        [GemCategory.ArcaneNourishing] = new ()
        {
            [PrimaryGemEffect.Tempering] = 0.5d,
            [PrimaryGemEffect.Arcane] = 0.35d,
            [PrimaryGemEffect.Bolt] = 0.05d,
            [PrimaryGemEffect.Fire] = 0.05d,
            [PrimaryGemEffect.Cold] = 0.01d,
            [PrimaryGemEffect.Nourishing] = 0.01d,
            [PrimaryGemEffect.FoolsNourishing] = 0.01d,
            [PrimaryGemEffect.PoormansNourishing] = 0.01d,
        },
    };
    
    public static Dictionary<GemCategory, Dictionary<SecondaryGemEffect, double>> Secondaries = new Dictionary<GemCategory, Dictionary<SecondaryGemEffect, double>>()
    {
        [GemCategory.SlotMachine] = new()
        {
            [SecondaryGemEffect.Bloodtinge] = 0.01d,
        },
        
        [GemCategory.Physical] = new()
        {
            [SecondaryGemEffect.Tempering] = 0.1d,
            [SecondaryGemEffect.Fools] = 0.01d,
        },
        
        [GemCategory.Radiant] = new()
        {
            [SecondaryGemEffect.Dense] = 0.03d,
            [SecondaryGemEffect.Radiant] = 0.03d,
        },
        
        [GemCategory.Striking] = new ()
        {
            [SecondaryGemEffect.Striking] = 0.03d,
        },
        
        [GemCategory.OpenFoes] = new ()
        {
            [SecondaryGemEffect.Finestrike] = 0.03d,
            [SecondaryGemEffect.Poormans] = 0.01d,
        },
        
        [GemCategory.PoisonsHunter] = new ()
        {
            [SecondaryGemEffect.Tempering] = 0.1d,
            [SecondaryGemEffect.Murky] = 0.03d,
            [SecondaryGemEffect.Dirty] = 0.03d,
            [SecondaryGemEffect.Beasthunters] = 0.01d,
            [SecondaryGemEffect.Kinhunters] = 0.01d,
        },
        
        [GemCategory.PhysicalUpNearDeath] = new ()
        {
            [SecondaryGemEffect.Poormans] = 0.01d,
        },
        
        [GemCategory.FireBolt] = new ()
        {
            [SecondaryGemEffect.Lethal] = 0.05d,
            [SecondaryGemEffect.Bolt] = 0.05d,
            [SecondaryGemEffect.Fire] = 0.05d,
            [SecondaryGemEffect.Dirty] = 0.03d,
            [SecondaryGemEffect.Murky] = 0.03d,
            [SecondaryGemEffect.Beasthunters] = 0.01d,
            [SecondaryGemEffect.Kinhunters] = 0.01d,
            [SecondaryGemEffect.Pulsing] = 0.03d,
        },
        
        [GemCategory.ArcaneNourishing] = new ()
        {
            [SecondaryGemEffect.Arcane] = 0.01d,
            [SecondaryGemEffect.FoolsNourishing] = 0.01d,
            [SecondaryGemEffect.PoormansNourishing] = 0.01d,
        },
    };
}