using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MainMenuBehavior
{
    //Hereda de nuestra clase MainMenuBehavior, asi ya tenemos las funciones LoadLevel/Endgame

    public static bool isPaused;

    public GameObject pauseMenu;

    public GameObject optionsMenu;

    private void Start()
    {
        ContinueGame();
        UpdateQualityLabel();
        UpdateVolumeLabel();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(!optionsMenu.activeInHierarchy) //Solo se ejecuta si el menu de opciones no está activo
            {
                isPaused = !isPaused;
                Time.timeScale = (isPaused ? 1.0f : 0.0f);

                pauseMenu.SetActive(isPaused);
            }
            else
            {
                OpenPauseMenu();
            }


        }
    }

    public void ContinueGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel();
        UpdateQualityLabel();

    }
    public void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel();
        UpdateQualityLabel();
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        UpdateVolumeLabel();
    }

    private void UpdateQualityLabel()
    {
        int currentQuality = QualitySettings.GetQualityLevel();

        string qualityName = QualitySettings.names[currentQuality];

        optionsMenu.transform.Find("QualityLevel").GetComponent<TMPro.TextMeshProUGUI>().text = "Calidad actual: " + qualityName;


    }

    private void UpdateVolumeLabel()
    {
        float audioVolume = AudioListener.volume * 100;

        optionsMenu.transform.Find("MasterVolume").GetComponent<TMPro.TextMeshProUGUI>().text = "Volumen actual: " + audioVolume.ToString("f2")+"%";
    }

    public void OpenOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
