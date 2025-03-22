using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentTimer : MonoBehaviour
{
    public static PersistentTimer Instance;
    public float timerDuration = 120f; // 2 minutes
    private float timer;
    private bool isTimerRunning = false; // Timer won't start automatically
    private Camera mainCamera;

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                TimerFinished();
            }
        }
    }

    void TimerFinished()
    {
        isTimerRunning = false;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (mainCamera != null)
        {
            mainCamera.GetComponent<OVRScreenFade>().FadeOut();
        }
        SceneManager.LoadScene(0);
        ResetTimer();
    }

    public void ResetTimer()
    {
        timer = timerDuration;
        isTimerRunning = false; // Timer stops until manually started again
    }

    public void StartTimer()
    {
        timer = timerDuration;
        isTimerRunning = true;
    }
}
