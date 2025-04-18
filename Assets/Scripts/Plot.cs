using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    [SerializeField] private AudioSource placeTurretSoundEffect;
    [SerializeField] private AudioSource clickUpgradeTurretSoundEffect;

    private GameObject towerObj;
    public Turret turret;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI()) return;

        if (towerObj != null)
        {
            turret.OpenUpgradeUI();
            clickUpgradeTurretSoundEffect.Play();
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (LevelManager.main.SpendCurrency(towerToBuild.cost))
        {
            towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            turret = towerObj.GetComponent<Turret>();
            placeTurretSoundEffect.Play();
        }
    }
}
