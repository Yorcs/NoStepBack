using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour {
    protected Vector2 direction = Vector2.right;
    protected float speed = 1f;
    protected int damage = 1;
    protected int criticalChance = 0;
    protected int criticalMultiplier = 2;
    protected int penetration = 1;
    protected bool doesPoison, doesFreeze;
    protected float statusDuration = 0; //for poisoning purposes
    protected int statusDamage = 0;

    public void SetDirection(Vector2 direction) {
        this.direction = direction;
        //Todo: improve this to use actual rotation
        transform.localScale = new Vector2(direction.x * Mathf.Abs(transform.localScale.x),  transform.localScale.y);
    }
    
    public void SetCritical(int criticalChance)
    {
        this.criticalChance = criticalChance;
    }

    public void SetCriticalMultiplier(int criticalMultiplier) {
        this.criticalMultiplier = criticalMultiplier;
    }

    public void SetSpeed(float bulletSpeed) {
        this.speed = bulletSpeed;
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public void SetPenetration(int penetration) {
        this.penetration = penetration;
    }

    //status stuff
    public void SetIsPoisoned(bool doesPoison)
    {
        this.doesPoison = doesPoison;
    }

    public void SetIsFrozen(bool doesFreeze)
    {
        this.doesFreeze = doesFreeze;
    }
    public void SetStatusDuration(float statusDuration)
    {
        this.statusDuration = statusDuration;
    }

    public void SetStatusDamage(int statusDamage)
    {
        this.statusDamage = statusDamage;
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

}
