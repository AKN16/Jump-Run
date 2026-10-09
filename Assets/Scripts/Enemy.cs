using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float patrolDistance = 5f;

    [Header("Chase (chỉ theo chiều ngang)")]
    [SerializeField] private float detectRange = 4f;          // khoảng cách NGANG để phát hiện player
    [SerializeField] private float verticalTolerance = 1f;    // lệch chiều dọc tối đa, vượt quá = coi như player ở trên/dưới -> không đuổi
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private float maxChaseDistance = 8f;     // đuổi xa tối đa tính từ vị trí gốc
    [SerializeField] private float loseRangeMultiplier = 1.5f;// đang đuổi thì tầm mất dấu rộng hơn tầm phát hiện (tránh chớp tắt)
    [SerializeField] private float loseSightTime = 1f;        // mất dấu (player nhảy lên / ra xa) quá lâu -> bỏ cuộc quay về
    [SerializeField] private float reDetectCooldown = 2f;     // sau khi bỏ cuộc, chờ bao lâu mới được phát hiện lại
    [SerializeField] private float stopDistanceX = 0.3f;      // player ngay trên/dưới đầu thì đứng yên, không quay đi quay lại

    [Header("Turn")]
    [SerializeField] private float turnPauseTime = 0.8f;      // thời gian nghỉ trước khi đổi hướng (để player né)

    [Header("Ground Check (mép vực)")]
    [SerializeField] private Transform groundCheck;           // empty object đặt ở chân enemy
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private float groundCheckForward = 0.3f;

    [Header("Wall Check (vách)")]
    [SerializeField] private LayerMask wallLayer;             // để trống thì dùng groundLayer
    [SerializeField] private float wallCheckDistance = 0.1f;  // khoảng cách tới vách thì coi là chạm

    private Vector3 startPos;
    private float leftBound;
    private float rightBound;
    private bool movingRight = true;
    private Transform player;
    private Collider2D col;

    private enum State { Patrol, Chase, Return, Wait }
    private enum MoveResult { Moved, Wall, Edge }

    private State state = State.Patrol;
    private State stateAfterWait = State.Patrol;
    private bool pendingFacingRight;
    private float waitTimer;
    private float lostTimer;
    private float detectCooldown;

    private LayerMask WallMask => wallLayer.value != 0 ? wallLayer : groundLayer;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        startPos = transform.position;
        leftBound = startPos.x - patrolDistance;
        rightBound = startPos.x + patrolDistance;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (detectCooldown > 0f) detectCooldown -= Time.deltaTime;

        switch (state)
        {
            case State.Patrol: Patrol(); break;
            case State.Chase: Chase(); break;
            case State.Return: ReturnToStart(); break;
            case State.Wait: Wait(); break;
        }
    }

    // ───────────────────────── Patrol ─────────────────────────
    void Patrol()
    {
        CheckForPlayer();
        if (state != State.Patrol) return;

        MoveResult result = Move(movingRight ? Vector2.right : Vector2.left, speed);
        float x = transform.position.x;

        // Chạm vách / mép vực -> cắt luôn đầu mút bên đó tại vị trí hiện tại rồi quay đầu
        if (result != MoveResult.Moved)
        {
            if (movingRight) rightBound = x;
            else leftBound = x;

            TurnTo(!movingRight, State.Patrol);
            return;
        }

        if (movingRight && x >= rightBound) TurnTo(false, State.Patrol);
        else if (!movingRight && x <= leftBound) TurnTo(true, State.Patrol);
    }

    // ───────────────────────── Chase ─────────────────────────
    void CheckForPlayer()
    {
        if (player == null || detectCooldown > 0f) return;

        if (CanSeePlayer(detectRange))
        {
            lostTimer = 0f;
            state = State.Chase;
        }
    }

    // Chỉ "thấy" player khi cùng độ cao tương đối. Player đứng trên cao / đang nhảy -> không thấy.
    bool CanSeePlayer(float horizontalRange)
    {
        Vector2 d = player.position - transform.position;
        return Mathf.Abs(d.x) <= horizontalRange && Mathf.Abs(d.y) <= verticalTolerance;
    }

    void Chase()
    {
        if (player == null) { state = State.Return; return; }

        // Đuổi quá xa vị trí gốc -> bỏ cuộc
        if (Mathf.Abs(transform.position.x - startPos.x) >= maxChaseDistance)
        {
            GiveUpChase();
            return;
        }

        // Player nhảy lên cao / ra xa: đứng yên, không đuổi theo chiều dọc. Quá lâu thì bỏ cuộc.
        if (!CanSeePlayer(detectRange * loseRangeMultiplier))
        {
            lostTimer += Time.deltaTime;
            if (lostTimer >= loseSightTime) GiveUpChase();
            return;
        }
        lostTimer = 0f;

        float dx = player.position.x - transform.position.x;
        if (Mathf.Abs(dx) < stopDistanceX) return; // player ngay đầu/chân, đứng yên

        bool playerIsRight = dx > 0f;

        // Đổi hướng: nghỉ một nhịp trước khi quay đầu
        if (playerIsRight != movingRight)
        {
            TurnTo(playerIsRight, State.Chase);
            return;
        }

        // Gặp vách / mép vực: bỏ cuộc, không đứng ì ở đó
        if (Move(playerIsRight ? Vector2.right : Vector2.left, chaseSpeed) != MoveResult.Moved)
            GiveUpChase();
    }

    void GiveUpChase()
    {
        detectCooldown = reDetectCooldown;
        lostTimer = 0f;
        state = State.Return;
    }

    // ───────────────────────── Return ─────────────────────────
    void ReturnToStart()
    {
        CheckForPlayer();
        if (state != State.Return) return;

        float dx = startPos.x - transform.position.x;

        if (Mathf.Abs(dx) < 0.1f)
        {
            transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
            state = State.Patrol;
            return;
        }

        bool startIsRight = dx > 0f;

        if (startIsRight != movingRight)
        {
            TurnTo(startIsRight, State.Return);
            return;
        }

        // Bị chặn trên đường về -> thôi, tuần tra tại chỗ
        if (Move(startIsRight ? Vector2.right : Vector2.left, speed) != MoveResult.Moved)
            state = State.Patrol;
    }

    // ───────────────────────── Wait (nghỉ trước khi quay đầu) ─────────────────────────
    void TurnTo(bool faceRight, State next)
    {
        pendingFacingRight = faceRight;
        stateAfterWait = next;
        waitTimer = turnPauseTime;
        state = State.Wait;
    }

    void Wait()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer > 0f) return;

        // Đang đuổi: hết nghỉ thì quay về phía player hiện tại (phòng khi player đã đổi chỗ)
        if (stateAfterWait == State.Chase && player != null)
            pendingFacingRight = player.position.x > transform.position.x;

        movingRight = pendingFacingRight;
        Flip();
        state = stateAfterWait;
    }

    // ───────────────────────── Move / Check ─────────────────────────
    MoveResult Move(Vector2 direction, float moveSpeed)
    {
        if (IsWallAhead(direction)) return MoveResult.Wall;
        if (!HasGroundAhead(direction)) return MoveResult.Edge;

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        return MoveResult.Moved;
    }

    bool IsWallAhead(Vector2 direction)
    {
        Bounds b = col != null ? col.bounds : new Bounds(transform.position, Vector3.one * 0.5f);

        // Hộp mỏng cao bằng ~80% thân, bắt đầu từ giữa người -> không dính mặt đất dưới chân
        Vector2 size = new Vector2(0.05f, b.size.y * 0.8f);
        float dist = b.extents.x + wallCheckDistance;

        return Physics2D.BoxCast(b.center, size, 0f, direction, dist, WallMask);
    }

    bool HasGroundAhead(Vector2 direction)
    {
        if (groundCheck == null) return true;

        Vector2 checkPos = (Vector2)groundCheck.position + direction * groundCheckForward;
        return Physics2D.Raycast(checkPos, Vector2.down, groundCheckDistance, groundLayer);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (movingRight ? 1f : -1f);
        transform.localScale = scale;
    }

    // ───────────────────────── Gizmos ─────────────────────────
    void OnDrawGizmosSelected()
    {
        Vector3 s = Application.isPlaying ? startPos : transform.position;

        // Vùng phát hiện: hộp ngang x dọc (không phải hình tròn)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(detectRange * 2f, verticalTolerance * 2f, 0f));

        // Khoảng tuần tra (đỏ). Khi chạy game, đầu mút bị cắt sẽ co lại theo thực tế
        float l = Application.isPlaying ? leftBound : s.x - patrolDistance;
        float r = Application.isPlaying ? rightBound : s.x + patrolDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(l, s.y, s.z), new Vector3(r, s.y, s.z));
    }
}