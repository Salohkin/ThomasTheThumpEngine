using HarmonyLib;
using LCSoundTool;

namespace ThomasTheThumpEngine.Patches
{
    [HarmonyPatch(typeof(StartMatchLever))]
    internal class StartMatchLevelPatch
    {
        [HarmonyPatch("StartGame")]
        [HarmonyPostfix]
        static void NetworkThomasTheme()
        {
            return;
        }
    }
}
