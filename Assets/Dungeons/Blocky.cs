using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocky : MonoBehaviour {
    Rigidbody Rigid;
    public int time = 4;
    public int power = 20;
    bool perished;

    private void Awake()
    {
        
    }
    void Start()
    {
        Rigid = GetComponent<Rigidbody>();
        perished = false;
    }
    void ApplyDamage(int theDamage)
    {
        transform.rotation = FindObjectOfType<Ouch>().gameObject.GetComponentInChildren<Camera>().transform.rotation;
        Rigid.AddForce(Vector3.forward * power * theDamage);
        StartCoroutine(perish(time));
    }
    IEnumerator perish(int destroy)
    {
        if (!perished)
        {
            perished = true;
            yield return new WaitForSeconds(destroy);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dilet"))
        {
            StartCoroutine(perish(Mathf.RoundToInt(time * 0.75f)));
        }
    }
}
