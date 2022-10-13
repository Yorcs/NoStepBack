using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class KnivesSubweapon : AbstractSubweapon
{
    [SerializeField] GameObject knivesPrefab;
    private float knifeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 3;
        damage = 3;
        knifeSpeed = 3f;
    }

    public override void UseSubweapon()
    {
        SpawnKnives();
    }

    private Knives SpawnKnives()
    {
        GameObject GO = Instantiate(knivesPrefab);
        GO.transform.position = transform.position;

        Knives newKnives = GO.GetComponent<Knives>();
        Assert.IsNotNull(newKnives);

        newKnives.SetDirection(Vector2.right);
        newKnives.SetSpeed(knifeSpeed);
        newKnives.SetDamage(damage);

        return newKnives;
    }
}
