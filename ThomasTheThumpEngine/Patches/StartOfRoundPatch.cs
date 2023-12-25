using BepInEx;
using HarmonyLib;
using LCSoundTool;
using System;
using System.IO;

namespace ThomasTheThumpEngine.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void LoadTheme()
        {
            try
            {
                string path = Path.Combine(Paths.PluginPath, "LineLoad-ThomasTheThumpEngine");
                ThumperThomasBase.thomasTheme = SoundTool.GetAudioClip(path, "Thomas_the_Tank_Engine_Theme.ogg");
                ThumperThomasBase.Instance.logger.LogInfo("Chase theme loaded successfully!");
            }
            catch (Exception e)
            {
                ThumperThomasBase.Instance.logger.LogWarning(e);
            }
        }
    }
}
