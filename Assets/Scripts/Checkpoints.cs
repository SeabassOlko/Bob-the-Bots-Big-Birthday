using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private PlayerController player;  // Reference to the player
    [SerializeField] private HashSet<Collider> checkpoints = new HashSet<Collider>(); // Use HashSet to avoid duplicates
    private SaveLoad saveLoad;  // Reference to the SaveLoad script
    [SerializeField] private TextMeshProUGUI saveNotification; // UI Text to display the save notification
    [SerializeField] private float notificationDuration = 2f;

    void Start()
    {
        // Cache references if not set in the Inspector
        if (player == null)
        {
            player = FindAnyObjectByType<PlayerController>();
        }

        if (saveLoad == null)
        {
            saveLoad = FindAnyObjectByType<SaveLoad>();
        }
        if (saveNotification != null)
        {
            saveNotification.gameObject.SetActive(false);
        }
    }

    // Handles entering a checkpoint trigger
    void OnTriggerEnter(Collider other)
    {
        // Ensure that the object is a checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            // Add the checkpoint to the HashSet (no duplicates)
            if (checkpoints.Add(other))
            {
                SaveGame();  // Save the game when a new checkpoint is added
                Debug.Log("Game Saved at Checkpoint: " + other.name);
                StartCoroutine(ShowSaveNotification());
            }
        }
    }

    // Calls the save function from SaveLoad
    void SaveGame()
    {
        saveLoad.SaveGame();
    }

    IEnumerator ShowSaveNotification()
    {
        if (saveNotification != null)
        {
            // Show the notification
            saveNotification.gameObject.SetActive(true);

            // Optionally, you can set a message here, like "Game Saved!"
            saveNotification.text = "Game Saved!";

            // Wait for the specified duration
            yield return new WaitForSeconds(notificationDuration);

            // Hide the notification
            saveNotification.gameObject.SetActive(false);
        }
    }
}

