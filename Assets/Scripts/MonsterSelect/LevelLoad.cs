using CharacterSelector.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoad : MonoBehaviour {

    public Text txtType;

    public Text txtName;

   // public Text txtDescription;

    public GameObject SpawnPoint;

    // Use this for initialization
    void Start () {

        MonsterInfo currentMonster = MonsterManager.Instance.GetCurrentCharacter();

        SpawnPoint = GameObject.Find("SpawnPoint");
        
        currentMonster.transform.position = SpawnPoint.transform.position;

        currentMonster.gameObject.SetActive(true);

       // txtType.text = "Type: " + currentMonster.MonsterType;

       // txtName.text = "Name: " + currentMonster.Name;

       // txtDescription.text = "Descripton: " + currentMonster.Description;
       
		
	}
	
}
