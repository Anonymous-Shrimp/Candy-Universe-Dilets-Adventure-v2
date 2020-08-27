using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteCharacter : MonoBehaviour
{
    public Transform[] sprite;
    public GameObject[] disappearOnFull;
    public float multiplier;
    public Camera cam;

    private void Start()
    {
        sprite[1] = GameObject.Find("SpriteCharacter").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<miniMap>().followingCharacter)
        {
            foreach (Transform s in sprite)
            {
                s.localScale = new Vector3(cam.orthographicSize * multiplier, cam.orthographicSize * multiplier, cam.orthographicSize * multiplier);
            }
            
        }
        else
        {
            foreach (Transform s in sprite)
            {
                s.localScale = new Vector3(cam.orthographicSize * multiplier / 2, cam.orthographicSize * multiplier / 2, cam.orthographicSize * multiplier / 2);
            }
        }
        if (disappearOnFull.Length > 0)
        {
            foreach (GameObject s in disappearOnFull)
            {
                s.SetActive(!FindObjectOfType<miniMap>().followingCharacter);
            }
        }
    }
}
