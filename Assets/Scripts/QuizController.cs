using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

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

    private int[] chemicalsArray = new int[] { 0, 1, 2, 3, 4 };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void InstantiateQuizElements()
    {
        chemicalsArray = ShuffleArray(chemicalsArray);

        for (int i = 0; i < chemicalsArray.Length; i++)
        {
            Instantiate(beakerPrefabs[chemicalsArray[i]], beakerTransforms[i]);
            Instantiate(labelPrefabs[i], labelTransforms[i]);
            
        }
    }

    public void SubmitAnswers()
    {
        for (int i = 0;i < dropDowns.Length;i++)
        {
            if (dropDowns[i].value == chemicalsArray[i])
            {
                correctAnswers++;
            }
        }
        airtableManager.totalCorrect = correctAnswers.ToString();
        airtableManager.totalWrong = (5 - correctAnswers).ToString();
        airtableManager.score = ((correctAnswers / 5f) * 100f).ToString();
        airtableManager.dateTime = DateTime.Now.ToString("dd-mm-yy HH:mm");

        try
        {
            airtableManager.CreateRecord();
        }
        catch (Exception e)
        {
            Debug.LogError("Error while creating record: " + e.Message);
        }

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
