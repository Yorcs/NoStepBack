using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {
    private GameFlowManager gameFlowManager;

    private List<IEnemy> enemies = new List<IEnemy>();

    [SerializeField] private EnemyHealthBarUI healthUI;

    [SerializeField] private PickupFactory pickupFactory;
    // Start is called before the first frame update
    void Start() {
        gameFlowManager = FindObjectOfType<GameFlowManager>();
        Assert.IsNotNull(enemies);
        Assert.IsNotNull(pickupFactory);
        Assert.IsNotNull(gameFlowManager);

        enemies.AddRange(GetComponentsInChildren<IEnemy>());
        

        
    }
    public void MoneyDrop(Vector2 position)
    {
        pickupFactory.CreateMoney(position);
    }

    public void LootDrop(Vector2 position) {
        pickupFactory.CreatePickup(position);
    }

    public EnemyHeart GetUI(IEnemy enemy) {
        EnemyHeart heart = healthUI.MakeHeart(enemy);
        return heart;
    }

    public void BossDefeated() {
        gameFlowManager.StartPVP();
    }
}
