using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafetyBriefBlackboard : MonoBehaviour
{
    public GameObject[] blackboradSlides;
    private int currentSlideIndex = 0;

    
    public void Start()
    {
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


}
