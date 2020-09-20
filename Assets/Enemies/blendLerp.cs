using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blendLerp : MonoBehaviour
{
    public Material hurt;
    Material normal;
    // Start is called before the first frame update
    void Start()
    {
        normal = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
        GetComponent<Renderer>().material.Lerp(GetComponent<Renderer>().material, normal, Time.deltaTime);
    }
    public void ApplyDamage(int theDamage)
    {
        GetComponent<Renderer>().material = hurt;

    }
}
