using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Tutorial()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 0;
    }
}