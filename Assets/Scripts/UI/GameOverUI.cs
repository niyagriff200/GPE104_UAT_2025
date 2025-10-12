using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI resultsText;

    private void Update()
    {
        if (GameManager.instance != null)
        {
            scoreText.text = "Score: " + GameManager.instance.score.ToString("0000");
            highScoreText.text = "High Score: " + GameManager.instance.topScore.ToString("0000");
        }
    }

    public void PlayAgain()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ShowGameplay();
        }
    }

    public void MainMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ShowMainMenu();
        }
    }

    public void ShowResults()
    {
        bool playerWon = false;

        // Check for win condition: all enemies defeated and enough were spawned
        if (GameManager.instance.activeEnemies.Count == 0 && GameManager.instance.initialEnemiesSpawned >= GameManager.instance.enemyCount)
        {
            playerWon = true;
        }

        // Check for loss condition: player pawn is missing
        if (GameManager.instance.players.Count > 0 && GameManager.instance.players[0].pawn == null)
        {
            playerWon = false;
        }

        // Update the title text based on the result
        if (playerWon)
        {
            resultsText.text = "You Win!";
        }
        else
        {
            resultsText.text = "You Lose!";
        }
    }
}
