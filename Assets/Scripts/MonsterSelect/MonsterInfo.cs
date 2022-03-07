using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour {

    public string MonsterType;
    
    
    
    public float P_maxHealth;
    public float currentHP;

    public float P_Attack;
    public float P_defense;

    public float P_SpAtk;
    
    public void HPChange(float changeValue)
    {
        currentHP += changeValue;
    }
    
}
