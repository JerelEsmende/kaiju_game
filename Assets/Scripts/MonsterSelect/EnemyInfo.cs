using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyInfo : MonoBehaviour
{
    
    public string enemyType;
    public int enemyValue;

    public float maxHP = 100;
    public float currentHP = 100;

    public float attackStat = 20;
    public float defenseStat = 10;
    public float SpAtk = 10;

    public void Start()
    {
        enemyValue = Random.Range(1, 6);
        determinetype();
    }
    
    public void HPChange(int changeValue)
    {
        currentHP += changeValue;
    }

    public void determinetype()
    {
        if (enemyValue == 1)
        {
            enemyType = "Crab";
        }
        if (enemyValue == 2)
        {
            enemyType = "Lobster";
        }
        if (enemyValue == 3)
        {
            enemyType = "Angler";
        }
        if (enemyValue == 4)
        {
            enemyType = "Snail";
        }
        if (enemyValue == 5)
        {
            enemyType = "Turtle";
        }
    }
    
    
    
}
