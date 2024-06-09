using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;

    int point = 0;
    public static GameSession instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = $"Score: {point}";
        livesText.text = $"Lives: {playerLives}";
    }

    public void TakeLife()
    {
        playerLives--;
        livesText.text = $"Lives: {playerLives}";

        if (playerLives <= 0)
        {
            ReloadGame();
        }
        else
        {
            Invoke("ReloadScene", 2.5f);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ReloadGame()
    {
        ScenePersist.instance.DestroyScenePersist();
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }

    public void AddPoint()
    {
        point++;
        scoreText.text = $"Score: {point}";
        print("sadasd");
    }

    void ResetScore()
    {
        point = 0;
        scoreText.text = $"Score: {point}";
    }
}
