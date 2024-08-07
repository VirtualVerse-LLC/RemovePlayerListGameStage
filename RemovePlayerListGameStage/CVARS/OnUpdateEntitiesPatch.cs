using HarmonyLib;

[HarmonyPatch(typeof(NetEntityDistribution), nameof(NetEntityDistribution.OnUpdateEntities))]
public class OnUpdateEntitiesPatch
{
    public static void Postfix(NetEntityDistribution __instance)
    {
        for (int i = 0; i < __instance.playerList.Count; i++)
        {
            EntityPlayer player = __instance.playerList[i];
            player.Buffs.SetCustomVarNetwork("mGameStage", (float)player.PartyGameStage);
            player.Buffs.SetCustomVarNetwork("mLootStage", (float)player.GetHighestPartyLootStage(0f, 0f));
        }
    }
}

