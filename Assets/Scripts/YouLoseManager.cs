using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLoseManager : MonoBehaviour
{
    public string level1SceneName = "Level1"; // Set this in the Inspector

    public void PlayAgain()
    {
        SceneManager.LoadScene(level1SceneName);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Or whatever your main menu scene name is
    }
}