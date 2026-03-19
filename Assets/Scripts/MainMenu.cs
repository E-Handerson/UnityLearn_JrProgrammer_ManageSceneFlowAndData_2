using System.Web;
using TMPro;
using Unity.Multiplayer.PlayMode;
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


    private void Start()
    {
        
       /* if (GameManager.Instance.CurrentPlayer.PlayerName != string.Empty)
        {
            Debug.Log($"The player name is {GameManager.Instance.CurrentPlayer.PlayerName}");
            _userName.text = GameManager.Instance.CurrentPlayer.PlayerName;
        }
        else
        {
            Debug.Log("no player found");
        }*/

        // If there is a high schore list, load the highest score player name to the field
        if(GameManager.Instance.HighScorePlayerList != null)
        {
            _userName.text = GameManager.Instance.HighScorePlayerList.Players[0].PlayerName;
        }
        else
        {
            Debug.Log("No high scores found.");
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
        GameManager.Instance.CreatePlayer(_userName.text);

        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    /// <remarks>Currently configured to exit from the editor, not a published game</remarks>
    public void ExitGame()
    {
        EditorApplication.ExitPlaymode();
    }
}

