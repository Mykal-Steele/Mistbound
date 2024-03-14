using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Playtime : MonoBehaviour
{
    private int playtime = 0;
    private int seconds = 0;
    private int minutes = 0;
    private int hours = 0;
    private bool isShowing = false;
    public TextMeshProUGUI playtimeshow = null;

    void Start()
    {
        LoadPlaytime(); // Load saved playtime when the game starts
        StartCoroutine("Playtimer");
    }

    private IEnumerator Playtimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            playtime += 1;
            seconds = (playtime % 60);
            minutes = (playtime / 60) % 60;
            hours = (playtime / 3600);

            if (isShowing)
            {
                UpdatePlaytimeText();
            }

            SavePlaytime(); // Save playtime every second
        }
    }

    void Update()
    {
        if (isShowing)
        {
            UpdatePlaytimeText();
        }
    }


    public void TogglePlaytimeDisplay()
    {
        isShowing = !isShowing;
        if (isShowing)
        {
            UpdatePlaytimeText();
        }
        else
        {
            playtimeshow.text = "Click here to see your playtime";
        }
    }

    void UpdatePlaytimeText()
    {
        playtimeshow.text = "Playtime = " + hours.ToString() + " Hours " + minutes.ToString() + " Minutes " + seconds.ToString() + " Seconds";
    }
    public void SavePlaytime()
    {
        PlayerPrefs.SetInt("SavedPlaytime", playtime);
        PlayerPrefs.Save();
    }

    public void LoadPlaytime()
    {
        if (PlayerPrefs.HasKey("SavedPlaytime"))
        {
            playtime = PlayerPrefs.GetInt("SavedPlaytime");
        }
    }
}
