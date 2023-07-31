using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class GoodbyeMouse : MonoBehaviour
{
    GameObject lastSelect;
    public GameObject masterText;
    public GameObject musicText;
    public GameObject sfxText;
    public GameObject voiceText;


    private void Start()
    {
        lastSelect = new GameObject();
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            // Debug.Log($"{EventSystem.current.currentSelectedGameObject}");
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
        if (lastSelect == GameObject.Find("Slider - Master")) masterText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
        else masterText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        if (lastSelect == GameObject.Find("Slider - Music")) musicText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
        else musicText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        if (lastSelect == GameObject.Find("Slider - SFX")) sfxText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
        else sfxText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        if (lastSelect == GameObject.Find("Slider - Voice")) voiceText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
        else voiceText.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
    }
    public void Test()
    {

    }
}
