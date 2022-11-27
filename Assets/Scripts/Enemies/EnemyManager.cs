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

    private Camera mainCam;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        gameFlowManager = FindObjectOfType<GameFlowManager>();
        mainCam = Camera.main;

        Assert.IsNotNull(enemies);
        Assert.IsNotNull(pickupFactory);
        Assert.IsNotNull(gameFlowManager);
        Assert.IsNotNull(mainCam);
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
            Vector3 enemyPos = enemy.GetPosition();
            if(enemy.IsActive() && IsInDirection(position, enemyPos, direction) && IsOnscreen(enemyPos)) {
                if(IsCloser(position, enemyPos, result)) {
                    RaycastHit2D hit = Physics2D.Raycast(position, enemyPos - position, Mathf.Infinity, LayerMask.GetMask("Ground", "Enemies", "Walls"));
                    Debug.Log(hit.collider.gameObject.tag);
                    if(hit.collider.gameObject.CompareTag("Enemy")) {
                        result = enemy.GetPosition();
                    }
                }
            }
        }

        return result;
    }

    private bool IsInDirection(Vector3 startPosition, Vector3 targetPosition, Vector2 direction) {
        return targetPosition.x * direction.x > startPosition.x * direction.x;
    }
    

    private bool IsOnscreen(Vector3 position) {
        Vector3 screenPosition = mainCam.WorldToViewportPoint(position);
        return  screenPosition.x >= 0 && screenPosition.x <= 1 &&
                screenPosition.y >= 0 && screenPosition.y <= 1;
    }

    private bool IsCloser(Vector3 startPosition, Vector3 newTarget, Vector3 existingTarget) {
        return Mathf.Abs((newTarget - startPosition).magnitude) < 
               Mathf.Abs((existingTarget - startPosition).magnitude);
    }
}
