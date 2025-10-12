using UnityEngine;

public class ControlsUI : MonoBehaviour
{
    public void Settings()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ShowSettingsScreen();
        }
    }
}
