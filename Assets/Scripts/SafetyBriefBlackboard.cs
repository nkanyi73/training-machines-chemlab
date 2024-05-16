using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class SafetyBriefBlackboard : MonoBehaviour
{
    public GameObject[] blackboradSlides;
    private int currentSlideIndex = 0;
    private GameObject mainCamera;
    private DialogueRunner dialogueRunner;

    
    public void Start()
    {
        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //disable all slides expect slide 0
        for (int i = 1; i < blackboradSlides.Length; i++)
        {
            blackboradSlides[i].SetActive(false);
        }
    }
    public void NextSlide() 
    {
        dialogueRunner.Stop();
        //disable current slide when the button is pressed and active the next slide
        blackboradSlides[currentSlideIndex].SetActive(false);
        currentSlideIndex++;
        if (currentSlideIndex >= blackboradSlides.Length)
        {
            currentSlideIndex = 0;
        }
        blackboradSlides[currentSlideIndex].SetActive(true);
        PlayAudio(currentSlideIndex);
        
    
    }

    private void PlayAudio(int currentSlideIndex)
    {
        switch (currentSlideIndex)
        {
            case 0:
                break;
            case 1:
                dialogueRunner.StartDialogue("Safe2");
                break;
            case 2:
                dialogueRunner.StartDialogue("Safe3");
                break;
            case 3:
                dialogueRunner.StartDialogue("Safe4");
                break;
            case 4:
                dialogueRunner.StartDialogue("Safe5");
                break;
        }
    }

    public void SwitchScene()
    {
        StartCoroutine(SwitchSceneCoroutine());
    }

    private IEnumerator SwitchSceneCoroutine()
    {
        mainCamera.GetComponent<OVRScreenFade>().FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScene - Nick");
    }

}
