using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostManager : MonoBehaviour
{
    [SerializeField] private List<PostTrigger> posts; // List of all posts
    [SerializeField] private BossShield bossShield; // Reference to the boss shield
    [SerializeField] private float shieldTime; // Delay before turning the shield back on

    private List<PostTrigger> litPosts = new List<PostTrigger>();

    // Expose total posts shot to the Inspector for debugging
    [SerializeField] private int totalPostsShot = 0; // Total posts shot

    private void Start()
    {
        LightUpAllPosts(); // Initialize and light all posts at the start
    }

    private void LightUpAllPosts()
    {
        // Reset lit posts
        foreach (var post in litPosts)
        {
            post.SetLit(false); // Turn off the light effect for previously lit posts
        }
        litPosts.Clear(); // Clear the list of lit posts

        // Light up all posts
        foreach (var post in posts)
        {
            litPosts.Add(post);
            post.SetLit(true);
        }

        // Reset the total shot count for the next round
        totalPostsShot = 0; // Reset shot counter for new round
        Debug.Log("All posts lit. Total posts to shoot: " + posts.Count);
    }

    public void OnPostShot(PostTrigger post)
    {
        if (litPosts.Contains(post))
        {
            litPosts.Remove(post); // Remove the shot post from lit posts
            totalPostsShot++; // Increment the shot post count
            Debug.Log("Post shot! Total posts shot: " + totalPostsShot);

            // Check if all lit posts have been shot
            if (litPosts.Count == 0)
            {
                bossShield.TurnOffShield(); // Turn off the shield
                StartCoroutine(ActivateShieldDelay()); // Activate delay to turn it back on
            }
        }
    }

    private IEnumerator ActivateShieldDelay()
    {
        yield return new WaitForSeconds(shieldTime);
        bossShield.TurnOnShield(); // Turn the shield back on
        LightUpAllPosts(); // Light up all posts again
    }
}
