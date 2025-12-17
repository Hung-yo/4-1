using UnityEngine;

public class TotalDeathsManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI totalDeathsText;
    void Start()
    {
        DeathManager.totalDeaths = PlayerPrefs.GetInt("TotalDeaths", 0);
        UpdateTotalDeathsText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTotalDeathsText()
    {
        totalDeathsText.text = "Total Deaths: " + DeathManager.totalDeaths;
    }

    public void IncrementTotalDeaths()
    {
        DeathManager.totalDeaths++;
        PlayerPrefs.SetInt("TotalDeaths", DeathManager.totalDeaths);
        PlayerPrefs.Save();
    }
}
