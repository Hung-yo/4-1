using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;

public class PopupsManager : MonoBehaviour
{
    public string popupMessage;
    public GameObject popupTextBox;
    public TMPro.TextMeshProUGUI popupText;
    public float duration;
    public static PopupsManager popupsManager;

    private List<Popup> allPopups;

    void Awake()
    {
        if (popupsManager == null)
        {
            popupsManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        string path = Application.streamingAssetsPath + "/Popups.json";
        string json = File.ReadAllText(path);
        var wrapper = JsonUtility.FromJson<PopupsWrapper>(json);
        if (wrapper != null && wrapper.popups != null && wrapper.popups.Count > 0)
        {
            allPopups = wrapper.popups;
        }
        else
        {
            allPopups = new List<Popup>();
        }
    }

    void Start()
    {

    }

    [System.Serializable]
    public class PopupsWrapper
    {
        public List<Popup> popups;
    }

    [System.Serializable]
    public class Popup
    {
        public string type;
        public List<string> line;
    }

    public void ShowPopup(string type)
    {
        popupTextBox.SetActive(true);
        string popupMessage = "";
        if (allPopups != null)
        {
            foreach (var popup in allPopups)
            {
                if (popup.type == type && popup.line != null && popup.line.Count > 0)
                {
                    popupMessage = popup.line[0];
                    break;
                }
            }
        }
        StartCoroutine(_ShowPopup(popupMessage));
    }

    private IEnumerator _ShowPopup(string message)
    {
        popupText.text = message;
        yield return new WaitForSecondsRealtime(duration);
        popupText.text = "";
        popupTextBox.SetActive(false);
    }
}