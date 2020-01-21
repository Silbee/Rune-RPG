using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadSceneAsync(3);

    public void QuitGame() => Application.Quit();

    public void OpenOptions() { }
}
