using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform child;
    [SerializeField] float radius;

    private bool isMoving;
    private Transform planetPos;
    [SerializeField] float speed;

    public void Update()
    {
        if(isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, planetPos.position, speed * Time.deltaTime);

            if(Vector3.Distance(transform.position, planetPos.position) < 0.01f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
                isMoving = false;
            }
        }
    }

    public void SetPlanet(Transform planet)
    {
        planetPos = planet;
        isMoving = true;
    }

    public Transform GetTarget()
    {
        return target;
    }

    public Transform GetChild()
    {
        return child;
    }

    public void HandleTarget()
    {
        target.GetComponent<TargetController>().StopMovingAndChangeParent();
    }

    public void UpdateTarget(Transform charater)
    {
        target.GetComponent<TargetController>().UpdateTarget(charater);
    }

    public float GetRadius()
    {
        return radius;
    }
}
