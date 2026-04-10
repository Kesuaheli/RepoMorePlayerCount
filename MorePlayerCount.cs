using BepInEx;
using HarmonyLib;
using Photon.Pun;
using UnityEngine.SceneManagement;

[BepInPlugin(modGUID, modName, modVersion)]
public class MorePlayerCount : BaseUnityPlugin
{
    const string modGUID = "de.kesuaheli.moreplayercount";
    const string modName = "MorePlayerCount";
    const string modVersion = "0.0.1";
    private void Awake()
    {
        var harmony = new Harmony(modGUID);
        harmony.PatchAll();

        SceneManager.sceneLoaded += OnSceneLoaded;

        Logger.LogInfo($"Mod Loaded!");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!SemiFunc.RunIsShop()) return;

        int playerCount = SemiFunc.IsMultiplayer() ? PhotonNetwork.CurrentRoom.PlayerCount : 1;
        int mapPlayerCountUpgradeCount = playerCount / 2;
        StatsManager.instance.itemDictionary.GetValueSafe("Item Upgrade Map Player Count").maxAmount = mapPlayerCountUpgradeCount;
        StatsManager.instance.itemDictionary.GetValueSafe("Item Upgrade Map Player Count").maxPurchaseAmount = mapPlayerCountUpgradeCount;
    }
}
