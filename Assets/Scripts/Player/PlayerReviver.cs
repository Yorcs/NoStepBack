using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerReviver : MonoBehaviour {
    private PlayerStatus player;
    // Start is called before the first frame update
    void Start() {
        player = GetComponentInParent<PlayerStatus>();
        Assert.IsNotNull(player);
    }

    public void RevivePlayer() {
        player.Revive();
    }
}
