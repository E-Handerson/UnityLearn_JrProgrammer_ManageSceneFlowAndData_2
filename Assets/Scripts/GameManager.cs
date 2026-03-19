using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Information")]
    [SerializeField] private Button _loadPlayer;
    [SerializeField] private TextMeshProUGUI _scoreText;


    public Player CurrentPlayer;
    public PlayerList HighScorePlayerList;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        // Check if there is already an instance and destroy it if there is 
        if (Instance != null)
        {
            Debug.LogWarning("Previous instance found");
            Destroy(gameObject);
            return;
        }

        // Create a new instance and set it to not be destroyed
        Instance = this;
        DontDestroyOnLoad(gameObject);

       
        LoadHighScores();
    }

    /// <summary>
    /// Create the player based on the name entered in the field
    /// </summary>
    /// <param name="name">Name passed from the user input field in the main menu</param>
    /// <remarks>Set up the player name and score, updates the name each round and resets the score</remarks>
    public void CreatePlayer(string name)
    {
        CurrentPlayer.PlayerName = name;
        CurrentPlayer.Score = 0;
        Debug.Log($"Current Player: {CurrentPlayer.PlayerName} Current Score: {CurrentPlayer.Score}");
    }
    
    /// <summary>
    /// Look for a file holding the top score and load it if found, or return a new player
    /// </summary>
    public void LoadHighScores()
    {
        // Load the high score list
        HighScorePlayerList = SaveData.ReadHighScoreList();
        if(HighScorePlayerList == null )
        {
            Debug.Log("no players found");
        }
        else

        {
            // Set the Best Score with information provided
            _scoreText.text = $"Best Score : {HighScorePlayerList.Players[0].PlayerName} : {HighScorePlayerList.Players[0].Score}";
        }
    }

}
