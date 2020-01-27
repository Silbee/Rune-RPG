using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

[System.Serializable]
public class Options
{
    public float masterVolume, musicVolume, sfxVolume;
}

public class MainMenu : MonoBehaviour
{
    string path;

    Options options = new Options();
    Canvas menuCanvas, optionsCanvas;
    [SerializeField] AudioMixerGroup masterMixer, musicMixer, sfxMixer;
    Slider masterVolumeSlider, musicVolumeSlider, sfxVolumeSlider;
    TMP_Text masterMixerPercentageText, musicMixerPercentageText, sfxMixerPercentageText;


    void Start()
    {
        menuCanvas = GameObject.Find("Main Menu").GetComponent<Canvas>();
        optionsCanvas = GameObject.Find("Options Menu").GetComponent<Canvas>();

        masterVolumeSlider = GameObject.Find("MasterVolumeSlider").GetComponent<Slider>();
        musicVolumeSlider = GameObject.Find("MusicVolumeSlider").GetComponent<Slider>();
        sfxVolumeSlider = GameObject.Find("SFXVolumeSlider").GetComponent<Slider>();

        masterMixerPercentageText = GameObject.Find("MasterPercentage").GetComponent<TMP_Text>();
        musicMixerPercentageText = GameObject.Find("MusicPercentage").GetComponent<TMP_Text>();
        sfxMixerPercentageText = GameObject.Find("SFXPercentage").GetComponent<TMP_Text>();

        path = Application.streamingAssetsPath + "/Options.json";
        options = JsonUtility.FromJson<Options>(File.ReadAllText(path));

        masterVolumeSlider.value = options.masterVolume;
        musicVolumeSlider.value = options.musicVolume;
        sfxVolumeSlider.value = options.sfxVolume;
    }

    void OnDisable() => File.WriteAllText(path, JsonUtility.ToJson(options, true));

    public void SetMasterVolume() => ConfigureSlider(masterMixer, "masterVolume", masterVolumeSlider, masterMixerPercentageText);

    public void SetMusicVolume() => ConfigureSlider(musicMixer, "musicVolume", musicVolumeSlider, musicMixerPercentageText);

    public void SetSFXVolume() => ConfigureSlider(sfxMixer, "sfxVolume", sfxVolumeSlider, sfxMixerPercentageText);

    public void StartGame() => SceneManager.LoadSceneAsync(2);

    public void QuitGame() => Application.Quit();

    public void ToggleFullscreen() => Screen.fullScreen = !Screen.fullScreen;

    public void ToggleCanvasEnabled()
    {
        optionsCanvas.enabled = !optionsCanvas.enabled;
        menuCanvas.enabled = !menuCanvas.enabled;
    }

    void ConfigureSlider(AudioMixerGroup targetMixer, string parameterName, Slider targetSlider, TMP_Text targetSliderPercentageText)
    {
        targetMixer.audioMixer.SetFloat(parameterName, targetSlider.value);
        targetSliderPercentageText.SetText((100 / targetSlider.minValue * -targetSlider.value + 100).ToString("F0") + "%");

        if (targetMixer.audioMixer.GetFloat(parameterName, out float volumeValue))
            switch (parameterName)
            {
                case "masterVolume":
                    options.masterVolume = volumeValue;
                    break;
                case "musicVolume":
                    options.musicVolume = volumeValue;
                    break;
                case "sfxVolume":
                    options.sfxVolume = volumeValue;
                    break;
                default:
                    break;
            }
    }
}
