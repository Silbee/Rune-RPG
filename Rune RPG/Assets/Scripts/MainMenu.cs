using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Canvas menuCanvas, optionsCanvas;

    private void Start()
    {
        menuCanvas = GameObject.Find("Main Menu").GetComponent<Canvas>();
        optionsCanvas = GameObject.Find("Options Menu").GetComponent<Canvas>();
    }

    public void StartGame() => SceneManager.LoadSceneAsync(3);

    public void QuitGame() => Application.Quit();

    public void OpenOptions()
    {
        optionsCanvas.enabled = true;
        menuCanvas.enabled = false;
    }

    public void CloseOptions()
    {
        optionsCanvas.enabled = false;
        menuCanvas.enabled = true;
    }
}
