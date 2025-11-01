using System.Reflection;
using SPT.Reflection.Patching;
using Random = UnityEngine.Random;

namespace WeaponMastery.Patches
{
    public class SetWeaponLevelPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(FirearmsAnimator).GetMethod("SetWeaponLevel", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        private static void PatchPostfix(FirearmsAnimator __instance, float weaponLevel)
        {
            var randomWeaponLevel = Random.Range(0, 3);
            WeaponAnimationSpeedControllerClass.SetWeaponLevel(__instance.Animator, randomWeaponLevel);
            //Logger.LogInfo($"[Patch] Random WeaponLevel = {randomWeaponLevel}");
        }
    }
}
