using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPos;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPos = transform.position; // mặc định: điểm xuất phát
    }

    public void SetCheckpoint(Vector3 pos) => respawnPos = pos;

    // Gọi hàm này khi player chết
    public void Respawn()
    {
        transform.position = respawnPos;
        if (rb != null) rb.linearVelocity = Vector2.zero; // Unity 6; bản cũ dùng rb.velocity
    }
}