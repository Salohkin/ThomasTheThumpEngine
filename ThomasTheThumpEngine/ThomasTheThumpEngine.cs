using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomasTheThumpEngine.Patches;
using static ThomasTheThumpEngine.Patches.StartOfRoundPatch;

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
        }
    }
}
