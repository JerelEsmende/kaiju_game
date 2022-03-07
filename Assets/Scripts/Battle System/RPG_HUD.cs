using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPG_HUD : MonoBehaviour
{
    public Text nameText;
    public Slider HPBar;

    /*
    public void SetHUD(RPG_Unit unit)
    {
        nameText.text = unit.unitName;
        HPBar.maxValue = unit.maxHP;
        HPBar.value = unit.currentHP;
    }
    */
    
    public void SetHUD(RPG_Unit unit)
    {  
        //GameObject thePlayer = GameObject.FindWithTag("PlayerMonster");
       // MonsterInfo monsterinfo = thePlayer.GetComponent<MonsterInfo>();
        
        //GameObject theEnemy = GameObject.FindWithTag("EnemyMonster");
       //EnemyInfo enemyInfo = theEnemy.GetComponent<EnemyInfo>();
        
        
        
        nameText.text = unit.unitName;
        HPBar.maxValue = unit.maxHP;
        HPBar.value = unit.currentHP;
    }
    

    public void SetHP(float hp)
    {
        HPBar.value = hp;
    }


}
