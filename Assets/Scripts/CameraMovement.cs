using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public List<Transform> targets;

    [SerializeField] Vector3 offset;
    private Vector3 velocity;
    [SerializeField] float smoothTime = .5f;
    [SerializeField] Vector3 minValues, maxValue;

    void LateUpdate()
    {
        if (targets.Count == 0)
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

        transform.position = smoothPosition;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}

