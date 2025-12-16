using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;

public class GodTextManager : MonoBehaviour
{
    public Dictionary<string, List<string>> godDialogue;
    public TMPro.TextMeshProUGUI godText;
    public float delay;

    void Awake()
    {
        string path = Application.streamingAssetsPath + "/GodDialogue.json";
        string json = File.ReadAllText(path);
        godDialogue = JsonUtility.FromJson<DialogueWrapper>(json).ToDictionary();
    }

    [System.Serializable]
    public class DialogueWrapper
    {
        public List<GodDialogue> dialogues;
        public Dictionary<string, List<string>> ToDictionary()
        {
            var dict = new Dictionary<string, List<string>>();
            foreach (var d in dialogues)
                dict[d.type] = d.lines;
            return dict;
        }
    }
    [System.Serializable]
    public class GodDialogue
    {
        public string type;
        public List<string> lines;
    }

    public IEnumerator ShowGodDialogue(string type, float delay)
    {
        if (godDialogue.ContainsKey(type))
        {
            foreach (var line in godDialogue[type])
            {
                godText.text = line;
                yield return new WaitForSeconds(delay);
            }
        }
    }
}