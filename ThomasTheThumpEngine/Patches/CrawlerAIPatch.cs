using UnityEngine;
using HarmonyLib;
using LCSoundTool;


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
                ___creatureVoice.PlayOneShot(ThumperThomasBase.ThomasTheme);
            }
        }
    }
}