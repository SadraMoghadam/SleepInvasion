using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	float secs = 2f;				// number of seconds to wait

	// public GameObject	splash;		// splash screen that is not shown in pro
	// public GameObject	pgc;		// polimi game collective splash
	public GameObject	splash;

	public string next_scene = string.Empty;

	// Use this for initialization
	void Start () {
#if UNITY_PRO 
		splash.enable = false;
#endif
		StartCoroutine (ShowSplashScreens ());
	}
	
	IEnumerator ShowSplashScreens()
	{
		// yield return new WaitForSeconds(secs);
		// pgc.SetActive(false);


#if !UNITY_PRO
		splash.SetActive(true);
		yield return new WaitForSeconds(6f);
		splash.SetActive(false);
#endif
		Application.LoadLevel(next_scene);
	}
}
