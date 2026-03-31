using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    // Added by coder
    [SerializeField] private Text _bestScore;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        UpdateBestScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
        }
        
        // Watch for the backspace to return to the main menu
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    /// <summary>
    /// Update the best score line
    /// </summary>
    private void UpdateBestScore()
    {
       
        if (GameManager.Instance.HighScorePlayerList != null)
        {
            _bestScore.text = $"Best Score : {GameManager.Instance.HighScorePlayerList[0].PlayerName} : {GameManager.Instance.HighScorePlayerList[0].Score}";
        }
    }

    /// <summary>
    /// When the game is over, update the score if the player has become the new top score
    /// </summary>
    public void GameOver()
    { 
        // Update the current players score
        GameManager.Instance.CurrentPlayer.Score = m_Points;

        // Update the high score list
        GameManager.Instance.AddHighScore();

        // Update the displayed top score
        UpdateBestScore();

        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
