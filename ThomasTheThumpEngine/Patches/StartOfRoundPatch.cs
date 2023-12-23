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
            try
            {
                string path = Path.Combine(Paths.PluginPath, "LineLoad-ThomasTheThumpEngine");
                ThomasTheme = SoundTool.GetAudioClip(path, "Thomas_the_Tank_Engine_Theme.ogg");
                ThumperThomasBase.Instance.logger.LogInfo("Chase theme loaded successfully!");
            }
            catch (Exception e)
            {
                ThumperThomasBase.Instance.logger.LogWarning(e);
            }
        }
    }
}
