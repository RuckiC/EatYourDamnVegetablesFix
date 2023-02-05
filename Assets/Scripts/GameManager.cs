using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CandyBar candyBar;
    public GameObject gameOverText;
    public GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(candyBar.sugarHighTime <= 0)
        {
            pauseGame();
            gameOverText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                restartGame();
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


}
