using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookArea : MonoBehaviour
{
    public bool inLineOfSight;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dilet"))
        {
            inLineOfSight = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Dilet"))
        {
            inLineOfSight = false;
        }
    }
}
