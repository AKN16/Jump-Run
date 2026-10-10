using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            gameManager.AddScore(1);                    // cộng điểm trước
            AudioManager.Instance.PlayCoinSound();      // rồi mới phát âm
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Trap"))
        {
            gameManager.GameOver();
        }
        else if (collision.CompareTag("Enemy"))
        {
            gameManager.GameOver();
        }
        else if (collision.CompareTag("Key"))
        {
            gameManager.GameWin();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("River"))
        {
            gameManager.GameOver();
        }
    }
}