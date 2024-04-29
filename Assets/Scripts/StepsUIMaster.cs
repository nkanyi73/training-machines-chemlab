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

    [Header("Step Three GameObjects")]
    public GameObject resetStepThreeButton;
    public GameObject nichromeWire;
    public GameObject nichromeWireTransform;

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

    private void SwitchToStepThree()
    {
        // do a fade
        mainCamera.GetComponent<OVRScreenFade>().FadeOut();
        nichromeWire.SetActive(true);
        resetStepTwoButton.SetActive(false);
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
            case 3:
                ResetToBeginningOfStepThree();
                break;
        }

        mainCamera.GetComponent<OVRScreenFade>().FadeIn();
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
        UpdateUI();
        resetStepTwoButton.SetActive(false);

        for (int i = 0; i < beakers.Length; i++)
        {
            beakers[i].transform.position = beakersResetOneTransform[i].position;
        }

    }

    public void ResetToBeginningOfStepThree()
    {
        currentStep = 11;
        UpdateUI();
        resetStepTwoButton.SetActive(false);

        for (int i = 0; i < beakers.Length; i++)
        {
            beakers[i].transform.position = beakersResetOneTransform[i].position;
        }

    }
}
