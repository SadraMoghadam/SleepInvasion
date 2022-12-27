using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroOutroController : MonoBehaviour
{
    [SerializeField] private bool isIntro;
    
    private VideoPlayer _video;


    private void Start()
    {
        _video = GetComponent<VideoPlayer>();
        _video.loopPointReached += CheckOver;
    }
 
    void CheckOver(VideoPlayer vp)
    {
        if (isIntro)
        {
            GameManager.Instance.LoadScene("MainGame");
        }
        else
        {
            GameManager.Instance.LoadScene("MainMenu");
        }
    }
    
}
