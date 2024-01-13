using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothTime = .3f;
    Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        FollowTarget(target);
    }

    void FollowTarget(Transform targetPos)
    {
        if (targetPos == null)
        {
            Debug.LogWarning("Camera need a target");
            return;
        }

        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
        transform.position = targetPos.position + offset;
    }
}
