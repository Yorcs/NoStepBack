using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager instance;
    private GameFlowManager gameFlowManager;

    private List<IEnemy> enemies = new List<IEnemy>();
    private List<IEnemy> activeEnemies = new List<IEnemy>();

    private List<PlayerStatus> players = new ();

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

    public void AddEnemyToActive(IEnemy newEnemy) {
        activeEnemies.Add(newEnemy);
    }

    public void RemoveEnemy(IEnemy enemy) {
        enemies.Remove(enemy);
        activeEnemies.Remove(enemy);
    }

    public void RemoveEnemyFromActive(IEnemy enemy) {
        activeEnemies.Remove(enemy);
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

        foreach(IEnemy enemy in activeEnemies) {
            Vector3 enemyPos = enemy.GetPosition();
            if(enemy.IsActive() && IsInDirection(position, enemyPos, direction) && IsOnscreen(enemyPos)) {
                if(IsCloser(position, enemyPos, result)) {
                    RaycastHit2D hit = Physics2D.Raycast(position, enemyPos - position, Mathf.Infinity, LayerMask.GetMask("Ground", "Enemies", "Walls"));
                    if(!hit.collider.gameObject.CompareTag("Ground") && !hit.collider.gameObject.CompareTag("Walls") && !hit.collider.gameObject.CompareTag("PassableGround")) {
                        result = enemyPos;
                    }
                }
            }
        }
        return result;
    }

    public Vector3 FindClosestVisiblePlayer(Vector3 position, Vector2 direction) {
        Vector3 result = Vector3.positiveInfinity;

        foreach(PlayerStatus player in players) {
            Vector3 enemyPos = player.GetPosition();
            if(!player.IsDead() && IsInDirection(position, enemyPos, direction)) {
                if(IsCloser(position, enemyPos, result)) {
                    RaycastHit2D hit = Physics2D.Raycast(position, enemyPos - position, Mathf.Infinity, LayerMask.GetMask("Ground", "Players", "Walls"));
                    if(!hit.collider.gameObject.CompareTag("Ground") && !hit.collider.gameObject.CompareTag("Walls") && !hit.collider.gameObject.CompareTag("PassableGround")) {
                        result = enemyPos;
                    }
                }
            }
        }
        return result;
    }

    public Vector3 FindClosestPlayer(Vector3 position) {
        Vector3 result = Vector3.positiveInfinity;

        foreach(PlayerStatus player in players) {
            Vector3 enemyPos = player.GetPosition();
            if(IsCloser(position, enemyPos, result)) {
                result = enemyPos;
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

    public void OnPlayerJoined(PlayerInput input) {
        PlayerStatus player = input.gameObject.GetComponent<PlayerStatus>();
        Assert.IsNotNull(player);
        players.Add(player);
    }
}
