using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour {

    private string[] _videogames_names = new string[26]
    {
        "AnotherD",
        "Arrow",
        "Be-Headed",
        "CloudDiver",
        "CursedHeroOfThePast",
        "DarkUnknown",
        "Don'TLetLukeFall",
        "DreamDiary",
        "FeedMe,Quack!",
        "Freshaliens",
        "HarmonyInDarkness",
        "HotPotato",
        "JourneyToAntartica",
        "JustTheTwoOfUs",
        "LaserKnight",
        "NoClip",
        "Paradox!",
        "ParkourNews",
        "PumpDownTheFlame",
        "Reset::Relive",
        "Shade",
        "SleepInvasion",
        "TheFelineParadox",
        "TiedRivals",
        "Zorball",
        ">>Test"
    };

    enum VideoGamesName
    {
        AnotherD=0,
        Arrow=1,
        BeHeaded=2,
        CloudDiver=3,
        CursedHeroOfThePast=4,
        DarkUnknown=5,
        DonTLetLukeFall=6,
        DreamDiary=7,
        FeedMeQuack=8,
        Freshaliens=9,
        HarmonyInDarkness=10,
        HotPotato=11,
        JourneyToAntartica=12,
        JustTheTwoOfUs=13,
        LaserKnight=14,
        NoClip=15,
        Paradox=16,
        ParkourNews=17,
        PumpDownTheFlame=18,
        ResetRelive=19,
        Shade=20,
        SleepInvasion=21,
        TheFelineParadox=22,
        TiedRivals=23,
        Zorball=24,
        Test=25
    };
    
    [SerializeField] private VideoGamesName Videogame;
    [SerializeField] private InputField Feedback;
    
    public void SendFeedback()
    {
        string feedback = Feedback.text;
        StartCoroutine(PostFeedback(_videogames_names[(int) Videogame],feedback));
    }
    
    IEnumerator PostFeedback(string videogame_name, string feedback) 
    {
        // https://docs.google.com/forms/d/e/1FAIpQLSdyQkpRLzqRzADYlLhlGJHwhbKZvKJILo6vGmMfSePJQqlZxA/viewform?usp=pp_url&entry.631493581=Simple+Game&entry.1313960569=Very%0AGood!

        string URL =
            "https://docs.google.com/forms/d/e/1FAIpQLSeFwyAYVYJfyQE5NovsRw7VlhvCxZksuT1Gf-m_MGqPjy43-w/formResponse";
        
        WWWForm form = new WWWForm();

        form.AddField("entry.1179497332", videogame_name);
        form.AddField("entry.40349665", feedback);

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

        print(www.error);
        
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        
        // at the end go back to the main menu
        MainMenuManager.Instance.CloseFeedback();
    }
    
    
    
    public static IEnumerator PostTimer(PlayerPrefsKeys key)
    {
        float timer = PlayerPrefsManager.GetFloat(key, 0);
        string name = key.ToString();
        string URL =
            "https://docs.google.com/forms/d/e/1FAIpQLSeFwyAYVYJfyQE5NovsRw7VlhvCxZksuT1Gf-m_MGqPjy43-w/formResponse";
        
        WWWForm form = new WWWForm();

        string timerStr = name + " = " + (timer / 60.0).ToString("#.##");
        
        form.AddField("entry.1560066554", timerStr);

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

        print(www.error);
        
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        
    }
    
    public static IEnumerator PostFinishedGameLevel(int level)
    {
        string URL =
            "https://docs.google.com/forms/d/e/1FAIpQLSeFwyAYVYJfyQE5NovsRw7VlhvCxZksuT1Gf-m_MGqPjy43-w/formResponse";
        
        WWWForm form = new WWWForm(); 
        
        form.AddField("entry.1587231683", "Level" + level + " Finished");

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

        print(www.error);
        
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        
    }
    
    public static IEnumerator PostStartedGame()
    {
        string URL =
            "https://docs.google.com/forms/d/e/1FAIpQLSeFwyAYVYJfyQE5NovsRw7VlhvCxZksuT1Gf-m_MGqPjy43-w/formResponse";
        
        WWWForm form = new WWWForm(); 
        
        form.AddField("entry.2072885783", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

        print(www.error);
        
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        
    }
    
}