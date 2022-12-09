using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private List<Score> ScorePanels;
    private int scoreIndex = 0;

    public Score GetScoreDisplay(PlayerStatus status, PlayerActions action)
    {
        Score scorePanel = ScorePanels[scoreIndex];
        scorePanel.SetPlayerStatus(status, action);
        scoreIndex ++;
        return scorePanel;
    }
}
