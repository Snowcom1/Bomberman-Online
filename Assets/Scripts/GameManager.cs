using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;

    public void checkWinners()
    {
        int lifeCount = 0;

        foreach (GameObject player in players)
        {
            if(player.activeSelf)
            {
                lifeCount++;
            }
        }

        if(lifeCount <= 1)
        {
            nextRound();
        }
    }

     public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void nextRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
  
