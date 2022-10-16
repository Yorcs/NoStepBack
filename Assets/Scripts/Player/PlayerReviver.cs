using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerReviver : MonoBehaviour {
    private Player player;
    // Start is called before the first frame update
    void Start() {
        player = GetComponentInParent<Player>();
        Assert.IsNotNull(player);
    }

    public void RevivePlayer() {
        player.Revive();
    }
}
