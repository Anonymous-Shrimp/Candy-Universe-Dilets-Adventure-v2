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
    public float cooldown;
    float hittedTimer;
    public bool mad = false;
    public bool madAlerted = false;
    public float turnSpeed = 200f;

    private bool canDieAgain = false;
    private Animator Anim;
    public AnimationClip walkingClip;
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
        if ((Vector3.Distance(target.transform.position, transform.position) < distanceUntilNotice) && (hitted || area.inLineOfSight) || madAlerted)
        {
            AnimatorClipInfo[] a = Anim.GetCurrentAnimatorClipInfo(0);
            if (a[0].clip == walkingClip)
            {
                transform.Translate(new Vector3(0, 0, Time.deltaTime * 5));
            }
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
            cooldown = hittedCooldown;
            madAlerted = false;
            
                mad = true;
            
        }

        else
        {
            if (cooldown > 0)
            {
                AnimatorClipInfo[] a = Anim.GetCurrentAnimatorClipInfo(0);
                if (a[0].clip == walkingClip)
                {
                    transform.Translate(new Vector3(0,0,Time.deltaTime * 5));
                }
                Quaternion lookRot = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, Time.deltaTime * turnSpeed * Mathf.Abs(lookRot.y - transform.rotation.y));
                transform.rotation = new Quaternion(defaultRot.x, transform.rotation.y, defaultRot.z, transform.rotation.w);
                
                madAlerted = false;

                mad = true;

            }
            hittedTimer -= Time.deltaTime;
            mad = false;
            
        }
        Anim.SetBool("Mad", mad);
        Anim.SetFloat("Distance", Vector3.Distance(target.transform.position, transform.position));


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

        ParticleSystem e = Instantiate(explode, transform.position, transform.rotation).GetComponent<ParticleSystem>();
        e.Play();
        Destroy(e.gameObject, 5);
        Destroy(gameObject);
    }
    IEnumerator alertBiddies(AI buddy, float delayMin, float delayMax)
    {
        
        yield return new WaitForSeconds(Random.Range(delayMin, delayMax));
        buddy.madAlerted = true;


    }

}

