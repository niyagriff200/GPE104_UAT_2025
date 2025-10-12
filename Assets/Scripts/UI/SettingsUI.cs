using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public void Controls()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ShowControlsScreen();
        }
    }

    public void MainMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ShowMainMenu();
        }
    }
}
