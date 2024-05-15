using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class OnboardingUIManager : MonoBehaviour
{
    public TextMeshProUGUI tileTextMeshPro;
    public TextMeshProUGUI descriptionTextMeshPro;
    public DialogueRunner dialogueRunner;

    public string[] titleText;
    public string[] descriptionText;

    private int currentStep;
    private GameObject mainCamera;

    [Header("Scene Elements")]
    public GameObject controllerImage;
    public GameObject resetButton;
    public GameObject SwitchSceneButton;
    public GameObject NextButton;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
        UpdateUI();
    }

    public void NextButtonAction()
    {
        currentStep++;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (currentStep >= 0 && currentStep < titleText.Length)
        {
            tileTextMeshPro.text = titleText[currentStep];
            descriptionTextMeshPro.text = descriptionText[currentStep];
            descriptionTextMeshPro.text = FormatDescriptionText(descriptionText[currentStep]);

            switch (currentStep)
            {
                case 2:
                    controllerImage.SetActive(false);
                    resetButton.SetActive(true);
                    SwitchSceneButton.SetActive(true);
                    NextButton.SetActive(false);
                    break;

            }
        }
        else
        {
            currentStep = 0;
        }
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
                ResetToBeginning();
                break;
        }

        mainCamera.GetComponent<OVRScreenFade>().FadeIn();
    }

    public void ResetToBeginning()
    {
        dialogueRunner.Stop();
        currentStep = 0;
        UpdateUI();
        resetButton.SetActive(false);
        SwitchSceneButton.SetActive(false) ;
        NextButton.SetActive(true);
        controllerImage.SetActive(true);
        dialogueRunner.StartDialogue("Onboarding");
        //description text same as above
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

    public void SwitchScene()
    {
        mainCamera.GetComponent<OVRScreenFade>().FadeOut();

        SceneManager.LoadScene("MainScene - Safety");
    }
}


