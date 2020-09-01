﻿using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using System.Linq;

public class Ouch : MonoBehaviour
{
    
    public float health = 100;
    int maxHealth = 100;
    public GameObject dilet;
    public DiletBar Bar;
    public bool introduction;
    private float barSize;
    [Space]
    public CameraShake CameraShake;
    public PostProcessVolume Screen;
    [Space]
    public float duration = 0.2f;
    public float magnitude = 0.1f;
    [Space]
    public int GoblinDamage;
    [Space]
    public bool water = false;
    public bool thunder = false;
    public bool fire = false;
    public bool ice = false;
    public bool rock = false;
    public bool nan = false;
    [Space]
    public bool start = false;
    public bool inDungeon = false;
    [Space]
    public GameObject fireGate;
    public GameObject waterGate;
    public GameObject thunderGate;
    public GameObject iceGate;
    public GameObject rockGate;

    [Space]
    public GameObject candyParticle;
    public timePlace timeInPlace;
    public List<QuestData> questData = new List<QuestData>();
    public ResearchData telidData;
    public Animator animationArea;
    public CandyCounter candy;
    public QuestManager questManager;
    public TelidResearch telid;
    Rigidbody Rigid;
    [Space]
    public bool cold = false;
    float coldTimer;
    float regenTimer;
    float jumpPower;
    bool refreshResearchData = false;
    float jumpEnergyReduction;
    float runSpeed;
    public Animator jumpBarAnim;

