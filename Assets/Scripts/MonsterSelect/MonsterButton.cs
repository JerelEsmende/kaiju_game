using CharacterSelector.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterButton : MonoBehaviour {
    
    public void SwitchCharacter()
    {
        //Finds the text on the button and sets it to the monstertype
        string characterName = transform.Find("Text").GetComponent<Text>().text;

        MonsterManager.Instance.SetCurrentCharacterType(characterName);

    }

    public void CreateCharacter()
    {
        MonsterManager.Instance.CreateCurrentCharacter();
    }
}
