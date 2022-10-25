using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyHealthBarUI : MonoBehaviour {

    //private List<IEnemy> enemiesOnScreen = new List<IEnemy>();


    [SerializeField] private GameObject healthUIPrefab;


    public EnemyHeart MakeHeart(IEnemy enemy) {
        // enemiesOnScreen.Add(enemy);
        GameObject go = Instantiate(healthUIPrefab);
        go.transform.SetParent(transform, false);
        EnemyHeart heart = go.GetComponent<EnemyHeart>();
        Assert.IsNotNull(heart);

        return heart;
    }
}
