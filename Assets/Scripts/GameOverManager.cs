using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    
    public string level1SceneName = "level1";   // Inspector'dan ayarlanabilir

    public void RestartGame()
    {
        SceneManager.LoadScene(level1SceneName);
    }

}