using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SafetyBriefBlackboard : MonoBehaviour
{
    public GameObject[] blackboradSlides;
    private int currentSlideIndex = 0;
    private GameObject mainCamera;

    
    public void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //disable all slides expect slide 0
        for (int i = 1; i < blackboradSlides.Length; i++)
        {
            blackboradSlides[i].SetActive(false);
        }
    }
    public void NextSlide() 
    {
        //disable current slide when the button is pressed and active the next slide
        blackboradSlides[currentSlideIndex].SetActive(false);
        currentSlideIndex++;
        if (currentSlideIndex >= blackboradSlides.Length)
        {
            currentSlideIndex = 0;
        }
        blackboradSlides[currentSlideIndex].SetActive(true);
        
    
    }

    public void SwitchScene()
    {
        mainCamera.GetComponent<OVRScreenFade>().FadeOut();

        SceneManager.LoadScene("MainScene - Nick");
    }

}
