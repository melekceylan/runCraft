using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    private int currentHealth;
    
    
    // UI elementleri
    public GameObject gameOverText;
    public GameObject tryAgainButton;
    public TextMeshProUGUI healthText; //sağlık göstergesi için uı text

    void Start()
    {
        currentHealth = 1;
        // UI text'i bulamazsa otomatik bul
        if (healthText == null)
        {
            healthText = GameObject.FindGameObjectWithTag("HealthText")?.GetComponent<TextMeshProUGUI>();
        }

        // UI elementlerini başlangıçta gizle
        if (gameOverText != null) gameOverText.SetActive(false);
        if (tryAgainButton != null) tryAgainButton.SetActive(false);
        UpdateHealthUI();

    }

    // Hasar alma fonksiyonu
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        
        if (currentHealth <= 0)
        {
            Die(); // Ölüm işlemini tetikle
        }
    }

    // Can artırma fonksiyonu
    public void AddHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthUI();
        // Health 4 olduğunda level complete
        if (currentHealth >= 4)
        {
            LevelManager.Instance.ShowLevelComplete();
            GetComponent<PlayerController>().enabled = false;
        }
    }

    // UI'ı güncelle
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        
        // Game Over UI'ı göster
        if (gameOverText != null) gameOverText.SetActive(true);
        if (tryAgainButton != null) tryAgainButton.SetActive(true);
        
        // Oyun durumu ayarları
        Time.timeScale = 0; // Oyunu durdur
        
        // Player'ı devre dışı bırak ama yok etme
        GetComponent<PlayerController>().enabled = false;
    }

    // Mevcut canı döndüren fonksiyon
    public int GetCurrentHealth()
    {
    return currentHealth;
    }

    
}


