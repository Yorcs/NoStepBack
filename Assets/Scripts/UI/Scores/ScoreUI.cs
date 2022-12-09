using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private GameObject scoreUIPrefab;

    public Score MakeScoreDisplay(PlayerStatus status, PlayerActions action)
    {
        GameObject go = Instantiate(scoreUIPrefab);
        go.transform.SetParent(transform, false);
        Score score = go.GetComponent<Score>();
        // score.SetTotalScore();
        Assert.IsNotNull(score);
        score.SetPlayerStatus(status, action);
        

        return score;
    }
}
