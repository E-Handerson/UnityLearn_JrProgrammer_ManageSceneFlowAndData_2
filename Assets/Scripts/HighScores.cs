using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour
{
    [Header("Score List")]
    [SerializeField] private TextMeshProUGUI[] _highScore;


    public void Start()
    {
        // display the high scores on load
        DisplayHighScores();
    }


    private void Update()
    {
        // Return to the main menu
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }

    /// <summary>
    /// Load the high scores to be displayed
    /// </summary>
    private void DisplayHighScores()
    {
        if (GameManager.Instance.HighScorePlayerList != null)
        {
            for (int i = 0; i < GameManager.Instance.HighScorePlayerList.Players.Count; i++)
            {
                _highScore[i].text = $"{GameManager.Instance.HighScorePlayerList.Players[i].PlayerName}\t{GameManager.Instance.HighScorePlayerList.Players[i].Score}";
            }
        }
    }

}
