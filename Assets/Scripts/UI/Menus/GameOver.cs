using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] Image gameOverScreen;
    public void GameOverOn()
    {
        gameOverScreen.gameObject.SetActive(true);
    }

    public void NotGameOver()
    {
        gameOverScreen.gameObject.SetActive(false);
    }
}
