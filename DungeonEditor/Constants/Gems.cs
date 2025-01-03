using System.ComponentModel;
using DungeonEditor.Attributes;
using DungeonEditor.Enumerations;

namespace DungeonEditor.Constants;

public static class Gems
{
    public static class Primaries
    {
        [Name("Blunt ATK UP %", "Adept - Thrust ATK UP %")]
        public static readonly Dictionary<GemCategory, double> Adept = new()
        {
            [GemCategory.Physical] = 0.05
        };
        
        [Name("Phys. UP at full HP %")]
        public static readonly Dictionary<GemCategory, double> FoolsPhysical = new()
        {
            [GemCategory.Physical] = 0.01
        };

        [Name("Arcane ATK UP %")]
        public static readonly Dictionary<GemCategory, double> Arcane = new()
        {
            [GemCategory.ArcaneNourishing] = 0.35
        };
        
        [Name("Arcane scaling", "ATK UP at full HP %", "ATK UP %", "ATK UP near death %")]
        public static readonly Dictionary<GemCategory, double> ArcaneScalingNourishing = new()
        {
            [GemCategory.ArcaneNourishing] = 0.01
        };

        [Name("Add rapid poison effect", "Add slow poison effect")]
        public static readonly Dictionary<GemCategory, double> Poison = new()
        {
            [GemCategory.PoisonsHunter] = 0.03,
            [GemCategory.FireBolt] = 0.03
        };

        [Name("ATK vs beasts UP %", "ATK vs the kin UP %")]
        public static readonly Dictionary<GemCategory, double> Hunter = new()
        {
            [GemCategory.PoisonsHunter] = 0.01,
            [GemCategory.FireBolt] = 0.01
        };

        [Name("Blood ATK UP %", "Bloodtinge scaling")]
        public static readonly Dictionary<GemCategory, double> Bloodtinge = new()
        {
            [GemCategory.Random] = 0.01
        };
        
        [Name("Bolt ATK UP %")]
        public static readonly Dictionary<GemCategory, double> Bolt = new()
        {
            [GemCategory.FireBolt] = 0.2,
            [GemCategory.ArcaneNourishing] = 0.05
        };
        
        [Name("Fire ATK UP %")]
        public static readonly Dictionary<GemCategory, double> Fire = new()
        {
            [GemCategory.FireBolt] = 0.4,
            [GemCategory.ArcaneNourishing] = 0.05
        };


        [Name("STR scaling", "SKL scaling")]
        public static readonly Dictionary<GemCategory, double> HeavySharp = new()
        {
            [GemCategory.OpenFoes] = 0.03,
            [GemCategory.PoisonsHunter] = 0.03
        };


        [Name("Phys. UP near death %")]
        public static readonly Dictionary<GemCategory, double> PoormansPhysical = new()
        {
            [GemCategory.OpenFoes] = 0.01,
            [GemCategory.PhysicalUpNearDeath] = 0.01
        };

        [Name("HP continues to recover")]
        public static readonly Dictionary<GemCategory, double> Pulsing = new()
        {
            [GemCategory.Pulsing] = 0.03
        };

        [Name("Reduces stamina costs")]
        public static readonly Dictionary<GemCategory, double> Radiant = new()
        {
            [GemCategory.Radiant] = 0.03
        };

        [Name("Charge ATKs UP %")]
        public static readonly Dictionary<GemCategory, double> Striking = new()
        {
            [GemCategory.Striking] = 0.05
        };

        [Name("Physical ATK UP %")]
        public static readonly Dictionary<GemCategory, double> Tempering = new()
        {
            [GemCategory.Physical] = 0.9,
            [GemCategory.PoisonsHunter] = 0.75,
            [GemCategory.FireBolt] = 0.3,
            [GemCategory.ArcaneNourishing] = 0.5
        };
    }
    
    public static class Secondaries
    {
        [Name("Add arcane ATK", "ATK UP at full HP %", "ATK UP near death %")]
        public static readonly Dictionary<GemCategory, double> ArcaneNourishing = new()
        {
            [GemCategory.ArcaneNourishing] = 0.01
        };

        [Name("Add blood ATK")]
        public static readonly Dictionary<GemCategory, double> OddBloodtinge = new()
        {
            [GemCategory.Random] = 0.01
        };

        [Name("Add bolt ATK", "Add fire ATK", "Boosts rally potential")]
        public static readonly Dictionary<GemCategory, double> BoltFire = new()
        {
            [GemCategory.FireBolt] = 0.05
        };

        [Name("HP continues to recover")]
        public static readonly Dictionary<GemCategory, double> Pulsing = new()
        {
            [GemCategory.FireBolt] = 0.03
        };

        [Name("Add physical ATK")]
        public static readonly Dictionary<GemCategory, double> OddPhysical = new()
        {
            [GemCategory.Physical] = 0.1,
            [GemCategory.PoisonsHunter] = 0.1
        };
        
        [Name("ATK vs beasts UP %", "ATK vs the kin UP %")]
        public static readonly Dictionary<GemCategory, double> Hunter = new()
        {
            [GemCategory.PoisonsHunter] = 0.01,
            [GemCategory.FireBolt] = 0.01
        };
        
        [Name("Add rapid poison effect", "Add slow poison effect")]
        public static readonly Dictionary<GemCategory, double> Poison = new()
        {
            [GemCategory.PoisonsHunter] = 0.03,
            [GemCategory.FireBolt] = 0.03
        };
        
        [Name("ATK vs open foes UP %")]
        public static readonly Dictionary<GemCategory, double> OpenFoes = new()
        {
            [GemCategory.OpenFoes] = 0.03,
        };
        
        [Name("Phys. UP at full HP %")]
        public static readonly Dictionary<GemCategory, double> FoolsPhysical = new()
        {
            [GemCategory.Physical] = 0.01,
        };

        [Name("WPN durability UP", "Reduces stamina costs")]
        public static readonly Dictionary<GemCategory, double> DenseRadiant = new()
        {
            [GemCategory.Radiant] = 0.03
        };
        
        [Name("Phys. UP near death %")]
        public static readonly Dictionary<GemCategory, double> PoormansPhysical = new()
        {
            [GemCategory.OpenFoes] = 0.01,
            [GemCategory.PhysicalUpNearDeath] = 0.01,
        };
        
        [Name("Charge ATKs UP %")]
        public static readonly Dictionary<GemCategory, double> Striking = new()
        {
            [GemCategory.Striking] = 0.03
        };
    }
}