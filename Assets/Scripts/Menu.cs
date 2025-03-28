using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Text currencyUI;

    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
    }
}
