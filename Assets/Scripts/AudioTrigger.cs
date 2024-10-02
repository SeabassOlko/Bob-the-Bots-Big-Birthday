using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip enterAudioClip; // Audio clip to play on trigger enter
    [SerializeField] private AudioClip exitAudioClip; // Audio clip to play on trigger exit
    [SerializeField] private float volume = 1.0f; // Volume of the audio

  

    private void OnTriggerEnter(Collider other)
    {

            // Check if the collided object has a specific tag (optional)
            if (other.CompareTag("Player")) // You can change "Player" to any tag you want
            {
                PlayAudio(enterAudioClip);

            }
 
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player")) // Adjust as necessary
        {
            PlayAudio(exitAudioClip);
          
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            Debug.Log($"Audio clip played: {clip.name}");
        }
        else
        {
            Debug.LogWarning("Audio clip is not assigned.");
        }
    }
}
