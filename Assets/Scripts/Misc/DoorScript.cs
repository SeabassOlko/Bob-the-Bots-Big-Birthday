using UnityEngine;

public class VerticalPressMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("character_nearby", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("character_nearby", false);
        }
    }
}
