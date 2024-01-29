using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //UI buttons
    [SerializeField] private Button playButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    //Game score
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    //check start/over
    public bool isGameStarted = false;
    public bool isGameOver = false;
    public bool cursorOnUI = false;//not spawn obj while coursor on buttons
    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        Physics.gravity = new Vector3(0f, -40f);
        playButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);
    }


    private void FixedUpdate()
    {
        if (isGameOver)
        {
            Time.timeScale = 0;
        }
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = $"Score: {score}";
    }

    private void StartGame()
    {
        playButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        spawnManager.ShowNextObj();
        isGameStarted = true;
        Debug.Log("Game started");
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Debug.Log("Game restarted");
    }

    private void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
