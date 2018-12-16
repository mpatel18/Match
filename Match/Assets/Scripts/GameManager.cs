using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking; 

public class GameManager : MonoBehaviour {

    public static GameManager instance; 
    public GameSetting gameSetting;


    private void Awake()
    {
        instance = this;
    }

    private const string PLAYER_ID_PREFIX = "Player"; 

    private static Dictionary<string, Player> playerList = new Dictionary<string, Player>(); 

    public static void RegisterPlayer(string netID, Player player)
    {
        string playerID = PLAYER_ID_PREFIX + " " + netID;
        playerList.Add(playerID, player);
        player.transform.name = playerID; 
    }

    public static void DeRegisterPlayer(string playerID)
    {
        playerList.Remove(playerID); 
    }

    public static Player GetPlayer(string playerID)
    {
        return playerList[playerID];
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical(); 

        foreach(string playerListKey in playerList.Keys)
        {
            GUILayout.Label(playerListKey + " - " + playerList[playerListKey].transform.name + " health is " + playerList[playerListKey].currentHealth); 
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    } 
}
