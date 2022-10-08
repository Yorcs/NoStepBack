using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReceiver : MonoBehaviour, IController
{

    public bool isUpButton()
    {
        if (.GetAxis("Vertical"))
        {
            return true;
        }
        return false;
    }

    public bool isDownButton()
    {
        if (PlayerInputReceiver.GetAxis("Vertical"))
        {
            isDownButton = true;
        }
        else
        {
            isDownButton = false;
        }
    }

    public bool isForwardButton()
    {
        if (PlayerInputReceiver.GetAxis("Horizontal"))
        {
            isForwardButton = true;
        }
        else
        {
            isForwardButton = false;
        }
    }

    //needs to determine which button.
    public bool isSpecialButton()
    {
        if (PlayerInputReceiver.GetAxis("Vertical"))
        {
            isSpecialButton = true;
        }
        else
        {
            isSpecialButton = false;
        }
    }

    void OnKeyDown()
    {
        Vector2 position = transform.position;
        if(isUpButton)
        {

        }
    }
}
