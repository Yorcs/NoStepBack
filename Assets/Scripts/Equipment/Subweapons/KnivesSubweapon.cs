using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class KnivesSubweapon : AbstractSubweapon
{
    [SerializeField] GameObject knivesPrefab;
    [SerializeField] private float knifeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        subweaponRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(subweaponRenderer);
        damage = 1000;
        knifeSpeed = 20f;
    }

    public override void UseSubweapon()
    {
        SpawnKnives();
        Destroy(gameObject);
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
