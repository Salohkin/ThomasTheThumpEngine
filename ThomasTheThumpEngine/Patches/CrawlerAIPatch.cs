using UnityEngine;
using HarmonyLib;

namespace ThomasTheThumpEngine.Patches
{
    [HarmonyPatch(typeof(CrawlerAI))]
    internal class CrawlerAIPatch : MonoBehaviour
    {
        [HarmonyPatch("BeginChasingPlayerClientRpc")]
        [HarmonyPostfix]
        static void PlayTheme(CrawlerAI __instance, ref bool ___hasEnteredChaseMode) //ref int ___currentBehaviourStateIndex, ref bool ___hasEnteredChaseMode, ref AudioSource ___creatureVoice
        {
            if (__instance.currentBehaviourStateIndex == 1 && !___hasEnteredChaseMode)
            {
                __instance.creatureVoice.PlayOneShot(ThumperThomasBase.thomasTheme);
                ThumperThomasBase.Instance.logger.LogDebug("Chase theme started!");
            }
        }
    }
}
