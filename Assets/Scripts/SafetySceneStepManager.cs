using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SafetySceneStepManager : MonoBehaviour
{
    private DialogueRunner dialogRunner;
    private Camera mainCamera;

    [System.Serializable]
    public class Step
    {
        public string stepName;

        public GameObject UIScreen;

        public string yarnNode;

    }

    public List<Step> Steps;

    private int currentStepIndex;

    // Start is called before the first frame update
    void Start()
    {
        dialogRunner = GameObject.FindObjectOfType<DialogueRunner>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        StartSteps();
    }

    private void StartSteps()
    {
        currentStepIndex = 0;
        LoadCurrentStep();
    }

    private void LoadCurrentStep()
    {
        Step currentStep = Steps[currentStepIndex];

        // Disable all screens before enabling the current one
        foreach (var step in Steps)
        {
            if (step.UIScreen) step.UIScreen.SetActive(false);
        }
        if (currentStep.UIScreen) currentStep.UIScreen.SetActive(true);


        // Trigger yarn spinner dialogue
        if (!string.IsNullOrEmpty(currentStep.yarnNode))
        {
            dialogRunner.Stop();
            dialogRunner.StartDialogue(currentStep.yarnNode);
        }
        else
        {
            dialogRunner.StartDialogue(currentStep.yarnNode);
        }
    }

    public void AdvanceStep()
    {
        if (currentStepIndex < Steps.Count)
        {
            currentStepIndex++;
            LoadCurrentStep();
        }
    }

    public void ResetSafetyScene()
    {
        currentStepIndex = 0;
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        mainCamera.gameObject.GetComponent<OVRScreenFade>().FadeOut();
        yield return new WaitForSeconds(5);
        LoadCurrentStep();
        mainCamera.gameObject.GetComponent<OVRScreenFade>().FadeIn();

    }
}
