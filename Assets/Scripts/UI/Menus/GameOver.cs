using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void GameOverOn()
    {
        gameObject.SetActive(true);
    }

    public void NotGameOver()
    {
        gameObject.SetActive(false);
    }
}
