using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    public Transform target;
    public int Health = 100;
    public EnemyBar Bar;
    private float barSize;
    public ParticleSystem explode;
    public Transform targetLookAt;
    private Quaternion defaultRot;
    Rigidbody rb;

    private Animator Anim;
    public float distanceUntilNotice = 30f;
    
    void Start()
    {
        Anim = GetComponent<Animator>();
        target = FindObjectOfType<Ouch>().transform;
        rb = GetComponent<Rigidbody>();
        defaultRot = transform.rotation;

    }
    void Update()
    {
        if(rb.velocity.magnitude == 0)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        Debug.DrawRay(transform.position, Vector3.down);
        if ((target.transform.position - this.transform.position).sqrMagnitude < distanceUntilNotice)
        {
            Quaternion lookRot= Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, Time.deltaTime * 100);
            Anim.SetTrigger("Attack");
        }
        else
        {
            Anim.SetTrigger("Idle");
        }
        transform.rotation = new Quaternion(defaultRot.x, transform.rotation.y, defaultRot.z, defaultRot.w);
        
        barSize = Health;
        barSize = barSize / 100;
        Bar.EnemySize(barSize);

    }
    void ApplyDamage(int TheDamage)
    {
        Health -= TheDamage;
        if (Health <= 0)
        {

            StartCoroutine(death());
            

        }

    }
    IEnumerator death()
    {
        Anim.SetTrigger("Death");
        yield return new WaitForSeconds(2);
        explode.Play();
        Destroy(explode.gameObject, 5);
        Destroy(gameObject);
    }
}

