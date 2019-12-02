using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    GameObject Sphere;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        Sphere = GameObject.Find("Sphere");
        offset = transform.position - Sphere.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Sphere.transform.position + offset;
    }
}
