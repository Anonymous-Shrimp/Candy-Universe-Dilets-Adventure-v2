using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : MonoBehaviour
{
    public AudioSource Hit;
    public AudioSource Miss;
    public int damage = 50;
    float Distance;
    public float maxDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Distance = hit.distance;
                if(Distance < maxDistance)
                {
                    hit.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
                    Hit.Play();
                }
                else
                {
                    Miss.Play();
                }
            }
            else
            {
                Miss.Play();
            }
        }
    }
}
