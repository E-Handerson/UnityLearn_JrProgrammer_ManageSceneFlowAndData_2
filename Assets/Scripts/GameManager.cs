using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Information")]
    [SerializeField] private TextMeshProUGUI _scoreText;

    // Player information and high score list
    public Player CurrentPlayer;
    public List<Player> HighScorePlayerList = new List<Player>();

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
    }

    private void Start()
    {
        // Load the high score list from the save data and display it
        SaveData.LoadHighScoreList();
    }

    /// <summary>
    /// Create the player based on the name entered in the field
    /// </summary>
    /// <param name="name">Name passed from the user input field in the main menu</param>
    /// <remarks>Set up the player name and score, updates the name each round and resets the score</remarks>
    public void CreatePlayer(string name, int score)
    {
        if (name != string.Empty)
        {
            CurrentPlayer.PlayerName = name;
            CurrentPlayer.Score = score;
            Debug.Log($"Current Player: {CurrentPlayer.PlayerName} Current Score: {CurrentPlayer.Score}");
        }
    }

    /// <summary>
    /// Add new high score to the list if the current player's score is higher than the lowest score in the list, then save the updated list
    /// </summary>
    public void AddHighScore()
    {
        // Create a new player object with the current player's name and score
        Player newHighScorePlayer = new Player();
        newHighScorePlayer.PlayerName = CurrentPlayer.PlayerName;
        newHighScorePlayer.Score = CurrentPlayer.Score;

        // Check if the new score is higher than the lowest score in the list, and if so, add it to the list and sort it
        if (newHighScorePlayer.Score > HighScorePlayerList[HighScorePlayerList.Count - 1].Score)
        {
            HighScorePlayerList.Add(newHighScorePlayer);
            HighScorePlayerList.Sort((x, y) => y.Score.CompareTo(x.Score));
            // If the list has more than 5 scores, remove the lowest score
            if (HighScorePlayerList.Count > 5)
            {
                HighScorePlayerList.RemoveAt(HighScorePlayerList.Count - 1);
            }
        }
        // Save the updated high score list
        SaveData.SaveHighScoreList();
    }
}

