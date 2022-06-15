using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawObject : MonoBehaviour
{
    [SerializeField] GameObject[] planet;

    public GameObject Spaw(Transform parent)
    {
        GameObject obj = Instantiate(planet[Random.Range(0, planet.Length)], Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(parent);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;

        return obj;
    }
}
