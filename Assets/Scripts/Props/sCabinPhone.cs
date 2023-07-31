using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCabinPhone : MonoBehaviour, IInteractable
{
    public bool isInteractable { get; set; }
    public string HudMessage { get; set; }
    public GameObject musicBox;
    public AudioClip audioFile;
    private AudioSource m_Audi;

    public void Start()
    {
        musicBox = GameManager.gm.gameObject;
        m_Audi = gameObject.GetComponent<AudioSource>();
        isInteractable = true;
        HudMessage = "Play Message";
    }

    public void OnInteract()
    {
        if (GameManager.gm.questManager.localQuestId == 13) GameManager.gm.questManager.AdvanceStep();
        StartCoroutine(CallCoroutine());
    }
    public void OnLookingAt()
    {

    }

    IEnumerator CallCoroutine()
    {
        GameManager.gm.audioManager.StopMusic();
        isInteractable = false;
        musicBox?.SetActive(false);
        m_Audi.PlayOneShot(audioFile);
        yield return new WaitForSeconds(audioFile.length);
        GameManager.gm.audioManager.RandomMusicClip();
        isInteractable = true;
        musicBox?.SetActive(true);

    }
}
