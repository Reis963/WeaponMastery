using BepInEx;
using WeaponMastery.Patches;

namespace WeaponMastery
{
    [BepInPlugin("com.reis963.weaponmastery", "Weapon Mastery", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal void Awake()
        {
            // Plugin startup logic
            new SetWeaponLevelPatch().Enable();
        }
    }
}
