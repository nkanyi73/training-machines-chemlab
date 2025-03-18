using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text;
using UnityEngine;


public class AirtableManager : MonoBehaviour
{

    [Header("Scripts")]
    // Reference to the AirtableSceneController script
    //public AirtableSceneController airtableSceneController;

    [Header("Airtable Elements")]
    // Airtable API endpoint and authentication details
    public string airtableEndpoint = "https://api.airtable.com/v0/";
    public string accessToken = "YOUR_ACCESS_TOKEN";
    public string baseId = "YOUR_BASE_ID";
    public string tableName = "YOUR_TABLE_NAME";
    private string dataToParse;

    [Header("Data For Airtable")]
    // Data fields for recording information
    public string dateTime;
    public string uuid;
    public string totalCorrect;
    public string totalWrong;
    public string score;

    [Header("Data From Airtable")]
    // Data fields for retrieving information from Airtable
    public string dataToLoad;
    public string lastRecordID;
    public string playerNameFromAirtable;
    public string volumeFromAirtable;
    public string coinsFromAirtable;
    public string timePlayedFromAirtable;
    public string healthFromAirtable;
    public string scoreFromAirtable;

    private void Start()
    {
        uuid = Guid.NewGuid().ToString();
    }
    // Method to create a new record in Airtable
    public void CreateRecord()
    {
        // Reset dataToLoad if it is not null
        if (dataToLoad != null)
        {
            dataToLoad = null;
        }

        // Get the current date and time
        dateTime = DateTime.Now.ToString("dd.MM.yyyy");
        uuid = Guid.NewGuid().ToString();

        // Create the URL for the API request
        string url = airtableEndpoint + baseId + "/" + tableName;

        // Create JSON data for the API request
        string jsonFields = "{\"fields\": {" +
                                    "\"Date\":\"" + dateTime + "\", " +
                                    "\"UUID\":\"" + uuid + "\", " +
                                    "\"Total Correct\":\"" + totalCorrect + "\", " +
                                    "\"Total Wrong\":\"" + totalWrong + "\", " +
                                    "\"Score\":\"" + score + "\"" +
                                    "}}";

        // Start the coroutine to send the API request
        StartCoroutine(SendRequest(url, "POST", response =>
        {
            // Log the response from the API
            Debug.Log("Record created: " + response);

            // Store the response for parsing
            dataToParse = response;

            // Parse the JSON response
            JSONParse();
        }, jsonFields));
    }

    // Coroutine to make API requests
    private IEnumerator SendRequest(string url, string method, Action<string> callback, string jsonData = "")
    {
        // Create a HTTP web request
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = method;
        request.ContentType = "application/json";
        request.Headers["Authorization"] = "Bearer " + accessToken;

        // Include JSON data in the request if provided
        if (!string.IsNullOrEmpty(jsonData))
        {
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(jsonData);
            }
        }

