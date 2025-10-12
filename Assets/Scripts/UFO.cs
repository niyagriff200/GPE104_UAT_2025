using UnityEngine;

// Plays looping UFO hum sound with spatial effects
public class UFO : MonoBehaviour
{
    private AudioSource humSource;

    private void Start()
    {
        humSource = GetComponent<AudioSource>();
        humSource.clip = GameManager.instance.ufoSound;
        humSource.loop = true;
        humSource.spatialBlend = 1f;
        humSource.dopplerLevel = 1f;
        humSource.Play();
    }
}
