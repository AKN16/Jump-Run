using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject GameWinUI;
    private bool isGameOver = false;
    private bool isGameWin = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScore();
        GameOverUI.SetActive(false);
        GameWinUI.SetActive(false);
    }

    public void AddScore(int points)
    {
        if (!isGameOver && !isGameWin)
        {
            score += points;
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        isGameOver = true;
        GameOverUI.SetActive(true);
        score = 0;
        Time.timeScale = 0;
    }

    public void GameWin()
    {
        isGameWin = true;
        GameWinUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isGameOver = false;
        score = 0;
        UpdateScore();
        SceneManager.LoadScene("Game");
    }

    public void Gotomenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }
}