using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class KnivesSubweapon : AbstractSubweapon
{
    [SerializeField] GameObject knivesPrefab;
    [SerializeField] private float knifeSpeed;
    private int uses = 1;

    // Start is called before the first frame update
    void Start()
    {
        subweaponRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(subweaponRenderer);
        damage = 1000;
        knifeSpeed = 20f;
    }

    public override void Upgrade(int ranks) {}

    //todo: Destroying this object causes problems with the player and seems to mess with subweapon pickups after that point
    public override void UseSubweapon(Vector2 direction, int layer)
    {
        SpawnKnives(direction, layer);
        uses --;
        if(uses == 0) {
            Destroy(gameObject);
        }
    }

    private Knives SpawnKnives(Vector2 direction, int layer)
    {
        GameObject GO = Instantiate(knivesPrefab);
        GO.transform.position = transform.position;
        GO.layer = layer;

        Knives newKnives = GO.GetComponent<Knives>();
        Assert.IsNotNull(newKnives);

        newKnives.SetDirection(direction);
        newKnives.SetSpeed(knifeSpeed);
        newKnives.SetDamage(damage);

        return newKnives;
    }
}
