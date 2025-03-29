using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject levelTextObject;
    [SerializeField] private GameObject bpsTextObject;
    [SerializeField] private GameObject rangeTextObject;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private AudioSource shootSoundEffect;
    [SerializeField] private AudioSource upgradeSoundEffect;
    [SerializeField] private AudioSource sellSoundEffect;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bulletsPerSecond = 1f;
    [SerializeField] private int baseUpgradeCost = 100;

    private Transform target;
    private float timeUntilFire;
    private Text levelText;
    private Text rangeText;
    private Text bpsText;

    private int level = 0;

    private void Start()
    {
        upgradeButton.onClick.AddListener(Upgrade);
        sellButton.onClick.AddListener(SellTurret);

        levelText = levelTextObject.GetComponent<Text>();
        rangeText = rangeTextObject.GetComponent<Text>();
        bpsText = bpsTextObject.GetComponent<Text>();
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        } else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bulletsPerSecond)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }

        if (upgradeUI != null)
        {
            upgradeUI.transform.rotation = Quaternion.identity;
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
        shootSoundEffect.Play();
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position,transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OpenUpgradeUI ()
    {
        upgradeUI.SetActive(true);
        UpdateUI();
    }

    public void CloseUpgradeUI ()
    { 
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (level < 3 && LevelManager.main.SpendCurrency(CalculateCost()) == true)
        {
            level++;
            bulletsPerSecond+=0.2f;
            targetingRange+=0.2f;
            upgradeSoundEffect.Play();
        } else
        {
            return;
        }

        UpdateUI();
        CloseUpgradeUI();
    }

    private int CalculateCost()
    {
        if ( level == 0 )
        {
            return baseUpgradeCost;
        } else
        {
            return baseUpgradeCost + (level * 50);
        }
    }
    private void UpdateUI()
    {
        if (levelText) levelText.text = "Level: " + level;
        if (rangeText) rangeText.text = "Range: " + targetingRange.ToString("F1");
        if (bpsText) bpsText.text = "BPS: " + bulletsPerSecond.ToString("F1");

        if (level == 3) {
            Text buttonText = upgradeButton.GetComponentInChildren<Text>();
            buttonText.text = "Max Upgrade";
        } else
        {
            Text buttonText = upgradeButton.GetComponentInChildren<Text>();
            buttonText.text = "Upgrade (" + CalculateCost() + ")";
        }

        Text sellText = sellButton.GetComponentInChildren<Text>();
        sellText.text = "Sell (" + CalculateSellPrice() + ")";
    }
    public void SellTurret()
    {
        int sellPrice = CalculateSellPrice();
        LevelManager.main.IncreaseCurrency(sellPrice);
        sellSoundEffect.Play();

        CloseUpgradeUI();
        Destroy(gameObject, sellSoundEffect.clip.length);
    }
    private int CalculateSellPrice()
    {
        return (baseUpgradeCost + (level * 50)) - (level * 25);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
