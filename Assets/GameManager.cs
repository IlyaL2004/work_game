using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Элементы")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI moneyText;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    [Header("Настройки Экономики")]
    public GroundHealth groundHealth;
    public GameObject zonePrefab;     

    private int score = 0;
    public int money = 0;
    public int winAmount = 15000;
    public int healCost = 5000;
    public int healAmount = 3;

    [Header("Таймер Зоны")]
    public float zoneSpawnTime = 45f;
    private float zoneTimer;

    void Awake()
    {
        Instance = this;
        zoneTimer = zoneSpawnTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TryBuyHeal();
        }

        zoneTimer -= Time.deltaTime;
        if (zoneTimer <= 0f)
        {
            SpawnZone();
            zoneTimer = zoneSpawnTime;
        }
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Спасено: " + score;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = "Энергия: " + money + " / " + winAmount;
        
        if (money >= winAmount)
        {
            TriggerWin();
        }
    }

    void TryBuyHeal()
    {
        if (money >= healCost)
        {
            money -= healCost;
            moneyText.text = "Энергия: " + money + " / " + winAmount;
            
            if (groundHealth != null)
            {
                groundHealth.HealHp(healAmount);
            }
        }
    }

    void SpawnZone()
    {
        Vector3 randomPos = Random.insideUnitSphere * 8f;
        randomPos.y = 1.5f;
        Instantiate(zonePrefab, randomPos, Quaternion.identity);
    }

    public void TriggerGameOver()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);
    }

    public void TriggerWin()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

        public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu"); 
    }
}
