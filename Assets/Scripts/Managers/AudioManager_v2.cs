using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum eSFX { 
                    // AMBIENCE
                    nightAmbience, dayAmbience, rain, wind,

                    // EVENTS
                    startLevel, endLevel, gameOver, 
                    pauseOn, pauseOff,
                    buttonClick, quitGame,

                    // SFX
                    swimming, walking, flashlightClick, flareGunShot, flareGunReload, lightningBottleCharged, lightningBottlePlaced,

                    // Cassette
                    letterCassette, cassette_01, Cassette_02, Cassette_03, Cassette_04, Cassette_05
}

public enum eMusic { titleMusic, gameplayMusic1 }


public class AudioManager_v2 : MonoBehaviour
{

    [Space]
    [Header("Volume Settings")]
    public float masterVolume = 1f;
    public float musicVolume = .5f;
    public float sfxVolume = .5f;
    public float voiceVolume = .5f;

    [Space]
    [Header("Audio Mixers")]
    public AudioMixerGroup masterMixer;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup voiceMixer;

    [Space]
    [Header("Audio Clips")]
    [NamedArray(typeof(eMusic))] public AudioClip[] music;
    [NamedArray(typeof(eSFX))] public AudioClip[] sfx;

    private AudioClip currentMusic;
    eMusic eCurrentMusic;

    [Space]
    [Header("Audio Sources")]
    public AudioSource sfxSource;

    public AudioSource musicSource1;
    public AudioSource musicSource2;

    public AudioSource ambienceSource;

    public AudioSource currentMusicSource;
    private AudioSource standbyMusicSource;

    public static int musicOrder = 0;

    AudioLowPassFilter lowPassFilter;

    // Awake Refs for making into Singleton
    private void Awake()
    {
        /*
        if (am == null)

        {
            am = this;
        }

        else if (am != this)
        {
            Destroy(gameObject);
        }
        */
        currentMusicSource = musicSource1;

        standbyMusicSource = musicSource2;

        musicSource1.loop = true;

        musicSource2.loop = true;
    }

    private void Start()
    {
        ChangeMasterVolume(masterVolume);
        ChangeMusicVolume(musicVolume);
        ChangeSFXVolume(sfxVolume);
        ChangeVoiceVolume(voiceVolume);
        RandomMusicClip();
    }
    
    // This function randomly choose a song from the music list and plays it
    public void RandomMusicClip()
    {
        if (GameManager.gm.curScene == GameManager.eScene.CTS_FrontEndScene) PlayMusic(eMusic.titleMusic);
        else if (GameManager.gm.curScene != GameManager.eScene.CTS_FrontEndScene)
        {
            int song;
            song = Random.Range(0, music.Length);
            FadeIn(musicSource1, 2f);
            PlayMusic((eMusic)song);
        }
    }

    // USE THIS FUNCTION TO SWITCH UP THE AUDIOCLIP FOR MUSIC
    public void ChangeMusicClip(AudioSource _source, eMusic _music)
    {

        _source.clip = music[(int)_music];

    }

    // USE THIS TO PLAY MUSIC ONCE THE CLIP HAS BEEN SET
    public void PlayMusic(eMusic _music)
    {
        currentMusic = music[(int)_music];
        Debug.Log($"{currentMusic}");

        currentMusicSource.clip = currentMusic;

        currentMusicSource.Play();

    }

    // USE THIS FOR GAMEOBJECTS WITH THEIR OWN AUDIOSOURCES TO RETURN CLIPS.  MOSTLY FOR 3D AUDIO.
    public AudioClip GetSFX(eSFX _sfx)
    {
        return (sfx[(int)_sfx]);
    }

    // USE THIS TO PLAY ANY SFX FROM ANYWHERE IN 2D
    public void PlaySFX(eSFX _sfx)
    {
        //Debug.Log("Playing " + sfx[(int)_sfx]);
        if (sfx[(int)_sfx] != null)
            sfxSource.PlayOneShot(sfx[(int)_sfx]);
        else
            Debug.LogWarning(_sfx.ToString() + " sound effect still needs a clip");
    }

    public void PlayAudioAtPoint(Transform _pointToPlay, eSFX _sfx)
    {

    }

    // MIXER CONTROLS - USE THESE FOR UI/OPTIONS MENU TO CHANGE SOUND PROPERTIES
    #region Volume Sliders
    public void ChangeMasterVolume(float _newValue)// Changes fader value of master volume
    {
        _newValue = Mathf.Clamp(_newValue, 0.1f, 1f);
        masterVolume = _newValue;
        masterMixer.audioMixer.SetFloat("MasterVolume", Mathf.Log10(_newValue) * 20);
    }
    public void ChangeMusicVolume(float _newValue)// Changes fader value of music volume
    {
        _newValue = Mathf.Clamp(_newValue, 0.1f, 1f);
        musicVolume = _newValue;
        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Log10(_newValue) * 20);// Changes as a logarithmic fade

    }

    public void ChangeSFXVolume(float _newValue)// Changes fader value of sfx volume
    {
        _newValue = Mathf.Clamp(_newValue, 0.1f, 1f);
        sfxVolume = _newValue;
        sfxMixer.audioMixer.SetFloat("SFXVolume", Mathf.Log10(_newValue) * 20);

    }

    public void ChangeVoiceVolume(float _newValue)
    {
        _newValue = Mathf.Clamp(_newValue, 0.1f, 1f);
        voiceVolume = _newValue;
        sfxMixer.audioMixer.SetFloat("VoiceVolume", Mathf.Log10(_newValue) * 20);

    }
    #endregion

    // STOP SOUNDS

    public void StopMusic()
    {

        //currentMusicSource.clip = currentMusic;
        currentMusicSource.Stop();

        //musicSource[(int)_soundIndex].Stop();
    }

    // FADING

    // FADES OUT A AUDIOSOUCE PASSED INTO IT
    public IEnumerator FadeOut(AudioSource _audioSource, float _fadeTime)
    {

        float startVolume = _audioSource.volume;

        while (_audioSource.volume > 0)
        {

            _audioSource.volume -= Time.deltaTime / _fadeTime;
            yield return null;

        }

        _audioSource.Stop();

        standbyMusicSource = _audioSource;
    }

    // FADES IN AN AUDIOSOUCES PASSED IN
    public IEnumerator FadeIn(AudioSource _audioSource, float _fadeTime)
    {

        float startVolume = _audioSource.volume;

        currentMusicSource = _audioSource;

        _audioSource.Play();
        _audioSource.volume = 0f;

        while (_audioSource.volume < 1)
        {


            _audioSource.volume += Time.deltaTime / _fadeTime;
            yield return null;

        }

    }

    // AUDIO FILTER - USE THIS WHEN PAUSED, ETC

    public void LowPassMusicToggle(bool _turnOn)
    {


        if (_turnOn)
        {
            musicMixer.audioMixer.SetFloat("LowPassFilter", 1000);
        }
        else
        {
            musicMixer.audioMixer.SetFloat("LowPassFilter", 22000);
        }

        //float startFreq = musicMixer.audioMixer.GetFloat("LowPassFilter");

    }

    // RETURNS CURRENT AUDIOSOURCE
    public AudioSource GetCurrentMusicSource()
    {

        return currentMusicSource;

    }

    // RETURNS STANDBY SOURCE
    public AudioSource GetStandbyMusicSource()
    {

        return standbyMusicSource;

    }

}