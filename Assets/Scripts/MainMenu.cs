using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [Header("UI Components")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _highScores;
    [SerializeField] private TMP_InputField _userName;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    private void Start()
    {
        // Check if there is a registered player and display the name, otherwise log that there is no registered player
        if (GameManager.Instance.CurrentPlayer.PlayerName == string.Empty)
        {
            Debug.Log("No registered player found");           
        }
        else
        {
            // If there is a registered player, display the name in the input field
            _userName.text = GameManager.Instance.CurrentPlayer.PlayerName;
        }

        // Display the best score on load
        LoadBestPlayer();
    }

    /// <summary>
    /// Load the best player score after each new high score is added to the list
    /// </summary>
    public void LoadBestPlayer()
    {
        // If there are no high scores in the list, display "Best Score: None", otherwise display the name and score of the best player
        if (GameManager.Instance.HighScorePlayerList == null)
        {
            _bestScoreText.text = "Best Score: None";
            
        }
        else
        {
            _bestScoreText.text = $"Best Score: {GameManager.Instance.HighScorePlayerList[0].PlayerName} - {GameManager.Instance.HighScorePlayerList[0].Score}";
        }
 
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    public void StartGame()
    {       
        Debug.Log("new game requested");
        SceneManager.LoadScene("main");
    }

    /// <summary>
    /// Load the high score screen
    /// </summary>
    public void HighScores()
    {
        Debug.Log("High scores requested");
        ChangeScene("HighScores");
    }

    /// <summary>
    /// Create the player then Load the requested scene
    /// </summary>
    /// <param name="sceneName">The scene name to load</param>
    private void ChangeScene(string sceneName)
    {
        // If the player name is empty, log a warning and use a default name, then create the player and load the requested scene
        if (_userName.text == string.Empty)
        {
            Debug.LogWarning("Player name is empty, using default name");
            _userName.text = "Player";
        }
        GameManager.Instance.CreatePlayer(_userName.text, 0);
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    /// <remarks>Currently configured to exit from the editor, not a published game</remarks>
    public void ExitGame()
    {
        SaveData.SaveHighScoreList();
        EditorApplication.ExitPlaymode();
    }
}

