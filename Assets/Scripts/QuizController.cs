using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class QuizController : MonoBehaviour
{
    [Header("Instantiation Parameters")]
    public Transform[] beakerTransforms;
    public GameObject[] beakerPrefabs;
    public Transform[] labelTransforms;
    public GameObject[] labelPrefabs;

    [Header("DropDowns")]
    public TMP_Dropdown[] dropDowns;
    private int correctAnswers;

    [Header("Airtable")]
    public AirtableManager airtableManager;

    [Header("Results")]
    public TMP_Text resultsTitle;
    public TMP_Text resultsBody;
    public GameObject resetButton;

    private DialogueRunner dialogueRunner;
    private GameObject mainCamera;
    public GameObject nichromeWire;
    public Transform nichromeWireTransform;

    private Dictionary<int, string> chemicalsDict = new Dictionary<int, string>();
    private Dictionary<int, string> chemicalsColor = new Dictionary<int, string>();
    private string resultDescription;

    private int[] chemicalsArray = new int[] { 0, 1, 2, 3, 4 };
    // Start is called before the first frame update
    void Start()
    {
        chemicalsDict.Add(0, "Calcium");
        chemicalsDict.Add(1, "Potassium");
        chemicalsDict.Add(2, "Lithium");
        chemicalsDict.Add(3, "Copper");
        chemicalsDict.Add(4, "Sodium");

        chemicalsColor.Add(0, "Red-Orange");
        chemicalsColor.Add(1, "Lilac");
        chemicalsColor.Add(2, "Crimson");
        chemicalsColor.Add(3, "Green");
        chemicalsColor.Add(4, "Orange");

        dialogueRunner = FindObjectOfType<DialogueRunner>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    public void InstantiateQuizElements()
    {
        chemicalsArray = ShuffleArray(chemicalsArray);

        for (int i = 0; i < chemicalsArray.Length; i++)
        {
            GameObject solution = Instantiate(beakerPrefabs[chemicalsArray[i]], beakerTransforms[i]);
            solution.tag = "Solution";
            GameObject label = Instantiate(labelPrefabs[i], labelTransforms[i]);
            label.tag = "Solution Label";
            
        }
    }

    public void ResetQuiz ()
    {
        mainCamera.GetComponent<OVRScreenFade>().FadeOut();
        resultDescription = "";
        dialogueRunner.Stop();
        GameObject[] solutionsArray = GameObject.FindGameObjectsWithTag("Solution");
        GameObject[] solutionLabels = GameObject.FindGameObjectsWithTag("Solution Label");

        for (int i = 0;i < solutionsArray.Length;i++)
        {
            Destroy(solutionsArray[i]);
            Destroy(solutionLabels[i]);
        }

        InstantiateQuizElements();
        nichromeWire.transform.position = nichromeWireTransform.position;
        resultsTitle.SetText("Quiz Time");
        resultsTitle.fontSize = 50;
        resultsBody.SetText("To test your new skills the beakers are now rearranged and labelled A to E.<br><br>Repeat the test as you have been shown and record which chemical you think is in each beaker on the iPad provided.<br><br>Click Submit when you have completed the 5 tests to see your score. Good luck!!");
        resultsBody.fontSize = 45;
        mainCamera.GetComponent<OVRScreenFade>().FadeIn();
        dialogueRunner.StartDialogue("Step4");
    }

    public async void SubmitAnswers()
    {
        for (int i = 0;i < dropDowns.Length;i++)
        {
            if (dropDowns[i].value == chemicalsArray[i])
            {
                correctAnswers++;
                resultDescription += "Well Done!! You correctly identified " + chemicalsDict[chemicalsArray[i]] + " ions which burn with a " + chemicalsColor[chemicalsArray[i]] + " colour. <br><br>";
            } else
            {
                resultDescription += "You misidentified " + chemicalsDict[chemicalsArray[i]] + " ions as " + chemicalsDict[dropDowns[i].value] + " ions. Remember " + chemicalsDict[dropDowns[i].value] + " ions burn with a " + chemicalsColor[dropDowns[i].value] + " colour. <br><br>";
            }
        }
        airtableManager.totalCorrect = correctAnswers.ToString();
        airtableManager.totalWrong = (5 - correctAnswers).ToString();
        airtableManager.score = ((correctAnswers / 5f) * 100f).ToString();
        airtableManager.dateTime = DateTime.Now.ToString("dd-mm-yy HH:mm");

        try
        {
            await airtableManager.CreateRecordAsync();
        }
        catch (Exception e)
        {
            Debug.LogError("Error while creating record: " + e.Message);
        }

        resultsTitle.SetText("Your Results");
        resultsBody.SetText(resultDescription);
        resultsBody.fontSize = 30f;
        resetButton.SetActive(true);

    }

    // method to shuffle the array
    int[] ShuffleArray(int[] array)
    {
        System.Random rng = new System.Random();
        int n = array.Length;
        int[] shuffledArray = new int[n];
        array.CopyTo(shuffledArray, 0);

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = shuffledArray[k];
            shuffledArray[k] = shuffledArray[n];
            shuffledArray[n] = value;
        }

        return shuffledArray;
    }
}
