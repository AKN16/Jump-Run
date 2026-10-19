using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint; // để trống thì dùng vị trí của chính object này
    private bool activated;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activated || !other.CompareTag("Player")) return;

        activated = true;
        Vector3 pos = respawnPoint != null ? respawnPoint.position : transform.position;
        other.GetComponent<PlayerRespawn>().SetCheckpoint(pos);
    }
}