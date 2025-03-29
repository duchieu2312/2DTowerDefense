using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    [Header("References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private AudioSource gameOverSoundEffect;
    [SerializeField] private AudioSource notEnoughMoneySoundEffect;

    public int currency;

    private void Awake()
    {
       main = this;
    }

    private void Start()
    {
        currency = 100;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        } else
        {
            Debug.Log("You do not have enough to purchase this item");
            notEnoughMoneySoundEffect.Play();
            return false;
        }
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverSoundEffect.Play();
        Time.timeScale = 0f;
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
