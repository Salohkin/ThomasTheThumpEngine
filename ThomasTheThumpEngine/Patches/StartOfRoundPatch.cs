using BepInEx;
using HarmonyLib;
using LCSoundTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ThomasTheThumpEngine.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        public static AudioClip ThomasTheme;

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void LoadTheme()
        {
            string path = Path.Combine(Paths.PluginPath, "ThomasTheThumpEngine");
            ThomasTheme = SoundTool.GetAudioClip(path, "Assets", "Thomas_the_Tank_Engine_Theme.ogg");
            if (ThomasTheme != null)
            {
                ThumperThomasBase.Instance.logger.LogInfo("Chase theme loaded successfully!");
            }
        }
    }
}
