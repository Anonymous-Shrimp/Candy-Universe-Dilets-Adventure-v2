using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanTeleport : MonoBehaviour {
    public GameObject nanTransport;
    public Ouch dilet;
    [HideInInspector]
    public bool nanGate = false;
	// Use this for initialization
	void Start () {
        nanTransport.SetActive(false);
        nanGate = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(dilet.fire && dilet.water && dilet.thunder && dilet.ice && dilet.rock)
        {
            nanGate = true;
            nanTransport.SetActive(true);
        }
	}
}
