using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string gameSceneName = "Game"; 

    void Awake()
    {
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
