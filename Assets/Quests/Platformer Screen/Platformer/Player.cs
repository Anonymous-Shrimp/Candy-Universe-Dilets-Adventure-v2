using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace platformQuest
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        Rigidbody2D rigid;
        public Vector2 respawnPoint;
        bool canJump = true;
        public float jumpSpeed = 3;
        public float movementSpeed = 5;
        float jumpTime;
        public float maxJumpTime;
        public GameObject checkpointParticle;
        public GameObject candyParticle;
        public GameObject endParticle;
        public Camera cam;
        float camTarget;
        float camStart;
        float sizeTarget;
        Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            canJump = true;
            camStart = cam.orthographicSize;
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            
            //cam.orthographicSize = camTarget;
            // if(cam.orthographicSize > camTarget)
            // {
            //     cam.orthographicSize -= Time.deltaTime;
            //}else if(cam.orthographicSize < camTarget)
            // {
            //     cam.orthographicSize += Time.deltaTime;
            // }
            Vector2 movement = new Vector2(0,0);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.x = 1;
            }else if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.x = -1;
            }
            else
            {
                movement.x = 0;
            }
            if (canJump)
            {
                jumpTime = maxJumpTime;
            }
            else
            {
                jumpTime -= Time.deltaTime;
            }
            transform.Translate(movement * movementSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.UpArrow) && (jumpTime > 0 || canJump)) 
            {
                rigid.AddRelativeForce(Vector2.up * jumpSpeed * 100);
                canJump = false;
            }
            anim.SetBool("Grounded", canJump);
        }
        private void OnCollisionEnter2D (Collision2D collision)
        {
            canJump = true;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Water"))
            {
                transform.position = respawnPoint;
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                
                ParticleSystem p = Instantiate(checkpointParticle, collision.transform.position, collision.transform.rotation).GetComponent<ParticleSystem>();
                p.Play();
                Destroy(p.gameObject, 3);
                respawnPoint = collision.transform.position;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Candy"))
            {
                ParticleSystem p = Instantiate(candyParticle, collision.transform.position, collision.transform.rotation).GetComponent<ParticleSystem>();
                p.Play();
                Destroy(p.gameObject, 3);
                FindObjectOfType<CandyCounter>().targetAmount += 5;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Finish"))
            {
                ParticleSystem p = Instantiate(endParticle, collision.transform.position, collision.transform.rotation).GetComponent<ParticleSystem>();
                p.Play();
                Destroy(p.gameObject, 3);
                FindObjectOfType<QuestManager>().changeProgress(13,1);
                Destroy(collision.gameObject);
            }
        }
    }
}
