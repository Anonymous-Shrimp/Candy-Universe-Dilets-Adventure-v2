using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnDistance : MonoBehaviour
{
    public Transform target;
    public float destroyDistance = 100;
    int layer;
    public float distance;
    void Start()
    {
        layer = gameObject.layer;
        if (target == null && FindObjectOfType<Ouch>() != null)
        {
            target = FindObjectOfType<Ouch>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);
        if (target != null)
        {
            if (distance < destroyDistance)
            {
                gameObject.layer = layer;
            }
            else
            {
                gameObject.layer = 11;
            }
        }
    }
}
