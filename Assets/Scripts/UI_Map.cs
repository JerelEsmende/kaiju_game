using System;
using System.Collections;
using System.Collections.Generic;
using CharacterSelector.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UI_Map : MonoBehaviour
{
    public Image myImage;
    public Sprite imagineCrabs;
    public Sprite imagineLobsters;
    public Sprite imagineAnglers;
    public Sprite imagineSnails;
    public Sprite imagineTurtles;
    
    public Text Attac;
    public Text Protec;
    public Text maxHealth;
    public Text SpAtk;

    public void StartAFight()
    {
        SceneManager.LoadScene("BattleSceneUnified");
    }

    public void BackToMain()
    {
        ClearMonster();
        SceneManager.LoadScene("NewMainMenu");
    }

    private void Start()
    {
        LoadMonster();
        MonsterSearch();
        printStats();
    }
    
    public GameObject SpawnPoint;

    public void LoadMonster()
    {
        MonsterInfo currentMonster = MonsterManager.Instance.GetCurrentCharacter();
        SpawnPoint = GameObject.Find("SpawnPoint");
        currentMonster.transform.position = SpawnPoint.transform.position;
        currentMonster.gameObject.SetActive(true);
    }

    public void MonsterSearch()
    {
        GameObject thePlayer = GameObject.FindWithTag("PlayerMonster");
        MonsterInfo monsterinfo = thePlayer.GetComponent<MonsterInfo>();
        Debug.Log(monsterinfo.MonsterType);
        string type = monsterinfo.MonsterType;
        
        monsterCustoms(type);
    }


    public void monsterCustoms(string type)
    {
        if (type == "Crab")
        {
            Debug.Log("ChangeImageCrab");
            myImage.sprite = imagineCrabs;
            
        }
        if (type == "Lobster")
        {
            Debug.Log("ChangeImageLobster");
            myImage.sprite = imagineLobsters;
        }
        if (type == "Angler")
        {
            Debug.Log("ChangeImageAngler");
            myImage.sprite = imagineAnglers;
        }
        if (type == "Snail")
        {
            Debug.Log("ChangeImageSnail");
            myImage.sprite = imagineSnails;
        }
        if (type == "Turtle")
        {
            Debug.Log("ChangeImageSnail");
            myImage.sprite = imagineTurtles;
        }
    }

    public void printStats()
    {
        GameObject thePlayer = GameObject.FindWithTag("PlayerMonster");
        MonsterInfo monsterinfo = thePlayer.GetComponent<MonsterInfo>();
        Attac.text = "ATK : " + monsterinfo.P_Attack;
        Protec.text = "DEF : " + monsterinfo.P_defense;
        maxHealth.text = "HP : " + monsterinfo.P_maxHealth;
        SpAtk.text = "SP.ATK : " + monsterinfo.P_SpAtk;
    }


    public void ClearMonster()
    {
        GameObject tmp  = GameObject.FindWithTag("PlayerMonster");
        Destroy(tmp);
    }



}
