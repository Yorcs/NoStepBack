using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;

    private List<IController> playersNumber = new List<IController>();

    //Todo: Check how many players are connected
    //todo: use state to change inbetween players number so it doesn't crash. or use loop?
    //todo: use math.lerp to calculate the distance between the players so that the camera always follow the group

    private void Start()
    {
        playersNumber.AddRange(GetComponentsInChildren<IController>());
    }

    private void Update()
    {
        Vector3 newPosition = Target.position;
        newPosition.z = -10;
        switch()
        {
            case playersNumber < 2:
                transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
                break;
            case playersNumber > 2:


        }
    }
}
