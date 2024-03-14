using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private TextMeshProUGUI volumeTextUI = null;
    public GameObject Options;
    public GameObject Main;
    public GameObject Maincover;
    private void Start()
    {
        LoadValues();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsMenuShow()
    {
        Options.SetActive(true);
        Main.SetActive(false);  
        Maincover.SetActive(false);
    }
    public void MainMenuShow()
    {
        Options.SetActive(false);
        Main.SetActive(true);  
        Maincover.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void VolumeSlider(float volume)
    {
        volumeTextUI.text = volume.ToString("F1");
    }
    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }


}
