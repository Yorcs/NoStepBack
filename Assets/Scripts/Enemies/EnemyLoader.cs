using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyLoader : MonoBehaviour {
    private EnemyManager enemyManager;

    private List<IEnemy> enemies = new List<IEnemy>();

    void Start() {
        enemyManager = EnemyManager.instance;
        Assert.IsNotNull(enemyManager);

        enemies.AddRange(GetComponentsInChildren<IEnemy>());

        enemyManager.AddEnemies(enemies);
    }
}
