using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject creditPanel;
    [SerializeField] private AudioSource clickSoundEffect;

    public void StartGame()
    {
        StartCoroutine(PlaySFXAndStartGame());
    }

    IEnumerator PlaySFXAndStartGame()
    {
        clickSoundEffect.Play();
        yield return new WaitForSeconds(clickSoundEffect.clip.length);
        SceneManager.LoadScene("Level 1");
    }

    public void OpenCredits()
    {
        creditPanel.SetActive(true);
        clickSoundEffect.Play();
    }

    public void CloseCredits()
    {
        creditPanel.SetActive(false);
        clickSoundEffect.Play();
    }

    public void QuitGame()
    {
        StartCoroutine(PlaySFXAndQuit());
    }

    IEnumerator PlaySFXAndQuit()
    {
        clickSoundEffect.Play();
        yield return new WaitForSeconds(clickSoundEffect.clip.length);
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}