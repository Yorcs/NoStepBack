using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable {
    void Setup(PlayerUIController player);
    void MoveLeft();
    void MoveRight();
    void Select();
}
