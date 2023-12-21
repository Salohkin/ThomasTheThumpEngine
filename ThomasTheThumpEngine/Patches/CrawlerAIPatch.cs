using UnityEngine;
using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using LCSoundTool;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using LCSoundTool.Resources;
using static ThomasTheThumpEngine.Patches.StartOfRoundPatch;

namespace ThomasTheThumpEngine.Patches
{
    [HarmonyPatch(typeof(CrawlerAI))]
    internal class CrawlerAIPatch : MonoBehaviour
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void PlayThomasTheme(ref int ___currentBehaviourStateIndex, ref bool ___hasEnteredChaseMode, ref AudioSource ___creatureVoice) // ref bool ___lostPlayerInChase
        {
            if (___currentBehaviourStateIndex == 1 && !___hasEnteredChaseMode)
            {
                ___creatureVoice.PlayOneShot(StartOfRoundPatch.ThomasTheme);
            }
        }
    }
}
