using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float patrolDistance = 5f;

    [Header("Chase")]
    [SerializeField] private float detectRange = 4f;       // player vào tầm này -> bắt đầu đuổi
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private float maxChaseDistance = 8f;  // đuổi xa tối đa bao nhiêu tính từ vị trí gốc

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;        // 1 empty object đặt ở chân enemy
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.3f;

    private Vector3 startPos;
    private bool movingRight = true;
    private Transform player;
    private Rigidbody2D rb;

    private enum State { Patrol, Chase, Return }
    private State state = State.Patrol;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        startPos = transform.position;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        switch (state)
        {
            case State.Patrol:
                Patrol();
                CheckForPlayer();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Return:
                ReturnToStart();
                break;
        }
    }

    void Patrol()
    {
        float leftBound = startPos.x - patrolDistance;
        float rightBound = startPos.x + patrolDistance;

        Move(movingRight ? Vector2.right : Vector2.left, speed);

        if (movingRight && transform.position.x >= rightBound)
        {
            movingRight = false;
            Flip();
        }
        else if (!movingRight && transform.position.x <= leftBound)
        {
            movingRight = true;
            Flip();
        }
    }

    void CheckForPlayer()
    {
        if (player == null) return;

        if (Vector2.Distance(transform.position, player.position) <= detectRange)
        {
            state = State.Chase;
        }
    }

    void Chase()
    {
        if (player == null) { state = State.Return; return; }

        float distFromStart = Mathf.Abs(transform.position.x - startPos.x);

        // Đuổi quá xa vị trí gốc -> quay về
        if (distFromStart >= maxChaseDistance)
        {
            state = State.Return;
            return;
        }

        bool playerIsRight = player.position.x > transform.position.x;
        Move(playerIsRight ? Vector2.right : Vector2.left, chaseSpeed);

        if (playerIsRight && !movingRight) { movingRight = true; Flip(); }
        else if (!playerIsRight && movingRight) { movingRight = false; Flip(); }
    }

    void ReturnToStart()
    {
        float distToStart = Mathf.Abs(transform.position.x - startPos.x);

        if (distToStart < 0.1f)
        {
            transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
            state = State.Patrol;
            return;
        }

        bool startIsRight = startPos.x > transform.position.x;
        Move(startIsRight ? Vector2.right : Vector2.left, speed);

        if (startIsRight && !movingRight) { movingRight = true; Flip(); }
        else if (!startIsRight && movingRight) { movingRight = false; Flip(); }
    }

    void Move(Vector2 direction, float moveSpeed)
    {
        if (groundCheck != null)
        {
            Vector2 checkPos = (Vector2)groundCheck.position + direction * 0.3f;
            bool hasGround = Physics2D.Raycast(checkPos, Vector2.down, groundCheckDistance, groundLayer);
            if (!hasGround) return; // ← nếu không thấy nền, DỪNG LUÔN, không di chuyển
        }
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (movingRight ? 1f : -1f);
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Vector3 s = Application.isPlaying ? startPos : transform.position;
        Gizmos.DrawLine(new Vector3(s.x - patrolDistance, s.y, s.z),
                         new Vector3(s.x + patrolDistance, s.y, s.z));
    }
}