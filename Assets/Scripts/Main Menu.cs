using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject howToPlayCanvas;

    private void Start()
    {
        mainMenuCanvas.SetActive(true);
        howToPlayCanvas.SetActive(false);
        AudioManager.instance.PlayMainMenuSound();
    }

    public void PlayGame()
    {
        AudioManager.instance.Destroy();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowHowToPlay()
    {
        mainMenuCanvas.SetActive(false);
        howToPlayCanvas.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        howToPlayCanvas.SetActive(false);
    }
}
