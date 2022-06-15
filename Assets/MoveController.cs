using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] GameObject eff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Planet")
        {
            StartCoroutine(HandleCollision(collision.transform));
        }
    }

    IEnumerator HandleCollision(Transform tran)
    {
        //Game over
        GameObject effect = Instantiate(eff, Vector2.zero, Quaternion.identity, tran.transform);
        effect.transform.localPosition = Vector2.zero;

        yield return new WaitForSeconds(0.5f);
        GamePlayController.Instance.GameOver();
    }
}
