using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class PlayerRespawnUI : MonoBehaviour
{
    [SerializeField] private GameObject respawnUIPrefab;

    public PlayerRespawner MakeRespawner(PlayerStatus players)
    {
        GameObject go = Instantiate(respawnUIPrefab);
        PlayerRespawner respawnCounter = go.GetComponent<PlayerRespawner>();
        Assert.IsNotNull(respawnCounter);

        return respawnCounter;
    }
}
