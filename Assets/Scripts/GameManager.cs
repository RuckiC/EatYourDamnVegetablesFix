using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CandyBar candyBar;
    public DinnerBar dinnerBar;
    public GameObject gameOverScreen;
    public GameObject winScreen;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
       if(candyBar.sugarHighTime <= 0)
        {
            pauseGame();
            gameOverScreen.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                restartGame();
            }
        } 

       if (dinnerBar.foodGoesColdTime <= 0)
        {
            pauseGame();
            winScreen.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quitGame()
    {
        Application.Quit();
    }


}