    void Awake()
    {
        runSpeed = GetComponent<FirstPersonController>().m_RunSpeed;
        jumpPower = GetComponent<FirstPersonController>().jumpAdd;
        candy = FindObjectOfType<CandyCounter>();
        Rigid = GetComponent<Rigidbody>();
        questManager = GetComponent<QuestManager>();
        telid = FindObjectOfType<TelidResearch>();
        jumpBarAnim = GameObject.Find("JumpBar").GetComponent<Animator>();

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Screen.weight = 0;

        //load
        
            questData.Clear();
            foreach (Quest q in questManager.quests)
            {
                q.active = false;
                q.completed = false;
                q.completed = false;
                q.progress = 0;
                QuestData qd = new QuestData(q.active, q.started, q.completed, q.progress);

                questData.Add(qd);
            }

        
        if (introduction)
        {
            water = false;
            thunder = false;
            fire = false;
            ice = false;
            rock = false;
            nan = false;
            health = 100;
            candy.targetAmount = 0;
            candy.candyAmount = 0;
            start = true;
            
            SavePlayer();
        }
        else
        {
            LoadPlayer();
            PlayerData data = SaveSystem.LoadPlayer();

            Vector3 position;
            if (start)
            {
                water = false;
                thunder = false;
                fire = false;
                ice = false;
                rock = false;
                nan = false;
                health = 100;
                position.x = -365.293f;
                position.y = 479.477f;
                position.z = 828.8861f;
                if (!questManager.quests[0].completed && !introduction)
                {
                    questManager.StartQuest(0);
                }
                if (FindObjectOfType<TimeCycle>() != null)
                {
                    FindObjectOfType<TimeCycle>().dayNum = 1;
                    FindObjectOfType<TimeCycle>().currentTimeOfDay = 0.3f;
                }
                if (telid != null)
                {
                    foreach(TelidResearchItem t in telid.items)
                    {
                        t.active = false;
                        t.questActive = false;
                        t.completed = false;
                    }
                }


                candy.targetAmount = 0;
                candy.candyAmount = 0;
                transform.position = position;
                start = false;
                SavePlayer();
            }
            else
            {
                if (sceneName == "Fire" || sceneName == "Water" || sceneName == "Thunder" || sceneName == "Rock" || sceneName == "Ice" || sceneName == "Nan")
                {
                    Debug.Log("Loading Dungeon");
                    water = data.water;
                    thunder = data.thunder;
                    fire = data.fire;
                    ice = data.ice;
                    rock = data.rock;
                    nan = data.nan;
                    health = data.health;
                    start = data.start;
                    inDungeon = data.inDungeon;
                    Vector3 loc;
                    loc.x = 0f;
                    loc.y = 0f;
                    loc.z = 0f;
                    transform.position = loc;
                    
                    
                }
                else if(sceneName == "Overworld 3" || currentScene.buildIndex == 3)
                {
                    Debug.Log("Loading Overworld");
                    LoadPlayer();
                    if (fire)
                    {
                        Destroy(fireGate);
                    }
                    if (water)
                    {
                        Destroy(waterGate);
                    }
                    if (thunder)
                    {
                        Destroy(thunderGate);
                    }
                    if (ice)
                    {
                        Destroy(iceGate);
                    }
                    if (rock)
                    {
                        Destroy(rockGate);
                    }
                    
                }
                
            }
        }
        

    }
    private void Start()
    {
        
    }
    void Update()
    {
        if(FindObjectOfType<TelidResearch>() != null)
        {
            telid = FindObjectOfType<TelidResearch>();
        }
        if (telid != null && !refreshResearchData)
        {
            bool attack = false;
            bool movement = false;
            bool defence = false;
            foreach (TelidResearchItem t in telid.items)
            {
                
                if (t.researchType == TelidResearchItem.ResearchType.Attack && t.active)
                {
                    telidData.attack = t.name;
                    attack = true;

                }
                if (t.researchType == TelidResearchItem.ResearchType.Movement && t.active)
                {
                    telidData.movement = t.name;
                    movement = true;

                }
                if (t.researchType == TelidResearchItem.ResearchType.Defense && t.active)
                {
                    telidData.defence = t.name;
                    defence = true;

                }

            }
            if (!attack) { telidData.attack = ""; }
            if (!movement) { telidData.movement = ""; }
            if (!defence) { telidData.defence = ""; }

        }
        else if(telid != null)
        {
            foreach (TelidResearchItem t in telid.items)
            {
                t.active = false;
                if (t.name == telidData.attack || t.name == telidData.movement || t.name == telidData.defence)
                {
                    t.active = true;
                }
            }
            refreshResearchData = false;
        }
        if (Input.GetKeyDown(KeyCode.F) && candy.targetAmount >= 10)
        {
            health += 10;
            candy.targetAmount -= 10;
            if (FindObjectOfType<QuestManager>().quests[5].active)
            {
                FindObjectOfType<QuestManager>().changeProgress(5, 0);
            }
        }
        if (health <= 0)
        {
            LoadPlayer();
            health = 100;
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Death");
        }
        if(telidData.movement == "Super Run")
        {
            if (Input.GetKey(KeyCode.LeftShift) && FindObjectOfType<MeleeSystem>().energyBar.value >= 0f)
            {
                FindObjectOfType<MeleeSystem>().energyBar.value -= Time.deltaTime * 1.5f;
                GetComponent<FirstPersonController>().m_RunSpeed = GetComponent<FirstPersonController>().m_RunSpeed;
                GetComponent<FirstPersonController>().m_IsWalking = false;
                GetComponent<FirstPersonController>().m_RunSpeed = 30;
            }
            else
            {
                GetComponent<FirstPersonController>().m_RunSpeed = GetComponent<FirstPersonController>().m_WalkSpeed;
                GetComponent<FirstPersonController>().m_IsWalking = true;
                GetComponent<FirstPersonController>().m_RunSpeed = 10;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && FindObjectOfType<MeleeSystem>().energyBar.value >= 0.2f)
            {
                FindObjectOfType<MeleeSystem>().energyBar.value -= 0.2f;
            }

            
        }
        else
        {
            
        }
        if(telidData.movement == "Super Jump")
        {
            GetComponent<FirstPersonController>().jumpInput = false;
        }
        else
        {
            GetComponent<FirstPersonController>().jumpInput = true;
        }
        if(telidData.movement == "Super Jump" && Input.GetKey(KeyCode.Space))
        {
            GetComponent<FirstPersonController>().jumpAdd += Time.deltaTime * 10;
            if(GetComponent<FirstPersonController>().jumpAdd > jumpBarAnim.gameObject.GetComponent<Slider>().maxValue)
            {
                GetComponent<FirstPersonController>().jumpAdd = jumpBarAnim.gameObject.GetComponent<Slider>().maxValue;
            }
            jumpBarAnim.gameObject.GetComponent<Slider>().value = GetComponent<FirstPersonController>().jumpAdd;
        }
        else if(telidData.movement == "Super Jump" && !Input.GetKey(KeyCode.Space) && GetComponent<FirstPersonController>().m_Jumping)
        {

            jumpBarAnim.gameObject.GetComponent<Slider>().value = GetComponent<FirstPersonController>().jumpAdd;
        }
        else 
        {

        }
        jumpBarAnim.SetBool("Entered", telidData.movement == "Super Jump" && Input.GetKey(KeyCode.Space));
        if (telidData.defence == "Regeneration")
        {
            
            
            regenTimer += Time.deltaTime;
            if(regenTimer > 4 && health < 100)
            {
                health += 15 * Time.deltaTime;
            }
        }
        if (telidData.defence == "Max Health+")
        {
            maxHealth = 300;
        }
        else
        {
            maxHealth = 100;
        }
        barSize = health;
        barSize = barSize / maxHealth;
        Bar.SetSize(barSize);
        
        Scene currentScene = SceneManager.GetActiveScene();
        
        string sceneName = currentScene.name;

        if( FindObjectOfType<TimeCycle>() != null)
        {
            if (FindObjectOfType<TimeCycle>().dayNum >= 10)
            {
                start = true;
                SavePlayer();
                FindObjectOfType<loading>().LoadLevelString("Death");
            }
        }
        if (FindObjectOfType<TimeCycle>() != null)
        {
            timeInPlace.dayNum = FindObjectOfType<TimeCycle>().dayNum;
            timeInPlace.TimeOfDay = FindObjectOfType<TimeCycle>().currentTimeOfDay;
        }
        
        if (cold)
        {
            coldTimer -= Time.deltaTime;
            if(coldTimer <= 0)
            {
                coldTimer = 7f;
                health -= 5;
                StartCoroutine(Screenoof(4f));
            }
        }


        if (questManager != null)
        {
            questData.Clear();
            foreach (Quest q in questManager.quests)
            {
                QuestData qd = new QuestData(q.active, q.started, q.completed, q.progress);
                questData.Add(qd);
            }
            

        }
        int gatesCompleted = 0;
        if (water) { gatesCompleted++; }
        if (fire) { gatesCompleted++; }
        if (ice) { gatesCompleted++; }
        if (thunder) { gatesCompleted++; }
        if (rock) { gatesCompleted++; }
        if(gatesCompleted != questManager.getProgress(1))
        {
            questManager.changeProgress(1, gatesCompleted);
        }
    }
    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("WaterDeath"))
        {
            FindObjectOfType<loading>().LoadLevelString("Death");
        }
        if (collision.gameObject.CompareTag("GoblinBox"))
        {
            
                health -= collision.gameObject.GetComponentInParent<AI>().damage;
            
            StartCoroutine(CameraShake.Shake(duration, magnitude));
            StartCoroutine(Screenoof(2));
        }
        if (collision.gameObject.CompareTag("Fire"))
        {

            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Fire");
        }
        if (collision.gameObject.CompareTag("Ice"))
        {
            
            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Ice");
        }
        if (collision.gameObject.CompareTag("Thunder"))
        {
            
            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Thunder");
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Water");
        }
        if (collision.gameObject.CompareTag("Rock"))
        {
            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Rock");
        }
        if (collision.gameObject.CompareTag("Nan"))
        {
            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Nan");
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            inDungeon = false;
            FindObjectOfType<loading>().LoadLevelString("Overworld 3");
        }
        if (collision.gameObject.CompareTag("FireBoss"))
        {
            LoadPlayer();
            fire = true;
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Overworld 3");
        }
        if (collision.gameObject.CompareTag("WaterBoss"))
        {
            LoadPlayer();
            water = true;
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Overworld 3");
        }
        if (collision.gameObject.CompareTag("ThunderBoss"))
        {
            LoadPlayer();
            thunder = true;
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Overworld 3");
        }
        if (collision.gameObject.CompareTag("IceBoss"))
        {
            LoadPlayer();
            ice = true;
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Overworld 3");
        }
        if (collision.gameObject.CompareTag("RockBoss"))
        {
            LoadPlayer();
            rock = true;
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Overworld 3");
        }
        if (collision.gameObject.CompareTag("NanBoss"))
        {
            FindObjectOfType<loading>().LoadLevelString("ToBeContinued");
        }
        if (collision.gameObject.CompareTag("Electricity"))
        {
            
            Rigid.AddRelativeForce(Vector3.back * 100);
            StartCoroutine(Screenoof(2));
            health -= 10;
        }
        if (collision.gameObject.CompareTag("Freeze"))
        {
            cold = true;
        }
        if (collision.gameObject.CompareTag("Boulder"))
        {
            health -= 20;
            StartCoroutine(Screenoof(2));
        }
        if (collision.gameObject.CompareTag("Candy"))
        {
            candy.targetAmount += 20;
            GameObject i = Instantiate(candyParticle, collision.transform.position, collision.transform.rotation);
            i.GetComponent<ParticleSystem>().Play();
            Destroy(i, 5f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("FireArea"))
        {
            if(animationArea != null)
            {
                animationArea.SetTrigger("Fire");
            }
        }
        if (collision.gameObject.CompareTag("IceArea"))
        {
            if (animationArea != null)
            {
                animationArea.SetTrigger("Ice");
            }
        }
        if (collision.gameObject.CompareTag("RockArea"))
        {
            if (animationArea != null)
            {
                animationArea.SetTrigger("Rock");
            }
        }
        if (collision.gameObject.CompareTag("NanArea"))
        {
            if (animationArea != null)
            {
                animationArea.SetTrigger("Nan");
            }
        }
        if (collision.gameObject.CompareTag("PoyoArea"))
        {
            if (animationArea != null)
            {
                animationArea.SetTrigger("Poyo");
            }
        }
        if (collision.gameObject.CompareTag("WaterArea"))
        {
            if (animationArea != null)
            {
                animationArea.SetTrigger("Water");
            }
        }
        if (collision.gameObject.CompareTag("ThunderArea"))
        {
            if (animationArea != null)
            {
                animationArea.SetTrigger("Thunder");
            }
        }
        if (collision.gameObject.CompareTag("Poyo"))
        {
            StartCoroutine(Poyo());
        }
        if (collision.gameObject.CompareTag("TalkArea"))
        {
            collision.gameObject.GetComponent<TalkArea>().talkArea = true;
        }


    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("TalkArea"))
        {
            collision.gameObject.GetComponent<TalkArea>().talkArea = false;
        }
        if (collision.gameObject.CompareTag("Freeze"))
        {
            cold = false;
        }
    }
    IEnumerator Screenoof(float Sec)
    {
        if (telidData.defence == "Regeneration")
        {
            regenTimer = 0;
        }
        for (float i = 1; i >= 0; i -= Time.deltaTime / Sec)
        {
            Screen.weight = i;
            yield return null;
        }
    }
    IEnumerator Poyo()
    {
        GameObject.Find("PoyoImage").GetComponent<Animator>().SetTrigger("Poyo");
        yield return new WaitForSeconds(1.1f);
        transform.position = new Vector3(-365.293f, 479.477f, 828.8861f);
        
        yield return null;
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this, timeInPlace, candy.targetAmount, questData.ToArray(), telidData);

    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        water = data.water;
        thunder = data.thunder;
        fire = data.fire;
        ice = data.ice;
        rock = data.rock;
        nan = data.nan;
        health = data.health;
        start = data.start;
        inDungeon = data.inDungeon;
        candy.targetAmount = data.candyAmount;
        candy.candyAmount = data.candyAmount;
        for (int i = 0; i < data.questData.Length; i++)
        {
            questManager.quests[i].active = data.questData[i].active;
            questManager.quests[i].started = data.questData[i].started;
            questManager.quests[i].completed = data.questData[i].completed;
            questManager.quests[i].progress = data.questData[i].progress;
        }
        
        if (FindObjectOfType<TimeCycle>() != null)
        {
            FindObjectOfType<TimeCycle>().dayNum = data.dayNum;
            FindObjectOfType<TimeCycle>().currentTimeOfDay = data.timeOfDay;
        }
        
        else
        {
            timeInPlace.dayNum = data.dayNum;
            timeInPlace.TimeOfDay = data.timeOfDay;
        }
        if(telid != null)
        {
            foreach(TelidResearchItem t in telid.items)
            {
                t.active = false;
                if(t.name == data.telidData.attack || t.name == data.telidData.movement || t.name == data.telidData.defence)
                {
                    t.active = true;
                }
            }
            refreshResearchData = false;
        }
        else
        {
            telidData = data.telidData;
            refreshResearchData = true;
        }
        if(questManager != null)
        {
            int index = 0;
            foreach(QuestData q in data.questData)
            {
                questManager.quests[index].active = q.active;
                questManager.quests[index].started = q.started;
                questManager.quests[index].completed = q.completed;
                questManager.quests[index].progress = q.progress;
                index++;
            }
        }
        if (nan)
        {
            Vector3 position;
            Debug.Log("Game Already Completed");
            position.x = 153.72f;
            position.y = 678.79f;
            position.z = 891.52f;
            transform.position = position;
            nan = false;
            health = 100;
        }else
        {
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;
        }
    }
    public void LoadPlayerAndReload()
    {
        FindObjectOfType<loading>().LoadSameLevel();
    }
}