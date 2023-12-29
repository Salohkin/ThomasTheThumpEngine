using HarmonyLib;
using UnityEngine;

namespace ThomasTheThumpEngine.Patches
{
    [HarmonyPatch(typeof(EnemyAI))]
    internal class EnemyAIPatch : MonoBehaviour
    {
        [HarmonyPatch("KillEnemyClientRpc")]
        [HarmonyPrefix] // prefix so the creature isn't already destroyed when this runs (postfix might work, but haven't tested it)
        static void StopThemeOnDeath(EnemyAI __instance)
        {
            if (isEnemy(__instance, "crawler"))
            {
                StopTheme(__instance);
            }
        }

        [HarmonyPatch("SwitchToBehaviourStateOnLocalClient")]
        [HarmonyPostfix]
        static void StopThemeOnPassiveBehaviourState(EnemyAI __instance)
        {
            if (__instance.currentBehaviourStateIndex == 0 && isEnemy(__instance, "crawler"))
            {
                StopTheme(__instance);
            }
        }

        static void StopTheme(EnemyAI __instance)
        {
            __instance.creatureVoice.Stop();
            ThumperThomasBase.Instance.logger.LogDebug("Chase theme stopped!");
        }

        static bool isEnemy(EnemyAI enemy, string enemyName)
        {
            return (enemy.enemyType.enemyName.ToLower() == enemyName.ToLower());
        }
    }
}
