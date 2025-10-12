using UnityEngine;

// Applies temporary camera shake effect on damage
public class CameraShake : MonoBehaviour
{
    private float shakeIntensity;
    private float shakeDuration;

    private Vector3 originalPosition;
    private float timer;

    private void Start()
    {
        shakeIntensity = GameManager.instance.shakeIntensity;
        shakeDuration = GameManager.instance.shakeDuration;
        originalPosition = transform.position;
    }

    public void Shake()
    {
        timer = shakeDuration;
    }

    private void Update()
    {
        if (timer > 0)
        {
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;
            timer -= Time.deltaTime;
        }
        else
        {
            transform.position = originalPosition;
        }
    }
}
