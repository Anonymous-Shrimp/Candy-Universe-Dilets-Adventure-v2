using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteCharacter : MonoBehaviour
{
    public Transform sprite;
    public float multiplier;
    public Camera cam;

    
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<miniMap>().followingCharacter)
        {
            sprite.localScale = new Vector3(cam.orthographicSize * multiplier, cam.orthographicSize * multiplier, cam.orthographicSize * multiplier);
        }
        else
        {
            sprite.localScale = new Vector3(cam.orthographicSize * multiplier / 2, cam.orthographicSize * multiplier / 2, cam.orthographicSize * multiplier / 2);
        }
        
    }
}
