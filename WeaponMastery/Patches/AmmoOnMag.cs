using System.Reflection;
using SPT.Reflection.Patching;
using Random = UnityEngine.Random;

namespace WeaponMastery.Patches
{
    public class SetAmmoOnMagPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(FirearmsAnimator).GetMethod("SetAmmoOnMag", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        private static void PatchPostfix(FirearmsAnimator __instance, int count)
        {
            if (count > 0)
            {
                var fullmag = count;
                float randomAmmoOnMag = Random.Range(1, fullmag / 2);
                WeaponAnimationSpeedControllerClass.SetAmmoInMag(__instance.Animator, randomAmmoOnMag);
                //Logger.LogInfo($"[Patch] Random AmmoInMag = {randomAmmoOnMag}");
            }
            //Logger.LogInfo($"[SetAmmoOnMag] count = {count}");
        }
    }
}
