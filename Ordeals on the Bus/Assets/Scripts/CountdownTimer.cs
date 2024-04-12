using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float totalTime = 60f; // Total time for countdown in seconds
    private float timeRemaining; // Time remaining for countdown
    private bool isCountingDown = false; // Flag to check if countdown is active

    public Text countdownText; // Text component to display countdown
    public GameObject objectToTurnOff; // GameObject to turn off when countdown finishes
    public AudioSource audioSource; // AudioSource component for playing sound

    private bool isSoundPlayed = false; // Flag to check if the sound has been played

    void Start()
    {
        gameObject.SetActive(false);
        timeRemaining = totalTime; // Initialize time remaining
        UpdateTimerDisplay(); // Update the timer display initially
    }

    void Update()
    {
        if (isCountingDown)
        {
            // Countdown the timer
            timeRemaining -= Time.deltaTime;

            // Update the timer display
            UpdateTimerDisplay();

            // Check if the countdown has finished
            if (timeRemaining <= 0)
            {
                timeRemaining = 0; // Ensure timer doesn't go negative
                isCountingDown = false; // Stop the countdown
                Debug.Log("Countdown Finished!"); // Output a message
                TurnGameObjectOff(); // Turn off the specified GameObject

                // Play the sound if it hasn't been played yet
                if (!isSoundPlayed && audioSource != null)
                {
                    audioSource.Play();
                    isSoundPlayed = true;
                }
            }
        }
    }

    // Start the countdown
    public void StartCountdown()
    {
        gameObject.SetActive(true);
        isCountingDown = true;
    }

    // Update the timer display
    void UpdateTimerDisplay()
    {
        // Ensure the timer doesn't display negative values
        if (timeRemaining < 0)
        {
            timeRemaining = 0;
        }

        // Format the time remaining as minutes and seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        // Display "00:00" when countdown finishes
        if (minutes <= 0 && seconds <= 0)
        {
            countdownText.text = "00:00";
        }
        else
        {
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            // Update the UI text
            countdownText.text = timeString;
        }
    }

    // Turn off the specified GameObject
    void TurnGameObjectOff()
    {
        if (objectToTurnOff != null)
        {
            objectToTurnOff.SetActive(false);
        }
    }
}