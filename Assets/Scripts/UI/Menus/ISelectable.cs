using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable {
    void Setup(PlayerUIController player);

    void OnHover();
    void OnHoverLeave();
    void MoveLeft();
    void MoveRight();
    void Select();
    bool Confirm();
}
