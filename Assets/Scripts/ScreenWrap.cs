using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private void Update()
    {
        //Place on prefab that can wrap around screen
        if (GameManager.instance != null && GameManager.instance.gameplayState.activeInHierarchy)
        {
            GameManager.instance.ApplyScreenWrap(gameObject);
        }
    }
}
