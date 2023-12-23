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
                ThumperThomasBase.ThomasTheme.name = "Thomas_the_Tank_Engine_Theme";
                ThumperThomasBase.ThomasTheme = SoundTool.GetAudioClip(path, ThumperThomasBase.ThomasTheme.name + ".ogg");
                ThumperThomasBase.Instance.logger.LogInfo("Thomas the Tank Engine theme loaded successfully!");
            }
            catch (Exception e)
            {
                ThumperThomasBase.Instance.logger.LogWarning(e);
            }
        }
    }
}
