using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {

    public void TakeDamage(int damage);

    public void PushBack(int damage);

    public void SetPoison(float duration, int damage);
    public void SetFreeze(float duration);

    public Vector3 GetPosition();

    public bool IsActive();

}
