using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandle : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform basePos;
    [SerializeField] Transform spaceship;
    [SerializeField] float speed;
    [SerializeField] Vector2 dir;

    private bool isMoving;
    [SerializeField] Rigidbody2D rid;
    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //rid.AddForce(new Vector2(dir.x * speed, dir.y * speed));

            if(Vector2.Distance(transform.position, target.position) < 0.01f)
            {
                GamePlayController.Instance.GameOver();
            }
        }


    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetDir()
    {
        dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
    }

    public void HandleMoving(Transform parent)
    {
        if (!isMoving)
        {
            SetDir();
            transform.SetParent(parent);
            isMoving = true;
        }
    }

    public void ResetNewPos(Transform newParent, Vector2 newPos, float radius)
    {
        transform.SetParent(newParent);
        transform.localRotation = Quaternion.Euler(0,0,0);
        transform.position = new Vector3(newPos.x, newPos.y,0);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        //transform.localPosition = new Vector2(0, radius);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Planet" && isMoving)
        {
            isMoving = false;
            PlanetController planet = collision.gameObject.GetComponent<PlanetController>();
            ContactPoint2D contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector2.up, contact.normal);
            Vector2 pos = contact.point;
            ResetNewPos(planet.GetChild(), pos, planet.GetRadius());
            SetTarget(planet.GetTarget());
            GamePlayController.Instance.UpdateCurrentTarget(collision.gameObject);
            planet.UpdateTarget(transform);
            planet.SetPlanet(GamePlayController.Instance.planetPos1);

            GamePlayController.Instance.HandlePlanet();
            GamePlayController.Instance.UpdateScore();

            RotateShip();
        }
    }

    private void RotateShip()
    {
        float z = Vector3.Angle(transform.localPosition, new Vector3(0,1,0));
        if(transform.localPosition.x > 0)
            spaceship.transform.localRotation = Quaternion.Euler(0,0, spaceship.transform.localRotation.z - z);
        else
            spaceship.transform.localRotation = Quaternion.Euler(0, 0, spaceship.transform.localRotation.z  + z);
    }
}
