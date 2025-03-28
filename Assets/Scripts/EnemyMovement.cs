using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    private float baseMoveSpeed;

    void Start()
    {
        target = LevelManager.main.path[pathIndex];
        baseMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                LevelManager.main.GameOver();
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            } else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }

    public void ApplySlow(float slowAmount, float duration)
    {
        if (moveSpeed == baseMoveSpeed)
        {
            moveSpeed *= slowAmount;
        }
        StopAllCoroutines();
        StartCoroutine(RemoveSlow(duration));
    }

    private IEnumerator RemoveSlow(float duration)
    {
        yield return new WaitForSeconds(duration);
        moveSpeed = baseMoveSpeed;
    }
}
