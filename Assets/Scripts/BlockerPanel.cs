using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlockerPanel : MonoBehaviour
{

    public Text LoadingText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingScreen());
    }


    IEnumerator LoadingScreen()
    {
        LoadingText.text = "L o a d i n g";
        yield return new WaitForSeconds(1f);
        LoadingText.text = "L o a d i n g .";
        yield return new WaitForSeconds(1f);
        LoadingText.text = "L o a d i n g . .";
        yield return new WaitForSeconds(1f);
        LoadingText.text = "L o a d i n g . . .";
        yield return new WaitForSeconds(1f);

        
        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
            if(GameObject.FindGameObjectsWithTag("EnemyMonster").Length <= 0)
                Application.LoadLevel("NewMap");
    }
}


