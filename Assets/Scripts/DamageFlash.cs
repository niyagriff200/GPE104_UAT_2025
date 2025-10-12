using UnityEngine;

// Flashes sprite color briefly on damage
public class DamageFlash : MonoBehaviour
{
    private Color flashColor;
    private Color normalColor;
    private float flashDuration;

    private SpriteRenderer spriteRenderer;
    private bool isFlashing = false;
    private float flashTimer = 0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = GameManager.instance.normalColor;
        }

        flashColor = GameManager.instance.flashColor;
        normalColor = GameManager.instance.normalColor;
        flashDuration = GameManager.instance.flashDuration;
    }

    private void Update()
    {
        if (isFlashing)
        {
            flashTimer -= Time.deltaTime;

            if (flashTimer <= 0f)
            {
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = normalColor;
                }

                isFlashing = false;
            }
        }
    }

    public void Flash()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = flashColor;
            flashTimer = flashDuration;
            isFlashing = true;
        }
    }
}
