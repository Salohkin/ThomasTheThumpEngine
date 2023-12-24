using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using ThomasTheThumpEngine.Patches;
using UnityEngine;

namespace ThomasTheThumpEngine
{
    [BepInPlugin(pluginGUID, pluginName, pluginVersion)]
    public class ThumperThomasBase : BaseUnityPlugin
    {
        private const string pluginGUID = "LineLoad.ThomasTheThumpEngine";
        private const string pluginName = "Thomas The Thump Engine";
        private const string pluginVersion = "1.0.3";

        private readonly Harmony harmony = new Harmony(pluginGUID);

        public static ThumperThomasBase Instance;

        internal ManualLogSource logger;

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
            harmony.PatchAll(typeof(CrawlerAIPatch));
            harmony.PatchAll(typeof(EnemyAIPatch));
        }
    }
}
