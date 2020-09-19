using System.Collections;
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
    public Animator deadAnim;
    Rigidbody Rigid;
    [Space]
    public bool cold = false;
    float coldTimer;
    public bool harshEnvironment;
    float harshEnvironmentTimer;
    float regenTimer;
    float jumpPower;
    bool refreshResearchData = false;
    float jumpEnergyReduction;
    float runSpeed;
    public Animator jumpBarAnim;
    [Space]
    private Vector2 movement;
    private Vector2 movementTarget;
    public float stopSpeed = 2
        ;

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
        if (FindObjectOfType<TelidResearch>() != null)
        {
            telid = FindObjectOfType<TelidResearch>();
            if (FindObjectOfType<TimeCycle>() != null)
            {
                FindObjectOfType<TimeCycle>().dayNum = 1;
                FindObjectOfType<TimeCycle>().currentTimeOfDay = 0.3f;
            }
        }
        int gatesCompleted = 0;
        if (water) { gatesCompleted++; }
        if (fire) { gatesCompleted++; }
        if (ice) { gatesCompleted++; }
        if (thunder) { gatesCompleted++; }
        if (rock) { gatesCompleted++; }
        if (gatesCompleted != questManager.getProgress(1))
        {
            questManager.changeProgress(1, gatesCompleted);
        }

    }
    void Update()
    {
        Keybind keybind = FindObjectOfType<Keybind>();
        FindObjectOfType<FirstPersonController>().jump = keybind.keys["Jump"];
        FindObjectOfType<FirstPersonController>().run = keybind.keys["Run"];
        if (Input.GetKey(keybind.keys["Up"]) && !Input.GetKey(keybind.keys["Down"]))
        {
            movementTarget.y = 1;
        }else if (!Input.GetKey(keybind.keys["Up"]) && Input.GetKey(keybind.keys["Down"]))
        {
            movementTarget.y = -1;
        }
        else if((Input.GetKey(keybind.keys["Up"]) && Input.GetKey(keybind.keys["Down"])))
        {
            movementTarget.y = 0;
        }
        else if ((!Input.GetKey(keybind.keys["Up"]) && !Input.GetKey(keybind.keys["Down"])))
        {
            movementTarget.y = 0;
        }
        if (Input.GetKey(keybind.keys["Right"]) && !Input.GetKey(keybind.keys["Left"]))
        {
            movementTarget.x = 1;
        }
        else if (!Input.GetKey(keybind.keys["Right"]) && Input.GetKey(keybind.keys["Left"]))
        {
            movementTarget.x = -1;
        }
        else if ((Input.GetKey(keybind.keys["Right"]) && Input.GetKey(keybind.keys["Left"])))
        {
            movementTarget.x = 0;
        }
        else if ((!Input.GetKey(keybind.keys["Right"]) && !Input.GetKey(keybind.keys["Left"])))
        {
            movementTarget.x = 0;
        }
        if (Mathf.Abs(movement.y - movementTarget.y) < 0.1)
        {
            movement.y = movementTarget.y;
        }
        if (Mathf.Abs(movement.x - movementTarget.x) < 0.1)
        {
            movement.x = movementTarget.x;
        }
        if (movement.x > movementTarget.x)
        {
            movement.x -= Time.deltaTime * stopSpeed;
        }else if (movement.x < movementTarget.x)
        {
            movement.x += Time.deltaTime * stopSpeed;
        }
        if (movement.y > movementTarget.y)
        {
            movement.y -= Time.deltaTime * stopSpeed;
        }
        else if (movement.y < movementTarget.y)
        {
            movement.y += Time.deltaTime * stopSpeed;
        }
        
        FindObjectOfType<FirstPersonController>().keybindInput = movement;
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
        if (!questManager.quests[0].completed && !introduction && !questManager.quests[0].active)
        {
            questManager.StartQuest(0);
        }
        if (Input.GetKeyDown(FindObjectOfType<Keybind>().keys["Eat Candy"]) && candy.targetAmount >= 10 && maxHealth - health > 0) 
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
            
            deadAnim.SetTrigger("Dead");
            FindObjectOfType<PauseMenu>().canPause = false;
        }
        if(telidData.movement == "Super Run")
        {
            if (Input.GetKey(FindObjectOfType<Keybind>().keys["Run"]) && FindObjectOfType<MeleeSystem>().energyBar.value >= 0f)
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
            if (Input.GetKeyDown(FindObjectOfType<Keybind>().keys["Run"]) && FindObjectOfType<MeleeSystem>().energyBar.value >= 0.2f)
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
        if(telidData.movement == "Super Jump" && Input.GetKey(FindObjectOfType<Keybind>().keys["Jump"]))
        {
            GetComponent<FirstPersonController>().jumpAdd += Time.deltaTime * 10;
            if(GetComponent<FirstPersonController>().jumpAdd > jumpBarAnim.gameObject.GetComponent<Slider>().maxValue)
            {
                GetComponent<FirstPersonController>().jumpAdd = jumpBarAnim.gameObject.GetComponent<Slider>().maxValue;
            }
            jumpBarAnim.gameObject.GetComponent<Slider>().value = GetComponent<FirstPersonController>().jumpAdd;
        }
        else if(telidData.movement == "Super Jump" && !Input.GetKey(FindObjectOfType<Keybind>().keys["Jump"]) && GetComponent<FirstPersonController>().m_Jumping)
        {

            jumpBarAnim.gameObject.GetComponent<Slider>().value = GetComponent<FirstPersonController>().jumpAdd;
        }
        else 
        {

        }
        jumpBarAnim.SetBool("Entered", telidData.movement == "Super Jump" && Input.GetKey(FindObjectOfType<Keybind>().keys["Jump"]));
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
                deadAnim.SetTrigger("Dead");
                FindObjectOfType<PauseMenu>().canPause = false;
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
        if (harshEnvironment)
        {
            harshEnvironmentTimer -= Time.deltaTime;
            if (harshEnvironmentTimer <= 0)
            {
                harshEnvironmentTimer = 10f;
                health -= 15;
                StartCoroutine(Screenoof(7f));
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
        
    }
    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("WaterDeath"))
        {
            deadAnim.SetTrigger("Dead");
            FindObjectOfType<PauseMenu>().canPause = false;
        }
        if (collision.gameObject.CompareTag("GoblinBox"))
        {
            
                health -= collision.gameObject.GetComponentInParent<AI>().damage;
            
            StartCoroutine(CameraShake.Shake(duration, magnitude));
            StartCoroutine(Screenoof(2));
        }
        if (collision.gameObject.CompareTag("Fire"))
        {
            Vector3 pos = transform.position;
            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            SavePlayer();
            transform.position = pos;
            FindObjectOfType<loading>().LoadLevelString("Fire");
        }
        if (collision.gameObject.CompareTag("Ice"))
        {
            Vector3 pos = transform.position;
            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
            SavePlayer();
            transform.position = pos;
            FindObjectOfType<loading>().LoadLevelString("Ice");
        }
        if (collision.gameObject.CompareTag("Thunder"))
        {
            Vector3 pos = transform.position;
            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            SavePlayer();
            transform.position = pos;
            FindObjectOfType<loading>().LoadLevelString("Thunder");
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            Vector3 pos = transform.position;
            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
            SavePlayer();
            transform.position = pos;
            FindObjectOfType<loading>().LoadLevelString("Water");
        }
        if (collision.gameObject.CompareTag("Rock"))
        {
            Vector3 pos = transform.position;
            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            SavePlayer();
            transform.position = pos;
            FindObjectOfType<loading>().LoadLevelString("Rock");
        }
        if (collision.gameObject.CompareTag("Nan"))
        {
            Vector3 pos = transform.position;
            inDungeon = true;
            transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            SavePlayer();
            transform.position = pos;
            FindObjectOfType<loading>().LoadLevelString("Nan");
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            inDungeon = false;
            FindObjectOfType<loading>().LoadLevelString("Overworld 3");
        }
        if (collision.gameObject.CompareTag("FireBoss"))
        {
            Vector3 pos = transform.position;
            LoadPlayer();
            fire = true;
            health = 100;
            SavePlayer();
            transform.position = pos;
            FindObjectOfType<loading>().LoadLevelString("Overworld 3");
        }
        if (collision.gameObject.CompareTag("WaterBoss"))
        {
            Vector3 pos = transform.position;
            LoadPlayer();
            health = 100;
            water = true;
            SavePlayer();
            transform.position = pos;
            FindObjectOfType<loading>().LoadLevelString("Overworld 3");
        }
        if (collision.gameObject.CompareTag("ThunderBoss"))
        {
            Vector3 pos = transform.position;
            LoadPlayer();
            health = 100;
            thunder = true;
            SavePlayer();
            transform.position = pos;
            FindObjectOfType<loading>().LoadLevelString("Overworld 3");
        }
        if (collision.gameObject.CompareTag("IceBoss"))
        {
            Vector3 pos = transform.position;
            LoadPlayer();
            health = 100;
            ice = true;
            SavePlayer();
            transform.position = pos;
            FindObjectOfType<loading>().LoadLevelString("Overworld 3");
        }
        if (collision.gameObject.CompareTag("RockBoss"))
        {
            Vector3 pos = transform.position;
            LoadPlayer();
            health = 100;
            rock = true;
            SavePlayer();
            transform.position = pos;
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
        if (collision.gameObject.CompareTag("HarshEnvironment"))
        {
            if(collision.gameObject.GetComponent<HarshEnvironment>() != null)
            {
                collision.gameObject.GetComponent<HarshEnvironment>().inArea = true;
            }
        }
        if (collision.gameObject.CompareTag("Boulder"))
        {
            health -= 20;
            StartCoroutine(Screenoof(2));
        }
        if (collision.gameObject.CompareTag("Candy"))
        {
            candy.targetAmount += Mathf.RoundToInt(collision.transform.localScale.x * 40);
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
        if (collision.gameObject.CompareTag("HarshEnvironment"))
        {
            if (collision.gameObject.GetComponent<HarshEnvironment>() != null)
            {
                collision.gameObject.GetComponent<HarshEnvironment>().inArea = false;
            }
        }
    }
    private void OnParticleCollision(GameObject collision)
    {
        if (collision.CompareTag("Electricity"))
        {
            StartCoroutine(Screenoof(3));
            health -= 20;
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
        Debug.Log("Saved Player");
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
    public Ouch loadToVarible()
    {
        Ouch playerData = new Ouch();
        PlayerData data = SaveSystem.LoadPlayer();

        playerData.water = data.water;
        playerData.thunder = data.thunder;
        playerData.fire = data.fire;
        playerData.ice = data.ice;
        playerData.rock = data.rock;
        playerData.nan = data.nan;
        playerData.health = data.health;
        playerData.start = data.start;
        playerData.inDungeon = data.inDungeon;
        playerData.candy.targetAmount = data.candyAmount;
        playerData.candy.candyAmount = data.candyAmount;

        return playerData;
    }
    public void LoadPlayerAndReload()
    {
        FindObjectOfType<loading>().LoadSameLevel();
    }
}