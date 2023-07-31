using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    PlayerMovement movement;
    public AudioManager_v2 audioManager_V2;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider voiceSlider;
    [SerializeField] Slider masterSlider;

    [SerializeField] Slider lookSlider;

    public const string mixerMusic = "MusicVolume";
    public const string mixerSFX = "SoundVolume";
    public const string mixerVoice = "VoiceVolume";
    public const string mixerMaster = "MasterVolume";

    //RESOLUTION STUFF
    [SerializeField] TMP_Dropdown resField1;
    [SerializeField] TMP_Dropdown resField2;
    [SerializeField] TMP_Dropdown resField3;

    private void Awake()
    {
        // Need to Set Save System that pulls from .json file
        audioManager_V2 = GameObject.Find("AudioManager").GetComponent<AudioManager_v2>();
        musicSlider.value = PlayerPrefs.GetFloat(mixerMusic, audioManager_V2.musicVolume);
        sfxSlider.value = PlayerPrefs.GetFloat(mixerSFX, audioManager_V2.sfxVolume);
        voiceSlider.value = PlayerPrefs.GetFloat(mixerVoice, audioManager_V2.voiceVolume);
        masterSlider.value = PlayerPrefs.GetFloat(mixerMaster, audioManager_V2.masterVolume);

        musicSlider.onValueChanged.AddListener(audioManager_V2.ChangeMusicVolume);
        sfxSlider.onValueChanged.AddListener(audioManager_V2.ChangeSFXVolume);
        voiceSlider.onValueChanged.AddListener(audioManager_V2.ChangeVoiceVolume);
        masterSlider.onValueChanged.AddListener(audioManager_V2.ChangeMasterVolume);
        lookSlider.onValueChanged.AddListener(SetPlayerSensitivity);
    }

    public void SetPlayerSensitivity(float _sliderVal)
    {
        movement.sensitivity = _sliderVal;
        Debug.Log(movement.sensitivity);
    }

    /*
    public void SetMusicVolume(float _volume)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(_volume) * 20);
    }

    public void SetSFXVolume(float _volume)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(_volume) * 20);
    }
    */

    public void SetFullscreen(bool setFullscreen)
    {
        Screen.fullScreen = setFullscreen;
    }
    
    public void SetRes(int _ResVal)
    {
        //Screen.SetResolution(_ResVal);
    }

    public void ApplyNewResolution()
    {
        //resField1.value
        //resField2.value
        //resField2.value
        //Screen.SetResolution();
    }
}
