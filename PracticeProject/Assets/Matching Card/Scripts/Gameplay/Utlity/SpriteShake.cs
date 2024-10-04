using System.Collections;
using UnityEngine;

public class SpriteShake : MonoBehaviour
{
    // Variables to control the shake effect
    public float shakeDuration = 0.5f; // Duration of the shake in seconds
    public float shakeMagnitude = 0.1f; // Magnitude of the shake

    private Vector3 originalPosition; // To store the sprite's original position

    void Start()
    {
        // Store the original position of the sprite
        originalPosition = transform.localPosition;
    }

    // Function to start shaking the sprite
    public void ShakeSprite()
    {
        StartCoroutine(Shake());
    }

    // Coroutine to handle the shake effect
    private IEnumerator Shake()
    {
        float elapsed = 0f;

        // Continue shaking for the duration specified
        while (elapsed < shakeDuration)
        {
            // Generate a random offset within the shake magnitude
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            // Apply the offset to the sprite's position
            transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            // Increment elapsed time
            elapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Return to the original position after shaking
        transform.localPosition = originalPosition;
    }
}
