using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Display the high scores and allow the player to sort the scores by highest or lowest score
/// </summary>
/// <remarks>Player can press the backspace to return to the main menu, or they can press the
/// up and down arrow to change the direction of the sorted high scores </remarks>
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

        // Display lowest score first
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameManager.Instance.HighScorePlayerList.Sort((p1, p2) => p1.Score.CompareTo(p2.Score));
            DisplayHighScores();
        }

        // DIsplay highest score first
        if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameManager.Instance.HighScorePlayerList.Sort((p1, p2) => p2.Score.CompareTo(p1.Score));
            DisplayHighScores();
        }
    }

    /// <summary>
    /// Load the high scores to be displayed
    /// </summary>
    private void DisplayHighScores()
    {
        if (GameManager.Instance.HighScorePlayerList != null)
        {
            for (int i = 0; i < GameManager.Instance.HighScorePlayerList.Count; i++)
            {
                _highScore[i].text = $"{GameManager.Instance.HighScorePlayerList[i].PlayerName}\t{GameManager.Instance.HighScorePlayerList[i].Score}";
            }
        }
    }
}
