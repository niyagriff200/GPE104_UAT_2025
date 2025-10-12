using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    public void MainMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ShowMainMenu();
        }
    }
}
