using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] bool isMoving;
    [SerializeField] Transform lookAt;
    [SerializeField] Transform parent;
    [SerializeField] Transform basePos;
    public void SetLookAt(Transform target)
    {
        lookAt = target;
    }

    public void UpdateTarget(Transform character)
    {
        transform.position = character.position;
        transform.localPosition = character.localPosition;
        transform.localRotation = character.localRotation;

        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y) * 10;
    }

    public void StopMovingAndChangeParent()
    {
        isMoving = false;
        transform.SetParent(parent);
    }
}