        // Get the response from the API
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = reader.ReadToEnd();
                // Invoke the callback with the response
                if (callback != null)
                {
                    callback(jsonResponse);
                }
            }
        }

        // Yield to the next frame
        yield return null;
    }

    // Method to retrieve a specific record from Airtable
    public void GetRecordValue(string recordID)
    {
        // Call the RetrieveRecord method with provided record ID and table name
        RetrieveRecord(recordID, tableName);
    }

    // Method to retrieve a record from Airtable based on record ID
    public void RetrieveRecord(string recordId, string readTableName)
    {
        // Create the URL for the API request
        string url = airtableEndpoint + baseId + "/" + readTableName + "/" + recordId;

        // Start the coroutine to send the API request
        StartCoroutine(SendRequest(url, "GET", response =>
        {
            // Parse the JSON response
            var responseObject = JsonUtility.FromJson<Dictionary<string, object>>(response);

            // Store the response for parsing
            dataToParse = response;

            // Parse the JSON response
            JSONParse();
        }));
    }

    // Method to parse the JSON response from Airtable
    public void JSONParse()
    {
        // Get the JSON source data
        string source = dataToParse;

        // Parse the JSON using Newtonsoft.Json
        dynamic data = JObject.Parse(source);

        // Extract the record ID from the parsed JSON
        lastRecordID = data.id;

        // Update the UI with the record ID
        //airtableSceneController.recordIDTMP.text = "Record ID: " + lastRecordID;

        // Log the last record ID
        Debug.Log("Last RecordID was: " + data.id);

        // Extract and display data based on the specified dataToLoad value
        if (dataToLoad == "PlayerName")
        {
            playerNameFromAirtable = data.fields.PlayerName;
            //airtableSceneController.playerNameFeedback.text = playerNameFromAirtable;
            Debug.Log("From Airtable: Player Name: " + playerNameFromAirtable);
        }

        if (dataToLoad == "Volume")
        {
            volumeFromAirtable = data.fields.Volume;
            //airtableSceneController.volumeFeedback.text = volumeFromAirtable;
            Debug.Log("From Airtable: Volume Data: " + volumeFromAirtable);
        }

        if (dataToLoad == "PlayerData")
        {
            playerNameFromAirtable = data.fields.PlayerName;
            volumeFromAirtable = data.fields.Volume;
            //airtableSceneController.playerNameFeedback.text = playerNameFromAirtable;
            //airtableSceneController.volumeFeedback.text = volumeFromAirtable;
            Debug.Log("From Airtable: Player Name: " + playerNameFromAirtable + ". Volume Data: " + volumeFromAirtable);
        }

        if (dataToLoad == "GameData")
        {
            coinsFromAirtable = data.fields.Coins;
            timePlayedFromAirtable = data.fields.TimePlayed;
            healthFromAirtable = data.fields.Health;
            scoreFromAirtable = data.fields.Score;

            //airtableSceneController.coinDataFeedback.text = coinsFromAirtable;
            //airtableSceneController.timePlayedFeedback.text = timePlayedFromAirtable;
            //airtableSceneController.healthDataFeedback.text = healthFromAirtable;
            //airtableSceneController.scoreDataFeedback.text = scoreFromAirtable;

            Debug.Log("From Airtable: Game Data: Coins: " + coinsFromAirtable + " Time Played: " + timePlayedFromAirtable +
                      " Health Data: " + healthFromAirtable + " Score Data: " + scoreFromAirtable);
        }
    }

    public async Task CreateRecordAsync()
    {
        // Reset dataToLoad if it is not null
        if (dataToLoad != null)
        {
            dataToLoad = null;
        }

        // Get the current date and time
        dateTime = DateTime.Now.ToString("dd.MM.yyyy");
        uuid = Guid.NewGuid().ToString();

        // Create the URL for the API request
        string url = airtableEndpoint + baseId + "/" + tableName;

        // Create JSON data for the API request
        string jsonFields = "{\"fields\": {" +
                                    "\"Date\":\"" + dateTime + "\", " +
                                    "\"UUID\":\"" + uuid + "\", " +
                                    "\"Total Correct\":\"" + totalCorrect + "\", " +
                                    "\"Total Wrong\":\"" + totalWrong + "\", " +
                                    "\"Score\":\"" + score + "\"" +
                                    "}}";

        try
        {
            // Send the API request asynchronously
            string response = await SendRequestAsync(url, HttpMethod.Post, jsonFields);

            // Log the response from the API
            Debug.Log("Record created: " + response);

            // Store the response for parsing
            dataToParse = response;

            // Parse the JSON response
            JSONParse();
        }
        catch (Exception e)
        {
            Debug.LogError("Error while creating record: " + e.Message);
        }
    }

    private async Task<string> SendRequestAsync(string url, HttpMethod method, string jsonData)
    {
        using (HttpClient client = new HttpClient())
        {
            // Set authorization header
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            // Create the request message
            var requestMessage = new HttpRequestMessage(method, url);

            // Include JSON data in the request if provided
            if (!string.IsNullOrEmpty(jsonData))
            {
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            }

            // Send the request asynchronously
            HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);

            // Ensure the request was successful
            responseMessage.EnsureSuccessStatusCode();

            // Read the response content as a string
            return await responseMessage.Content.ReadAsStringAsync();
        }
    }
}
