using System;
using System.Reflection;
using SPT.Reflection.Patching;
using Random = UnityEngine.Random;

namespace WeaponMastery.Patches
{
    public class SetWeaponLevelPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(FirearmsAnimator).GetMethod(
                nameof(FirearmsAnimator.SetWeaponLevel),
                BindingFlags.Instance | BindingFlags.Public,
                null,
                new[] { typeof(float) },
                null);
        }

        [PatchPrefix]
        private static void PatchPrefix(ref float weaponLevel)
        {
            if (!Plugin.IsEnabled.Value)
            {
                return;
            }

            int nativeLevel = (int)weaponLevel;
            if (nativeLevel < 0 || nativeLevel > 2 || weaponLevel != nativeLevel)
            {
                return;
            }

            if (Random.Range(0, 100) >= Plugin.WeaponLevelVariationChance.Value)
            {
                return;
            }

            weaponLevel = Random.Range(0, 3);
        }
    }
}
