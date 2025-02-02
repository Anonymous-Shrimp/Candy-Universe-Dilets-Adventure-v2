﻿using System.Collections;
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
    public Vector2 chargedEnergyBarPos;
    public float multiplier = 0.3f;
    public float energyReduction = 0.3f;
    private float chargedEnergyReduction = 0.3f;
    private float chargedDamage;
    public GameObject largeHitbox;
    public GameObject slapParticle;
    float chargedYPos;

    // Start is called before the first frame update
    void Start()
    {
        chargedDamage = damage;
        energyBar = GameObject.Find("EnergyBar").GetComponent<Slider>();
        chargedEnergyBar = GameObject.Find("ChargedEnergyBar").GetComponent<Slider>();
        chargedYPos = chargedEnergyBar.GetComponent<RectTransform>().anchoredPosition.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        chargedEnergyBar.value = energyBar.value;
        if(Input.GetKey(FindObjectOfType<Keybind>().keys["Slap"]) && !(FindObjectOfType<PauseMenu>().isPaused || FindObjectOfType<PauseMenu>().hudMenu || FindObjectOfType<PauseMenu>().talking) && FindObjectOfType<PauseMenu>().canPause)
        {
            if (energyBar.value >= chargedEnergyReduction + energyReduction)
            {
                chargedEnergyReduction += Time.deltaTime / 1.5f;
                chargedDamage += Time.deltaTime * 2.5f;

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
        if (Input.GetKeyUp(FindObjectOfType<Keybind>().keys["Slap"]) && !(FindObjectOfType<PauseMenu>().isPaused || FindObjectOfType<PauseMenu>().hudMenu))
        {
            if (energyBar.value >= energyReduction)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Distance = hit.distance;
                    if (Distance < maxDistance)
                    {
                        if (FindObjectOfType<Ouch>().telidData.attack == "Shock Slap")
                        {
                            GameObject g = Instantiate(largeHitbox, hit.point, Quaternion.identity);
                            Destroy(g, 1);
                        }
                        hit.transform.SendMessage("ApplyDamage", Mathf.Round(chargedDamage + damage), SendMessageOptions.DontRequireReceiver);
                        ParticleSystem p = Instantiate(slapParticle, hit.transform.position, hit.transform.rotation).GetComponent<ParticleSystem>();
                        p.transform.localScale = new Vector3(chargedDamage, chargedDamage, chargedDamage);
                        p.Play();
                        Destroy(p.gameObject, 3);
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
        float xPos = energyBar.value * (Mathf.Abs(chargedEnergyBarPos.x - chargedEnergyBarPos.y)) + chargedEnergyBarPos.x;
        chargedEnergyBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos, chargedYPos);
    }
}
