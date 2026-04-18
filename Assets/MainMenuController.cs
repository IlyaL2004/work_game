using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string gameSceneName = "Game"; 

    // Вызывается до того, как что-либо появится на экране
    void Awake()
    {
        // ЖЕЛЕЗОБЕТОННЫЙ ФИКС: Возвращаем время в норму!
        Time.timeScale = 1f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        Debug.Log("КНОПКА ИГРАТЬ НАЖАТА!!! Загружаю сцену: " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("КНОПКА ВЫХОД НАЖАТА!!!");
        Application.Quit();
    }
}