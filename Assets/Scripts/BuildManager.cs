using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;
    [SerializeField] private Button[] turretButtons;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color normalColor = Color.white;

    private int selectedTower = 0;
    private Button selectedButton;

    private void Awake()
    {
        main = this;
        UpdateButtonSelection();
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
        UpdateButtonSelection();
    }

    private void UpdateButtonSelection()
    {
        foreach (var btn in turretButtons)
        {
            btn.targetGraphic.color = normalColor;
        }

        selectedButton = turretButtons[selectedTower];
        selectedButton.targetGraphic.color = selectedColor;
    }
}
