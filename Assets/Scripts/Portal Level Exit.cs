using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("LoadNextLevel", 1.5f);
        }
    }

    void LoadNextLevel()
    {
        ScenePersist.instance.DestroyScenePersist();
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
