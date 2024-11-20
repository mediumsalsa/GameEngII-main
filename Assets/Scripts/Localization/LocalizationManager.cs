using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


//Menu Items
[System.Serializable]
public class Language
{
    public string lang;
    public string title;
    public string play;
    public string quit;
    public string options;
    public string credits;
}

public class LanguageData
{
    public Language[] languages;
}



public class LocalizationManager : MonoBehaviour
{
    public TextAsset jsonFile;
    public string currentLanguage;
    private LanguageData languageData;

    //Texts are placed here instead of UI to keep everything together
    public Text titleText;
    public Text playText;
    public Text quitText;
    public Text optionsText;
    public Text creditsText;

    //Deserializes the Json and sets the language to whatever
    private void Start()
    {
        languageData = JsonUtility.FromJson<LanguageData>(jsonFile.text);
        SetLanguage(currentLanguage);
    }

    public void SetLanguage(string newLanguage)
    {
        foreach (Language lang in languageData.languages)
        {
            if (lang.lang.ToLower() == newLanguage.ToLower()) 
            {
                titleText.text = lang.title;
                playText.text = lang.play;
                quitText.text = lang.quit;
                optionsText.text = lang.options;
                creditsText.text = lang.credits;
                return;
            }
        }
        Debug.LogWarning($"Language '{newLanguage}' not found in the JSON data.");
    }

}
