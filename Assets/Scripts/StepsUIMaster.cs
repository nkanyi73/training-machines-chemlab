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
    public DialogueRunner dialogueRunner;

    public string[] titleText;
    public string[] descriptionText;

    private int currentStep;

    public GameObject[] beakers;
    public GameObject[] solutionLabels;
    public Transform[] beakersResetOneTransform;

    private GameObject mainCamera;

    [Header("Step One GameObjects")]
    public GameObject resetStepOneButton;

    [Header("Step Two GameObjects")]
    public GameObject resetStepTwoButton;
    public GameObject bunsenBurner;

    [Header("Step Three GameObjects")]
    public GameObject resetStepThreeButton;
    public GameObject nichromeWire;
    public Transform nichromeWireTransform;

    [Header("Quiz GameObjects")]
    public GameObject iPad;
    public QuizController quizController;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        UpdateUI();
    }

    public void NextButtonAction()
    {
        currentStep++;
        UpdateUI();

    }

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
                case 10:
                    resetStepTwoButton.SetActive(true);
                    break;
                case 11:
                    SwitchToStepThree();
                    break;
                case 24:
                    resetStepThreeButton.SetActive(true);
                    break;
                case 25:
                    SwitchToQuizStep();
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
        dialogueRunner.Stop();
        resetStepOneButton.SetActive(false);
        bunsenBurner.SetActive(true);
        dialogueRunner.StartDialogue("Step2");

        mainCamera.GetComponent<OVRScreenFade>().FadeIn();
    }

    private void SwitchToStepThree()
    {
        // do a fade
        mainCamera.GetComponent<OVRScreenFade>().FadeOut();
        dialogueRunner.Stop();
        nichromeWire.SetActive(true);
        resetStepTwoButton.SetActive(false);
        bunsenBurner.SetActive(true);
        dialogueRunner.StartDialogue("Step3");

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
            case 3:
                ResetToBeginningOfStepThree();
                break;
        }

        mainCamera.GetComponent<OVRScreenFade>().FadeIn();
    }

    public void ResetToBeginningOfStepOne()
    {
        dialogueRunner.Stop();
        currentStep = 0;
        UpdateUI();
        resetStepOneButton.SetActive(false);
        //description text same as above


        for (int i = 0; i < beakers.Length; i++)
        {
            beakers[i].transform.position = beakersResetOneTransform[i].position;
        }
        dialogueRunner.StartDialogue("Step1");
    }

    public void ResetToBeginningOfStepTwo()
    {
        dialogueRunner.Stop();
        currentStep = 7;
        UpdateUI();
        resetStepTwoButton.SetActive(false);

        for (int i = 0; i < beakers.Length; i++)
        {
            beakers[i].transform.position = beakersResetOneTransform[i].position;
        }
        //dialogueRunner.StartDialogue("Step2");

    }

    public void ResetToBeginningOfStepThree()
    {
        dialogueRunner.Stop();
        currentStep = 11;
        UpdateUI();
        resetStepTwoButton.SetActive(false);

        for (int i = 0; i < beakers.Length; i++)
        {
            beakers[i].transform.position = beakersResetOneTransform[i].position;
        }
        //dialogueRunner.StartDialogue("Step3");

    }

    public void SwitchToQuizStep()
    {
        mainCamera.GetComponent<OVRScreenFade>().FadeOut();
        resetStepThreeButton.SetActive(false);
        iPad.SetActive(true);

        for (int i = 0;i < beakers.Length; i++)
        {
            Destroy(beakers[i]);
            Destroy(solutionLabels[i]);
        }
        nichromeWire.transform.position = nichromeWireTransform.position;
        quizController.InstantiateQuizElements();

        mainCamera.GetComponent<OVRScreenFade>().FadeIn();
    }
}
