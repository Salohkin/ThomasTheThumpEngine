using UnityEngine;
using HarmonyLib;

namespace ThomasTheThumpEngine.Patches
{
    [HarmonyPatch(typeof(CrawlerAI))]
    internal class CrawlerAIPatch : MonoBehaviour
    {
        [HarmonyPatch("BeginChasingPlayerClientRpc")]
        [HarmonyPostfix]
        static void PlayThomasTheme(ref int ___currentBehaviourStateIndex, ref bool ___hasEnteredChaseMode, ref AudioSource ___creatureVoice)
        {
            if (___currentBehaviourStateIndex == 1 && !___hasEnteredChaseMode)
            {
                ___creatureVoice.PlayOneShot(StartOfRoundPatch.thomasTheme);
                ThumperThomasBase.Instance.logger.LogInfo("Chase theme started!");
            }
        }
    }

    [HarmonyPatch(typeof(EnemyAI))]
    internal class EnemyAIPatch : MonoBehaviour
    {
        [HarmonyPatch("SwitchToBehaviourStateOnLocalClient")]
        [HarmonyPostfix]
        static void StopThomasTheme(int ___currentBehaviourStateIndex, ref EnemyType ___enemyType, ref AudioSource ___creatureVoice)
        {
            //ThumperThomasBase.Instance.logger.LogInfo("Enemy name is: " + ___enemyType.enemyName);
            if (___currentBehaviourStateIndex == 0 && ___enemyType.enemyName.ToLower() == "crawler")
            {
                ___creatureVoice.Stop();
                ThumperThomasBase.Instance.logger.LogInfo("Chase theme stopped!");
            }
        }
    }
}
