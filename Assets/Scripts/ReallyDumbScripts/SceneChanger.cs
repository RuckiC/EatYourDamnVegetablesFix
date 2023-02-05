using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject CreditsMenu;
    public GameObject MainMenu;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        CreditsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Ended Gamee");
    }
}