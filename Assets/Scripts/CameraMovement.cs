using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public List<PlayerStatus> targets;

    [SerializeField] Vector3 offset;
    private Vector3 velocity;
    [SerializeField] float smoothTime = .5f;
    [SerializeField] Vector3 minValues, maxValue;

    private bool Locked { get; set; }

    private void Start() {
        Locked = true;
        //this shouldn't be magic numbers later
        //offset = new Vector3(20, 0, -10);
    }


    void LateUpdate()
    {
        if (targets.Count == 0 || Locked)
        {
            return;
        }

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(newPosition.x, minValues.x, maxValue.x),
            Mathf.Clamp(newPosition.y, minValues.y, maxValue.y),
            Mathf.Clamp(newPosition.z, minValues.z, maxValue.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothTime * Time.fixedDeltaTime);

        minValues.x = smoothPosition.x;

        transform.position = smoothPosition;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].gameObject.transform.position;
        }

        var bounds = new Bounds(targets[0].gameObject.transform.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if(!targets[i].IsDead()) {
                bounds.Encapsulate(targets[i].gameObject.transform.position);
            }
        }

        return bounds.center;
    }

    public void OnPlayerJoined(PlayerInput input) {
        targets.Add(input.gameObject.GetComponent<PlayerStatus>());
    }
}

