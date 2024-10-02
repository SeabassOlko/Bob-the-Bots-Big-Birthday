using UnityEngine;

public class PostTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem lightEffect; // Reference to the particle system for lighting the post

    private bool isLit = true;
    private PostManager postManager;

    private void Start()
    {
        if (lightEffect == null)
        {
            Debug.LogError("Light effect not assigned to post. Please assign it in the Inspector.");
            return;
        }

        postManager = FindObjectOfType<PostManager>();
        SetLit(true); // Initialize as lit
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isLit && collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Post hit by projectile!");
            SetLit(false); // Set the post to unlit
            postManager.OnPostShot(this); // Notify PostManager
        }
    }

    public void SetLit(bool lit)
    {
        isLit = lit;

        if (lightEffect != null)
        {
            if (lit)
            {
                if (!lightEffect.isPlaying) // Play only if not already active
                {
                    lightEffect.Play();
                }
            }
            else
            {
                lightEffect.Stop();
            }
        }
        else
        {
            Debug.LogWarning("Light effect is missing.");
        }
    }
}
