using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeSystem : MonoBehaviour
{
    public AudioSource Hit;
    public AudioSource Miss;
    public int damage = 50;
    float Distance;
    public float maxDistance = 2;
    [Space]
    public Slider energyBar;
    public float multiplier = 0.3f;
    public float energyReduction = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        energyBar = GameObject.Find("EnergyBar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && energyBar.value >= energyReduction && !FindObjectOfType<PauseMenu>().isPaused)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Distance = hit.distance;
                if(Distance < maxDistance)
                {
                    hit.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
                    Hit.Play();
                }
                else
                {
                    Miss.Play();
                }
            }
            else
            {
                Miss.Play();
            }
            energyBar.value -= energyReduction;
        }
        energyBar.value += Time.deltaTime * multiplier;
    }
}
