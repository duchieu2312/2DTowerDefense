using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private bool isAOE = false;
    [SerializeField] private float aoeRadius = 1.5f;
    [SerializeField] private int aoeDamage = 1;
    [SerializeField] private bool isSlowBullet = false;
    [SerializeField] private float slowAmount = 0.5f;
    [SerializeField] private float slowDuration = 2f;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        transform.up = direction;

        rb.linearVelocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAOE)
        {
            ApplyAOEDamage();
        }
        else
        {
            if (collision.gameObject.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(bulletDamage);
            }
        }

        if (isSlowBullet && collision.gameObject.TryGetComponent<EnemyMovement>(out EnemyMovement enemy))
        {
            enemy.ApplySlow(slowAmount, slowDuration);
        }

        Destroy(gameObject);
    }

    private void ApplyAOEDamage()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, aoeRadius);
        foreach (Collider2D enemy in enemiesHit)
        {
            if (enemy.TryGetComponent<Health>(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(aoeDamage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (isAOE)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aoeRadius);
        }
    }

}
