using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanelGUI;
    [SerializeField] private GameObject _helpPanelGUI;

    void Start()  {   }

    public void Stop()
    {
        Time.timeScale = 0;
        _pausePanelGUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _pausePanelGUI.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Anwendung geschlossen");
        Application.Quit();
    }

    public void Help(bool newState)
    {
        _helpPanelGUI.SetActive(newState);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!_pausePanelGUI.activeSelf)
                Stop();
            else
                Resume();
        }
    }
}
