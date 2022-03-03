using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

//This script handles the main menu

public class MainSceneController : MonoBehaviour
{
    public Canvas chooseCharacterCanvas;
    bool chooseCharacterOn = false;
    public Canvas chooseModeCanvas;
    bool chooseModeOn = false;
    public Canvas audioSettingsCanvas;
    bool audioSettingsOn = false;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Text musicText = null;
    [SerializeField] private Slider effectsSlider = null;
    [SerializeField] private Text effectsText = null;
    public Canvas howToPlay;
    bool howToPlayOn = false;
    public Canvas credits;
    bool creditsOn = false;
    public Canvas quit;
    bool quitOn = false;
    public MouseLook mouse;


    //public AudioSource audioSource;
    //public AudioClip clip;
    //public bool mute = false;
    public static MainSceneController inst;

    //transition to the desired scene
    public void LoadGameScene(string characterName)
    {
        PlayerPrefs.SetString("character", characterName);
        AudioManager.instance.Stop("MainMenu");
        //AudioManager.instance.Play("Combat");
        SceneManager.LoadScene("Game");
    }

    public void LoadScene(string scene)
    {
        if (scene == "Parkour")
        {
            AudioManager.instance.Stop("MainMenu");
            //AudioManager.instance.Play("Parkour");
            PlayerPrefs.SetString("character", "BloodHound");
        }
        SceneManager.LoadScene(scene);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        inst = this;
    }
    void Start()
    {
        mouse.SetCursorLock(false);
        PlayerPrefs.SetFloat("musicValue", 1.0f);
        PlayerPrefs.SetFloat("effectsValue", 1.0f);
        AudioManager.instance.AdjustVolume();
        AudioManager.instance.Play("MainMenu");
        chooseCharacterCanvas.enabled = false;
        chooseModeCanvas.enabled = false;
        audioSettingsCanvas.enabled = false;
        LoadValues();
        howToPlay.enabled = false;
        credits.enabled = false;
        quit.enabled = false;

        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (chooseCharacterOn == false)
        {
            chooseCharacterCanvas.enabled = false;
        }
        else
        {
            chooseCharacterCanvas.enabled = true;
        }
        if (chooseModeOn == false)
        {
            chooseModeCanvas.enabled = false;
        }
        else
        {
            chooseModeCanvas.enabled = true;
        }
        if (audioSettingsOn == false)
        {
            audioSettingsCanvas.enabled = false;
        }
        else
        {
            audioSettingsCanvas.enabled = true;
        }
        if (howToPlayOn == false)
        {
            howToPlay.enabled = false;
        }
        else
        {
            howToPlay.enabled = true;
        }

        if (creditsOn == false)
        {
            credits.enabled = false;
        }
        else
        {
            credits.enabled = true;
        }

        if (quitOn == false)
        {
            quit.enabled = false;
        }
        else
        {
            quit.enabled = true;
        }


        /*
        if (mute == true)
        {
            audioSource.Stop();
        }
        */
    }

    public void Play()
    {
        chooseCharacterOn = true;
    }

    public void Mode()
    {
        chooseModeOn = true;
    }

    public void Audio()
    {
        audioSettingsOn = true;
    }
    public void MusicSlider(float music)
    {
        musicText.text = music.ToString("0.0");
    }

    public void SaveMusic()
    {
        float musicValue = musicSlider.value;
        PlayerPrefs.SetFloat("musicValue", musicValue);
        LoadValues();
        AudioManager.instance.AdjustVolume();
    }

    public void EffectsSlider(float effects)
    {
        effectsText.text = effects.ToString("0.0");
    }

    public void SaveEffects()
    {
        float effectsValue = effectsSlider.value;
        PlayerPrefs.SetFloat("effectsValue", effectsValue);
        LoadValues();
        AudioManager.instance.AdjustVolume();
    }

    void LoadValues()
    {
        float musicValue = PlayerPrefs.GetFloat("musicValue");
        musicSlider.value = musicValue;
        float effectsValue = PlayerPrefs.GetFloat("effectsValue");
        effectsSlider.value = effectsValue;
        //can be used
        //AudioListener.volume = musicValue;
    }

    public void AudioBack()
    {
        audioSettingsOn = false;
        audioSettingsCanvas.enabled = false;
    }

    public void HowToPlay()
    {
        howToPlayOn = true;
    }

    public void HowToPlayBack()
    {
        howToPlayOn = false;
        howToPlay.enabled = false;
    }

    public void Credits()
    {
        creditsOn = true;
    }

    public void CreditsBack()
    {
        creditsOn = false;
        credits.enabled = false;
    }

    public void Mute()
    {
        //mute = true;
    }

    public void Quit()
    {
        quitOn = true;
        Application.Quit();
    }
    public void QuitBack()
    {
        quitOn = false;
        quit.enabled = false;
    }
}
