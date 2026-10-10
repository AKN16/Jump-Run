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
        if (isGameOver) return; // tránh gọi nhiều lần

        isGameOver = true;
        GameOverUI.SetActive(true);
        score = 0;
        AudioManager.Instance.PlayGameOver(); // phát âm thua 1 lần
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
        isGameWin = false;
        score = 0;
        UpdateScore();
        SceneManager.LoadScene("Game");
    }

    public void Gotomenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
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