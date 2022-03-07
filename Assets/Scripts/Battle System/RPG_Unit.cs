using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG_Unit : MonoBehaviour
{
    
    
    //public int unitLevel;
    public string unitName;

    public float maxHP;
    public float currentHP;

    public float attackStat;
    public float defenseStat;
    public float SpStat;



    public void HPChange(float changeValue)
    {
        currentHP += changeValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject thePlayer = GameObject.FindWithTag("PlayerMonster");
        MonsterInfo monsterinfo = thePlayer.GetComponent<MonsterInfo>();
        
        maxHP = monsterinfo.P_maxHealth;
        attackStat = monsterinfo.P_Attack;
        defenseStat = monsterinfo.P_defense;
        SpStat = monsterinfo.P_SpAtk;
        unitName = monsterinfo.MonsterType;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
