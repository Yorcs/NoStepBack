using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyManager : MonoBehaviour {

    private List<IEnemy> enemies = new List<IEnemy>();

    [SerializeField] private PickupFactory pickupFactory;
    // Start is called before the first frame update
    void Start() {
        Assert.IsNotNull(enemies);
        Assert.IsNotNull(pickupFactory);

        enemies.AddRange(GetComponentsInChildren<IEnemy>());

        
    }

    public void LootDrop(Vector2 position) {
        pickupFactory.CreatePickup(position);
    }
}
