using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxAttack1 : MonoBehaviour {
    public GameObject hitbox;
    public Animator Anim;
    bool attacked = false;

    void Start () {
        hitbox.transform.gameObject.SetActive(false);
        Anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Kicking"))
        {
            if (!attacked)
            {
                attacked = true;
                StartCoroutine(hit());
                
            }
        }
        else
        {
            attacked = false;
        }
    
        
    }
    IEnumerator hit()
    {
        hitbox.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitbox.SetActive(false);
        yield return null;
    }
}
