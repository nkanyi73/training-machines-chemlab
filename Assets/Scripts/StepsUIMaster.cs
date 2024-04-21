using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Yarn.Unity;
using System;

public class StepsUIMaster : MonoBehaviour
{
    public TextMeshProUGUI tileTextMeshPro;
    public TextMeshProUGUI descriptionTextMeshPro;
    //public DialogueRunner dialogueRunner;

    public string[] titleText;
    public string[] descriptionText;

    private int currentStep;

    public GameObject[] beakers;
    public Transform[] beakersResetOneTransform;

    private GameObject mainCamera;

    [Header("Step One GameObjects")]
    public GameObject resetStepOneButton;

    [Header("Step Two GameObjects")]
    public GameObject resetStepTwoButton;
    public GameObject bunsenBurner;

    // Start is called before the first frame update
    void Start()
    {
        //dialogueRunner = FindAnyObjectByType<DialogueRunner>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        UpdateUI();
    }

    public void NextButtonAction()
    {
        currentStep++;
        UpdateUI();

        //// Check if the current step is within the valid range
        //if (currentStep >= 0 && currentStep < titleText.Length)
        //{
        //    string nodeToPlay = GetDialogueNodeForStep(currentStep);
        //    //float delay = GetDelayForStep(currentStep);

        //    // Start the coroutine for the current step
        //    //StartCoroutine(StartDialogueAfterDelay(delay, nodeToPlay));
        //}
    }

    //IEnumerator StartDialogueAfterDelay(float delay, string nodeToPlay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    dialogueRunner.StartDialogue(nodeToPlay);
    //}
    public void UpdateUI()
    {
        if(currentStep >= 0 && currentStep < titleText.Length)
        {
            tileTextMeshPro.text = titleText[currentStep];
            descriptionTextMeshPro.text = descriptionText[currentStep];
            descriptionTextMeshPro.text = FormatDescriptionText(descriptionText[currentStep]);

            switch(currentStep)
            {
                case 6:
                    resetStepOneButton.SetActive(true); 
                    break;
                case 7:
                    SwitchToStepTwo();
                    break;

            }
        }
        else
        {
            currentStep = 0;
        }
    }

    private void SwitchToStepTwo()
    {
        // do a fade
        mainCamera.GetComponent<OVRScreenFade>().FadeOut();
        resetStepOneButton.SetActive(false);
        bunsenBurner.SetActive(true);

        mainCamera.GetComponent<OVRScreenFade>().FadeIn();
    }

    private string FormatDescriptionText(string description)
    {
        // Split the description into lines
        string[] lines = description.Split('\n');

        // Add bullet points using rich text formatting
        string formattedText = "<b>Instructions:</b>\n";

        for (int i = 0; i < lines.Length; i++)
        {
            formattedText += $"{lines[i]}\n";
        }

        return formattedText;
    }

    public void StageResetControl(int stageToReset)
    {
        StartCoroutine(ResetTrainingStage(stageToReset));
    }

    public IEnumerator ResetTrainingStage(int stageToReset)
    {
        // do a fade
        mainCamera.GetComponent<OVRScreenFade>().FadeOut();

        //do a wait
        yield return new WaitForSeconds(2);

        switch (stageToReset)
        {
            case 1:
                ResetToBeginningOfStepOne();
                break;
       
            case 2:
                ResetToBeginningOfStepTwo();
                break;
        }

        mainCamera.GetComponent<OVRScreenFade>().FadeIn();

        //if stageToReset == 1 ....
        //do the reset
        //fade back in
    }

    public void ResetToBeginningOfStepOne()
    {
        currentStep = 0;
        UpdateUI();
        resetStepOneButton.SetActive(false);
        //description text same as above


        for (int i = 0; i < beakers.Length; i++)
        {
            beakers[i].transform.position = beakersResetOneTransform[i].position;
        }
    }

    public void ResetToBeginningOfStepTwo()
    {
        currentStep = 7;
        //description text same as above

        //for (int i = 0; i < beakers.Length; i++)
        //{
        //    beakers[i].transform.position = beakersResetOneTransform[i].position;
        //}
    }
}
