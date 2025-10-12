using Unity.VisualScripting;
using UnityEngine;

public class SplashScreenUI : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown == true)
        {
            GameManager.instance.ShowMainMenu();
        }
    }
}
