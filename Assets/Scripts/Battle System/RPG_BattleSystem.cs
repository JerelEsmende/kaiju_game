using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RPG_BattleSystem : MonoBehaviour
{

    public GameObject playerObject;
    private RPG_Unit playerUnit;
    public Transform playerBattleStation;
    public Sprite playerSprite;
    public RPG_HUD playerHUD;

    public GameObject enemyObject;
    private RPG_Unit enemyUnit;
    public Transform enemyBattleStation;
    public Sprite enemySprite;
    public RPG_HUD enemyHUD;

    public Text dialogueText;

    //public GameObject playerPlatform;
    //public GameObject enemyPlatform;

    public GameObject playerButtonCanvas;
    public AudioSource soundSource;
    public AudioClip attackSound;

    public AudioClip anglerSound;
    public AudioClip crabSound;
    public AudioClip lobsterSound;
    public AudioClip turtleSound;
    public AudioClip snailSound;
    public AudioClip healSound;
    public AudioClip blockSound;
    public AudioClip deathSound;
    
    
    public enum BattleState
    {
        Start,
        Player,
        Enemy,
        Won,
        Lost
    }

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.Start;
        playerButtonCanvas.SetActive(false);
        soundSource = GetComponent<AudioSource>();

        StartCoroutine(SetupBattle());
    }
    
    

    
    IEnumerator SetupBattle()
    {
        //playerPlatform = GameObject.Find("Player BattleStation");
        //enemyPlatform = GameObject.Find("Enemy BattleStation");
        
        GameObject thePlayer = GameObject.FindWithTag("PlayerMonster");
        MonsterInfo monsterinfo = thePlayer.GetComponent<MonsterInfo>();
        monsterCustoms(monsterinfo.MonsterType);
        playerUnit.unitName = monsterinfo.MonsterType;
        
        
        
        GameObject theEnemy = GameObject.FindWithTag("EnemyMonster");
        EnemyInfo enemyInfo = theEnemy.GetComponent<EnemyInfo>();
        enemyCustoms(enemyInfo.enemyValue);
        enemyUnit.unitName = enemyInfo.enemyType;
        
        
//--------this is where the other stats was set--------------------

        
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        dialogueText.text = "BATTLE START!";

        yield return new WaitForSeconds(3f);

        
        SpecialCounter.text = SpecialGauge.ToString();

        state = BattleState.Player;
        PlayerTurn();
    }


    void PlayerTurn()
    {
        playerButtonCanvas.SetActive(true);
        SPDisable();
        HealDisable();
        dialogueText.text = "Select an action.";
        
    }


    public Animator EnemyAnimeStation;
    IEnumerator EnemyTurn()
    {
        if (state == BattleState.Enemy)
        {
            playerButtonCanvas.SetActive(false);


            GameObject thePlayer = GameObject.FindWithTag("PlayerMonster");
            MonsterInfo monsterinfo = thePlayer.GetComponent<MonsterInfo>();

            GameObject theEnemy = GameObject.FindWithTag("EnemyMonster");
            EnemyInfo enemyInfo = theEnemy.GetComponent<EnemyInfo>();

            dialogueText.text = "ENEMY TURN";


            dialogueText.text = "The enemy " + enemyInfo.enemyType + " attacks!";

            EnemyAnimeStation.Play("EnemyAttack", 0);
            yield return new WaitForSeconds(0.5f);
            //does movement here

            if (!DefenceStance)
            {
                
                soundSource.PlayOneShot(attackSound, 0.5f);

                playerUnit.HPChange(monsterinfo.P_defense - enemyUnit.attackStat);
                playerHUD.SetHP(playerUnit.currentHP);
                CountSpecial();
                CountHeal();

                state = BattleState.Player;
                PlayerTurn();
            }

            if (DefenceStance)
            {
                soundSource.PlayOneShot(blockSound, 0.5f);

                dialogueText.text = monsterinfo.MonsterType + " blocked the attack!"; 
                DefenceStance = false;
                yield return new WaitForSeconds(1f);

                CountSpecial(); 
                CountHeal();
                
                state = BattleState.Player; 
                PlayerTurn(); 
            } 
        }
    }


    public void OnAttackButton()
    {
        playerButtonCanvas.SetActive(false);
        
        if (state == BattleState.Player)
        {
            StartCoroutine(AttackMove());
            
        }
    }

    public bool DefenceStance;
    public void OnDefendButton()
    {
        playerButtonCanvas.SetActive(false);

        if (state == BattleState.Player)
        {
            DefenceStance = true;
            state = BattleState.Enemy;
            StartCoroutine(EnemyTurn());
        }
    }


    IEnumerator AttackMove()
    {
        playerButtonCanvas.SetActive(false);

        GameObject thePlayer = GameObject.FindWithTag("PlayerMonster");
        MonsterInfo monsterinfo = thePlayer.GetComponent<MonsterInfo>();
        

        dialogueText.text = monsterinfo.MonsterType + " attacks!";
        yield return new WaitForSeconds(0.5f);

        //sound hit at 0.5secs
        soundSource.PlayOneShot(attackSound, 0.5f);
        enemyUnit.HPChange(enemyUnit.defenseStat - playerUnit.attackStat);
        enemyHUD.SetHP(enemyUnit.currentHP);
        yield return new WaitForSeconds(0.5f);
        
        yield return new WaitForSeconds(1f);
        
        playerButtonCanvas.SetActive(false);

        state = BattleState.Enemy;
        StartCoroutine(EnemyTurn());
    }
 
    
    public int HealGauge;
    public Text HealCounter;
    public Button HealButton;
    public void OnHealButton()
    {
        playerButtonCanvas.SetActive(false);
        if (state == BattleState.Player && HealGauge >= 3)
        {
           StartCoroutine(HealMove());
           HealGauge =  0;
        }
    }

    IEnumerator HealMove()
    {
        yield return new WaitForSeconds(1f);
        
        soundSource.PlayOneShot(healSound, 0.5f);
        playerUnit.HPChange(20);


        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);
        
        
        state = BattleState.Enemy;
        StartCoroutine(EnemyTurn());
    }
    
    public void HealDisable()
    {
        if (HealGauge < 3)
        {
            HealButton.interactable = false;
        }
        else
        {
            HealButton.interactable = true;
        }
    }
    
    public void CountHeal()
    {
        HealGauge += 1;
        HealCounter.text = HealGauge.ToString();
        if (HealGauge >= 3)
        {
            HealCounter.text = "R E A D Y";
        }
    }
    
    public void SpecialButton()
    {
        playerButtonCanvas.SetActive(false);
        if (state == BattleState.Player && SpecialGauge >= 3)
        {
            StartCoroutine(SpecialMove());
            SpecialGauge = 0;
        }
    }
    
    public Button spButton;

    public void SPDisable()
    {
        if (SpecialGauge < 3)
        {
            spButton.interactable = false;
        }
        else
        {
            spButton.interactable = true;
        }
    }
    
    public int SpecialGauge;
    public Text SpecialCounter;
    public void CountSpecial()
    {
        SpecialGauge += 1;
        SpecialCounter.text = SpecialGauge.ToString();
        if (SpecialGauge >= 3)
        {
            SpecialCounter.text = "R E A D Y";
        }
    }
    
    public Animator PlayerAnimeStation;
   // public Animator EffectsAnimator;
    IEnumerator SpecialMove()
    {
        GameObject thePlayer = GameObject.FindWithTag("PlayerMonster");
        MonsterInfo monsterinfo = thePlayer.GetComponent<MonsterInfo>();
        

        //wait and do animations for a certain time
        if (monsterinfo.MonsterType == "Crab") 
        {
            
            PlayerAnimeStation.Play("CrabSpecial", 0);
            yield return new WaitForSeconds(1f);
        
            soundSource.PlayOneShot(crabSound, 0.5f);
        }

        if (monsterinfo.MonsterType == "Lobster") 
        {
            PlayerAnimeStation.Play("LobsterSpecial", 0);
            yield return new WaitForSeconds(1f);
        
            soundSource.PlayOneShot(lobsterSound, 0.5f);
        }
        
        if (monsterinfo.MonsterType == "Angler") 
        {
            PlayerAnimeStation.Play("AnglerSpecial", 0);
            yield return new WaitForSeconds(1f);
        
            soundSource.PlayOneShot(anglerSound, 0.5f);
        }
        if (monsterinfo.MonsterType == "Turtle") 
        {

            PlayerAnimeStation.Play("TurtleSpecial", 0);
            yield return new WaitForSeconds(0.8f);
        
            soundSource.PlayOneShot(turtleSound, 1f);
            yield return new WaitForSeconds(0.1f);
            soundSource.PlayOneShot(turtleSound, 1f);
            yield return new WaitForSeconds(0.1f);
            soundSource.PlayOneShot(turtleSound, 1f);
        }
        if (monsterinfo.MonsterType == "Snail")
        {
            PlayerAnimeStation.Play("CrabSpecial", 0);
            yield return new WaitForSeconds(1f);
        
            soundSource.PlayOneShot(snailSound, 0.5f);
        }
        
        dialogueText.text = monsterinfo.MonsterType + " used its special attack!";
        
        
        //this hit should happen at 1 sec
        enemyUnit.HPChange(enemyUnit.defenseStat - playerUnit.SpStat * 2);
        enemyHUD.SetHP(enemyUnit.currentHP);
        
        
        yield return new WaitForSeconds(1f);
        
        state = BattleState.Enemy;
        StartCoroutine(EnemyTurn());
    }

    
    //--------------------------Monster Sprite Stuff-------------------------------------------
    
    public Sprite imagineCrabs;
    public Sprite imagineLobsters;
    public Sprite imagineAnglers;
    public Sprite imagineSnails;
    public Sprite imagineTurtles;
    public void monsterCustoms(string type)
    {
        
        GameObject playerGO = Instantiate(playerObject, playerBattleStation);
        playerUnit = playerGO.GetComponent<RPG_Unit>();
        playerGO.GetComponent<SpriteRenderer>().sprite = playerSprite;
        
        if (type == "Crab")
        { 
            Debug.Log("Change Sprite Crab");
           playerGO.GetComponent<SpriteRenderer>().sprite = imagineCrabs;

        }
        if (type == "Lobster")
        {
            Debug.Log("Change Sprite Lobster");
           playerGO.GetComponent<SpriteRenderer>().sprite = imagineLobsters;
        }
        if (type == "Angler")
        {
            Debug.Log("Change Sprite Angler");
           playerGO.GetComponent<SpriteRenderer>().sprite = imagineAnglers;
        }
        if (type == "Snail")
        {
            Debug.Log("Change Sprite Snail");
            playerGO.GetComponent<SpriteRenderer>().sprite = imagineSnails;
        }
        if (type == "Turtle")
        {
            Debug.Log("Change Sprite Turtle");
            playerGO.GetComponent<SpriteRenderer>().sprite = imagineTurtles;
        }
        
    }

    
    private SpriteRenderer enemyRenderer;
    public void enemyCustoms(int enemyValue)
    {
        GameObject enemyGO = Instantiate(enemyObject, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<RPG_Unit>();
        
        enemyGO.GetComponent<SpriteRenderer>().sprite = enemySprite;
        
        if (enemyValue == 1)
        {
            Debug.Log("Change Enemy Sprite Crab");
            enemyGO.GetComponent<SpriteRenderer>().sprite = imagineCrabs;
        }
        if (enemyValue == 2)
        {
            Debug.Log("Change Enemy Sprite Lobster");
            enemyGO.GetComponent<SpriteRenderer>().sprite = imagineLobsters;
        }
        if (enemyValue == 3)
        {
            Debug.Log("Change Enemy Sprite Angler");
            enemyGO.GetComponent<SpriteRenderer>().sprite = imagineAnglers;
        }
        if (enemyValue == 4)
        {
            Debug.Log("Change Enemy Sprite Snail");
            enemyGO.GetComponent<SpriteRenderer>().sprite = imagineSnails;
        }
        if (enemyValue == 5)
        {
            Debug.Log("Change Enemy Sprite Snail");
            enemyGO.GetComponent<SpriteRenderer>().sprite = imagineTurtles;
        }
    }
//---------------------------End of Monster Sprite Stuff-------------------------------------------


    // Update is called once per frame
    void Update()
    {
        if (playerUnit.currentHP <= 0)
        {
            playerButtonCanvas.SetActive(false);
            state = BattleState.Lost;
            dialogueText.text = "YOU LOSE";
            SceneManager.LoadScene("NewMainMenu");
        }

        if (enemyUnit.currentHP <= 0)
        {
            playerButtonCanvas.SetActive(false);

            state = BattleState.Won;
            dialogueText.text = "YOU WON";
            
            StartCoroutine(Deathroll());
            
        
            //SceneManager.LoadScene("NewMap");
        }
        
    }
    
    
    public GameObject StationToKEEL;
    IEnumerator Deathroll()
    {
        soundSource.PlayOneShot(deathSound, 0.5f);

        StationToKEEL.GetComponent<Animator>().enabled = false;
        StationToKEEL.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(3f);
        Destroy(GameObject.FindWithTag("EnemyMonster"));
        SceneManager.LoadScene("NewMap");
    } 
    
    


}
