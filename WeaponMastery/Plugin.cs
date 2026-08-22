using BepInEx;
using BepInEx.Configuration;
using WeaponMastery.Patches;

namespace WeaponMastery
{
    [BepInPlugin("com.reis963.weaponmastery", "Weapon Mastery", "1.3.1")]
    [BepInDependency("com.SPT.core", "4.1.3")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ConfigEntry<bool> IsEnabled { get; private set; }
        internal static ConfigEntry<int> MagazineCheckVariationChance { get; private set; }
        internal static ConfigEntry<int> WeaponLevelVariationChance { get; private set; }

        internal void Awake()
        {
            IsEnabled = Config.Bind(
                "General",
                "Enabled",
                true,
                "Enables or disables Weapon Mastery animation randomization.");

            MagazineCheckVariationChance = Config.Bind(
                "Magazine Check",
                "Variation chance",
                100,
                new ConfigDescription(
                    "Chance, from 0 to 100 percent, to use a reduced AmmoInMag value when a magazine check starts. The Animator Controller chooses the resulting animation.",
                    new AcceptableValueRange<int>(0, 100)));

            WeaponLevelVariationChance = Config.Bind(
                "Weapon Mastery",
                "Variation chance",
                100,
                new ConfigDescription(
                    "Chance, from 0 to 100 percent, to replace Tarkov's WeaponLevel with a random level from 0 to 2. The Animator Controller chooses the resulting animation.",
                    new AcceptableValueRange<int>(0, 100)));

            new SetWeaponLevelPatch().Enable();
            new SetAmmoOnMagPatch().Enable();
            new CheckAmmoPatch().Enable();
        }
    }
}
