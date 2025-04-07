using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string level1SceneName = "level1"; // Make this public so you can set it in the Inspector

    public void StartGame()
    {
        if (string.IsNullOrEmpty(level1SceneName))
        {
            Debug.LogError("level 1 Scene Name is not set in the Inspector!");
            return; // Stop the function if the scene name is not set
        }

        SceneManager.LoadScene(level1SceneName);
    }
}
