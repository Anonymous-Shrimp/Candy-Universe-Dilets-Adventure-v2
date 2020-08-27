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
    LookArea area;
    bool hitted = false;
    public float hittedCooldown = 3;
    float hittedTimer;
    public bool mad = false;
    public bool madAlerted = false;
    public float turnSpeed = 200f;

    private bool canDieAgain = false;
    private Animator Anim;
    public float distanceUntilNotice = 30f;
    
    void Start()
    {
        Anim = GetComponent<Animator>();
        target = FindObjectOfType<Ouch>().transform;
        rb = GetComponent<Rigidbody>();
        defaultRot = transform.rotation;
        area = GetComponentInChildren<LookArea>();
    }
    void FixedUpdate()
    {
        if(rb.velocity.magnitude == 0)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        Debug.DrawRay(transform.position, Vector3.down);
        if (((target.transform.position - transform.position).sqrMagnitude < distanceUntilNotice) && (hitted || area.inLineOfSight) || madAlerted)
        {
            Quaternion lookRot= Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, Time.deltaTime * turnSpeed * Mathf.Abs(lookRot.y - transform.rotation.y));
            transform.rotation = new Quaternion(defaultRot.x, transform.rotation.y, defaultRot.z, transform.rotation.w);
            foreach(AI i in FindObjectsOfType<AI>())
            {
                
                if(Vector3.Distance(i.transform.position, transform.position) < 40 && i != this && !madAlerted)
                {
                    StartCoroutine(alertBiddies(i, 0.2f, 1f));
                    
                }
            }
            madAlerted = false;
            
                mad = true;
            
        }

        else
        {
            
                mad = false;
            
        }
        Anim.SetBool("Mad", mad);
        
        
        barSize = Health;
        barSize = barSize / 100;
        Bar.EnemySize(barSize);
        if(hittedTimer <= 0)
        {
            hitted = false;
        }
        else
        {
            hittedTimer -= Time.deltaTime;
        }
    }
    void ApplyDamage(int TheDamage)
    {
        Health -= TheDamage;
        hitted = true;
        hittedTimer = hittedCooldown;
        if (Health <= 0 && !canDieAgain)
        {
            canDieAgain = true;
            FindObjectOfType<CandyCounter>().targetAmount += Random.Range(3, 13);
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
    IEnumerator alertBiddies(AI buddy, float delayMin, float delayMax)
    {
        
        yield return new WaitForSeconds(Random.Range(delayMin, delayMax));
        buddy.madAlerted = true;


    }

}

