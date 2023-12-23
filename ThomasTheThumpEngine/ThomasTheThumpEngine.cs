using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LCSoundTool;
using System.IO;
using System;
using ThomasTheThumpEngine.Patches;
using UnityEngine;

namespace ThomasTheThumpEngine
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
    public class ThumperThomasBase : BaseUnityPlugin
    {
        private const string pluginGUID = "LineLoad.ThomasTheThumpEngine";
        private const string pluginName = "Thomas The Thump Engine";
        private const string pluginVersion = "1.0.1";

        private readonly Harmony harmony = new Harmony(pluginGUID);

        public static ThumperThomasBase Instance;

        internal ManualLogSource logger;

        public static AudioClip ThomasTheme;

        private bool clipsSent = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            logger = BepInEx.Logging.Logger.CreateLogSource(pluginGUID);
            logger.LogInfo($"Plugin {pluginGUID} is loaded!");

            harmony.PatchAll(typeof(ThumperThomasBase));
            harmony.PatchAll(typeof(StartOfRoundPatch));
            //harmony.PatchAll(typeof(StartMatchLevelPatch));
            harmony.PatchAll(typeof(CrawlerAIPatch));
        }

        private void Start()
        {
            if (!SoundTool.networkingAvailable)
            {
                logger.LogWarning("LCSoundTool networking not enabled! This mod will not work fully and might run into problems.");
            }

            try
            {
                string path = Path.Combine(Paths.PluginPath, "LineLoad-ThomasTheThumpEngine");
                ThomasTheme.name = "Thomas_the_Tank_Engine_Theme";
                ThomasTheme = SoundTool.GetAudioClip(path, ThomasTheme.name + ".ogg");
                Instance.logger.LogInfo("Thomas the Tank Engine theme loaded successfully!");
            }
            catch (Exception e)
            {
                Instance.logger.LogWarning(e);
            }
        }

        private void Update()
        {
            if (!clipsSent && SoundTool.networkingAvailable && SoundTool.networkingInitialized)
            {
                SoundTool.SendNetworkedAudioClip(ThomasTheme);
                clipsSent = true;
                Instance.logger.LogInfo("Sending audio clips");
            }
            else if (ThomasTheme == null)
            {
                SoundTool.SyncNetworkedAudioClips();
            }
        }
    }
}
