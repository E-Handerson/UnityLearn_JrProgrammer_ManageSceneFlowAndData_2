
using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


/// <summary>
/// Save and load player data
/// </summary>
public static class SaveData 
{
    
    /// <summary>
    /// Get the saved information if there is any
    /// </summary>
    /// <returns>Return the player name and player score if there, and null if not there</returns>
    public static PlayerList ReadHighScoreList()
    {
       

        string playerFile = $"{Application.persistentDataPath}/saveFile.json"; 

        if(File.Exists(playerFile))
        {
            string jsonPlayer = File.ReadAllText(playerFile);

            PlayerList listOfPlayers = JsonUtility.FromJson<PlayerList>(jsonPlayer);

            listOfPlayers.Players = listOfPlayers.Players.OrderByDescending(p => p.Score).ToList();

            return listOfPlayers; 
        }
     
        return null;
        
    }

    /// <summary>
    /// Write the player high score to a file
    /// </summary>
    /// <param name="playerName">Use the player name entered</param>
    /// <param name="playerScore">Use the score earned</param>
    public static void SaveBestScore(string playerName, int playerScore)
    {
        Debug.Log(playerName + " " + playerScore);

        Player player = new Player();

        player.PlayerName = playerName;
        player.Score = playerScore;

        string jsonPlayer = JsonUtility.ToJson(player);

        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", jsonPlayer);
    }
}
