using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using SPT.Reflection.Patching;
using Random = UnityEngine.Random;

namespace WeaponMastery.Patches
{
    internal static class MagazineCheckState
    {
        private sealed class AmmoState
        {
            internal int Count;
        }

        private static readonly ConditionalWeakTable<FirearmsAnimator, AmmoState> AmmoStates =
            new ConditionalWeakTable<FirearmsAnimator, AmmoState>();

        internal static void SetAmmoCount(FirearmsAnimator animator, int count)
        {
            AmmoStates.GetOrCreateValue(animator).Count = count;
        }

        internal static bool TryGetAmmoCount(FirearmsAnimator animator, out int count)
        {
            AmmoState state;
            if (AmmoStates.TryGetValue(animator, out state))
            {
                count = state.Count;
                return true;
            }

            count = 0;
            return false;
        }
    }

    public class SetAmmoOnMagPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(FirearmsAnimator).GetMethod(
                nameof(FirearmsAnimator.SetAmmoOnMag),
                BindingFlags.Instance | BindingFlags.Public,
                null,
                new[] { typeof(int) },
                null);
        }

        [PatchPostfix]
        private static void PatchPostfix(FirearmsAnimator __instance, int count)
        {
            MagazineCheckState.SetAmmoCount(__instance, count);
        }
    }

    public class CheckAmmoPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(FirearmsAnimator).GetMethod(
                nameof(FirearmsAnimator.CheckAmmo),
                BindingFlags.Instance | BindingFlags.Public,
                null,
                Type.EmptyTypes,
                null);
        }

        [PatchPrefix]
        private static void PatchPrefix(FirearmsAnimator __instance)
        {
            int actualAmmoCount;
            if (!MagazineCheckState.TryGetAmmoCount(__instance, out actualAmmoCount))
            {
                actualAmmoCount = (int)AnimationControllerParametersTable.GetFloatAmmoInMag(__instance.Animator);
            }

            // A randomized value can otherwise remain until Tarkov updates the magazine again.
            AnimationControllerParametersTable.SetAmmoInMag(__instance.Animator, actualAmmoCount);

            if (!Plugin.IsEnabled.Value ||
                actualAmmoCount <= 1 ||
                Random.Range(0, 100) >= Plugin.MagazineCheckVariationChance.Value)
            {
                return;
            }

            int maximumExclusive = Math.Max(2, actualAmmoCount / 2);
            int randomizedAmmoCount = Random.Range(1, maximumExclusive);
            AnimationControllerParametersTable.SetAmmoInMag(__instance.Animator, randomizedAmmoCount);
        }
    }
}
