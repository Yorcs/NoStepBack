using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager instance;
    private GameFlowManager gameFlowManager;

    private List<IEnemy> enemies = new List<IEnemy>();

    [SerializeField] private EnemyHealthBarUI healthUI;

    [SerializeField] private PickupFactory pickupFactory;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        gameFlowManager = FindObjectOfType<GameFlowManager>();
        Assert.IsNotNull(enemies);
        Assert.IsNotNull(pickupFactory);
        Assert.IsNotNull(gameFlowManager);
    }

    public void AddEnemies(List<IEnemy> newEnemies) {
        enemies.AddRange(newEnemies);
    }

    public void RemoveEnemy(IEnemy enemy) {
        enemies.Remove(enemy);
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

    public Vector3 FindClosestVisibleEnemy(Vector3 position, Vector2 direction) {
        Vector3 result = Vector3.positiveInfinity;
        Debug.Log(enemies.Count);

        foreach(IEnemy enemy in enemies) {
            if(enemy.GetPosition().x * direction.x > position.x * direction.x) {
                if(Mathf.Abs((enemy.GetPosition() - position).magnitude) < 
                Mathf.Abs((result - position).magnitude)) {
                    result = enemy.GetPosition();
                }
            }
        }


        Debug.Log(result);
        return result;
    }
}
