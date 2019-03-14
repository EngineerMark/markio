using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainmenu_ui : MonoBehaviour 
{

    public bool isSettingsOpen = false;

    public GameObject mainMenu;
    public GameObject settingsMenu;

    public Toggle fullscreenToggle;

    public Dropdown difficulty;

    private void Start()
    {
        if (PlayerPrefs.HasKey("resolutionX"))
        {
            SetResolution(PlayerPrefs.GetInt("resolutionX"),PlayerPrefs.GetInt("resolutionY"),Convert.ToBoolean(PlayerPrefs.GetString("resolutionFS")));
            fullscreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetString("resolutionFS"));
        }

        if (PlayerPrefs.HasKey("difficulty"))
            difficulty.value = (int)PlayerPrefs.GetFloat("difficulty") - 1;
        else
            PlayerPrefs.SetFloat("difficulty", 1);

        difficulty.onValueChanged.AddListener(delegate
        {
            SetDifficulty(difficulty);
        });
    }

    public void MainmenuPlaygame(){
		SceneManager.LoadScene("testing");
	}

	public void QuitGame(){
		Application.Quit();
	}

    public void ToggleSettingsMenu() {
        isSettingsOpen = !isSettingsOpen;
        mainMenu.SetActive(!isSettingsOpen);
        settingsMenu.SetActive(isSettingsOpen);
    }

    public void SettingsResolutionButton()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonName);

        string[] res = buttonName.Split('x');
        int x = Convert.ToInt32(res[0]);
        int y = Convert.ToInt32(res[1]);
        SetResolution(x, y, fullscreenToggle.isOn);
    }

    public void SettingsSetFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        PlayerPrefs.SetString("resolutionFS", Convert.ToString(fullscreenToggle.isOn));
    }

    public void SetResolution(int x, int y, bool fs)
    {
        Screen.SetResolution(x, y, fs);
        PlayerPrefs.SetInt("resolutionX", x);
        PlayerPrefs.SetInt("resolutionY", y);
        PlayerPrefs.SetString("resolutionFS", Convert.ToString(fs));
    }

    public void SetDifficulty(Dropdown dd)
    {
        PlayerPrefs.SetFloat("difficulty", dd.value + 1);
    }
}
