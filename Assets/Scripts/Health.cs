using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;
    [SerializeField] private Slider healthBar;
    [SerializeField] private AudioSource deathSoundEffect;

    public int spawnWave = 1;
    private bool isDestroyed = false;
    private Animator animator;
    private Transform fill;

    private void Start()
    {
        animator = GetComponent<Animator>();
        fill = healthBar.transform.Find("Fill Area/Fill");

        if (healthBar != null)
        {
            healthBar.maxValue = hitPoints;
            healthBar.value = hitPoints;
        }
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (healthBar)
        {
            healthBar.value = hitPoints;
        }

        if (hitPoints <= 0 && !isDestroyed)
        {
            fill.gameObject.SetActive(false);
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            deathSoundEffect.Play();
            animator.SetTrigger("death");
        }
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }

}