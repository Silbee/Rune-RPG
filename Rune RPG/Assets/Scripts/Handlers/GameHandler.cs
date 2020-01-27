using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; } // Make this class accessible from any script without all fields and functions having to be static

    [Range(60, 150)]
    public int maxFramerate = 60;

    //void OnEnable() => SceneManager.sceneLoaded += PrintLoadedSceneName;

    //void OnDisable() => SceneManager.sceneLoaded -= PrintLoadedSceneName;

    //void PrintLoadedSceneName(Scene loadedScene, LoadSceneMode mode) => Debug.Log("Loaded scene: " + loadedScene.name + " in mode " + mode);

    void Start()
    {
        if (Instance == null) // Create singleton and then initialize it
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = maxFramerate; // Without this my laptop would turn into an Airbnb jet motor
    }

    public void GoToMainMenu(InputAction.CallbackContext context)
    {
        if (context.performed && SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
            SceneManager.LoadSceneAsync(0);
    }
}
