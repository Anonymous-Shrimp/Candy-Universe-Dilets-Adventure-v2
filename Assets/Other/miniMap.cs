using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMap : MonoBehaviour
{
    GameObject Dilet;
    public Vector3 offset;
    public bool followingCharacter = true;
    public Vector3 zoomOutPos;
    public float camSizeNormal;
    public float camSizeZoom;
    public Camera cam;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Dilet = FindObjectOfType<Ouch>().gameObject;
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            followingCharacter = !followingCharacter;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetBool("big", !anim.GetBool("big"));
        }
        if (followingCharacter)
        {
            transform.position = Dilet.transform.position;
            cam.orthographicSize = camSizeNormal;
        }
        else
        {
            transform.position = zoomOutPos;
            cam.orthographicSize = camSizeZoom;
        }
       
        
        
        transform.eulerAngles = new Vector3(0, Dilet.transform.eulerAngles.y, 0);
    }
}
