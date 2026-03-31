using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// Save and load player data
/// </summary>
public static class SaveData 
{    
    /// <summary>
    /// Get the saved information if there is any
    /// </summary>
    /// <returns>Return the player name and player score if there, and null if not there</returns>
    public static void LoadHighScoreList()
    {
        // Clear the current high score player list before loading new data
        GameManager.Instance.HighScorePlayerList.Clear();

        // Construct the file path for the saved player data
        string playerFile = $"{Application.persistentDataPath}/saveFile.json";

        // Check if the file exists at the specified path
        if (File.Exists(playerFile))
        {
            // Read the JSON string from the file
            string jsonPlayer = File.ReadAllText(playerFile);

            // Deserialize the JSON string into a PlayerList object
            PlayerList listOfPlayers = JsonUtility.FromJson<PlayerList>(jsonPlayer);

            // Add each player from the deserialized PlayerList to the high score player list in the GameManager
            foreach (Player player in listOfPlayers.Players)
            {
               GameManager.Instance.HighScorePlayerList.Add(player);
            }
        }
    }

    /// <summary>
    /// Write the player high score to a file
    /// </summary>
    public static void SaveHighScoreList()
    {
        // Create a new PlayerList object and assign the current high score player list to it
        PlayerList listOfPlayers = new PlayerList();
        listOfPlayers.Players = GameManager.Instance.HighScorePlayerList;

        // Serialize the PlayerList object to JSON format
        string jsonPlayerList = JsonUtility.ToJson(listOfPlayers);

        // Write the JSON string to a file in the persistent data path
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", jsonPlayerList);
    }
}
