using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject levelCompleteText;
    public GameObject nextLevelButton;
    public GameObject gameOverText;
    public GameObject tryAgainButton;
    public TextMeshProUGUI levelCompleteTextComponent;
    public TextMeshProUGUI nextLevelButtonText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        HideCompletionUI();
    }

    public void ShowLevelComplete()
    {
        if (levelCompleteText != null)
        {
            levelCompleteText.SetActive(true);
            if (IsLastLevel())
            {
                levelCompleteTextComponent.text = "Congratulations!\nYou have completed the game!";
            }
            else
            {
                levelCompleteTextComponent.text = $"Congratulations!\nYou have completed level {GetCurrentLevelNumber()}!";
            }
        }

        if (nextLevelButton != null)
        {
            if (!IsLastLevel())
            {
                nextLevelButton.SetActive(true);
                nextLevelButtonText.text = $"Continue with level {GetCurrentLevelNumber() + 1}";
            }
        }

        // Oyunu durdur
        Time.timeScale = 0;
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void ShowGameOver()
    {
        if (gameOverText != null) gameOverText.SetActive(true);
        if (tryAgainButton != null) tryAgainButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        Debug.Log("RestartLevel called!"); // Debug için
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HideCompletionUI()
    {
        if (levelCompleteText != null) levelCompleteText.SetActive(false);
        if (nextLevelButton != null) nextLevelButton.SetActive(false);
         if (gameOverText != null) gameOverText.SetActive(false);
        if (tryAgainButton != null) tryAgainButton.SetActive(false);
    }

    private int GetCurrentLevelNumber()
    {
        string currentSceneName = SceneManager.GetActiveScene().name.ToLower();
        if (currentSceneName.StartsWith("level"))
        {
            string numberPart = currentSceneName.Replace("level", "");
            if (int.TryParse(numberPart, out int levelNumber))
            {
                return levelNumber;
            }
        }
        return 1;
    }

    private bool IsLastLevel()
    {
        return SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1;
    }
}