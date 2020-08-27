using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class Ouch : MonoBehaviour
{
    public int health = 100;
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
    public timePlace timeInPlace;
    public Animator animationArea;
    public CandyCounter candy;
    Rigidbody Rigid;
    
    void Awake()
    {
        candy = FindObjectOfType<CandyCounter>();
        Rigid = GetComponent<Rigidbody>();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Screen.weight = 0;

        //load
        
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
                candy.targetAmount = 0;
                candy.candyAmount = 0;
                transform.position = position;
                if(FindObjectOfType<TimeCycle>() != null)
                FindObjectOfType<TimeCycle>().dayNum = 1;
                FindObjectOfType<TimeCycle>().currentTimeOfDay= 0.3f;
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
    void Update()
    {
        if (health <= 0)
        {
            LoadPlayer();
            health = 100;
            SavePlayer();
            FindObjectOfType<loading>().LoadLevelString("Death");
        }
        barSize = health;
        barSize = barSize / 100;
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

    }
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("WaterDeath"))
        {
            FindObjectOfType<loading>().LoadLevelString("Death");
        }
        if (collision.gameObject.CompareTag("GoblinBox"))
        {
            health -= GoblinDamage;
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
            health -= 1;
            StartCoroutine(Screenoof(0.5f));
        }
        if (collision.gameObject.CompareTag("Boulder"))
        {
            health -= 20;
            StartCoroutine(Screenoof(2));
        }
        if (collision.gameObject.CompareTag("Candy"))
        {
            health += 40;
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



    }

    IEnumerator Screenoof(float Sec)
    {

        for (float i = 1; i >= 0; i -= Time.deltaTime / Sec)
        {
            // set color with i as alpha
            Screen.weight = i;
            yield return null;
        }
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this, timeInPlace, candy.targetAmount);
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