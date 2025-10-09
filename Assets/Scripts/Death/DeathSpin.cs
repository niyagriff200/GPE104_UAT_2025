using UnityEngine;

public class DeathSpin : Death
{
    private bool isDying = false; // Flag to trigger spin/scale effect
    private float timer = 0f;     // Tracks how long the effect has been running

    // Hardcoded to avoid cluttering GameManager (for visual purposes only)
    private float spinRate = 360f;     // Full rotation per second
    private float scaleRate = 0.5f;    // Shrinks by half per second
    private float duration = 1.5f;     // Total time before destruction

    public override void Die()
    {
        // Called when object reaches 0 health—starts visual death effect
        isDying = true;
    }

    private void Update()
    {
        if (!isDying) return;

        timer += Time.deltaTime;

        // Rotate for dramatic effect—used on final life only
        transform.Rotate(0f, 0f, spinRate * Time.deltaTime);

        // Shrink object to simulate fading out
        transform.localScale -= Vector3.one * scaleRate * Time.deltaTime;

        // Destroy when effect finishes or object is nearly invisible
        if (timer >= duration || transform.localScale.x <= 0.1f)
        {
            Destroy(gameObject); // Only used when lives = 0
        }
    }
}
