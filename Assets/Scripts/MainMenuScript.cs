using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject creditPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OpenCredits()
    {
        creditPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}