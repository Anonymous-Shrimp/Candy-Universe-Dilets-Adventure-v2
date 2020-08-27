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
    public Slider chargedEnergyBar;
    public float multiplier = 0.3f;
    public float energyReduction = 0.3f;
    private float chargedEnergyReduction = 0.3f;
    private float chargedDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        chargedDamage = damage;
        energyBar = GameObject.Find("EnergyBar").GetComponent<Slider>();
        chargedEnergyBar = GameObject.Find("ChargedEnergyBar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        chargedEnergyBar.value = energyBar.value;
        if(Input.GetMouseButton(0) && !FindObjectOfType<PauseMenu>().isPaused)
        {
            if (energyBar.value >= chargedEnergyReduction + energyReduction)
            {
                chargedEnergyReduction += Time.deltaTime / 1.5f;
                chargedDamage += Time.deltaTime * 2.5f;
                print(chargedEnergyReduction + energyReduction);
            }
            chargedEnergyBar.value = chargedEnergyReduction + energyReduction;
            chargedEnergyBar.GetComponentInChildren<Image>().color = new Color(1, 1, 1, (chargedEnergyReduction + 0.5f) / 3);
            energyBar.value += Time.deltaTime * multiplier / 3;
        }
        else
        {
            energyBar.value += Time.deltaTime * multiplier;
            chargedEnergyBar.value = 0;
            chargedEnergyBar.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
        }
        if(chargedEnergyBar.value > energyBar.value)
        {
            chargedEnergyBar.value = energyBar.value;
        }
        if (Input.GetMouseButtonUp(0) && !FindObjectOfType<PauseMenu>().isPaused)
        {
            if (energyBar.value >= energyReduction)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Distance = hit.distance;
                    if (Distance < maxDistance)
                    {
                        hit.transform.SendMessage("ApplyDamage", Mathf.Round(chargedDamage + damage), SendMessageOptions.DontRequireReceiver);
                        print(chargedDamage + damage);
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
                energyBar.value -= chargedEnergyReduction + energyReduction;
            }
            
            chargedDamage = 0;
            chargedEnergyReduction = 0;
        }
       
    }
}
