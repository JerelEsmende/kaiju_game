using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class Clickables : MonoBehaviour {
    
    public bool timeFinished;
    void Start()
    {
        StartCoroutine(turnOn());
    }

    IEnumerator turnOn()
    {
        yield return new WaitForSeconds(4f);
        timeFinished = true;
    }
    
    public void OnMouseDown()
    {
        if (timeFinished)
        {
            ClickScene();
        }
    }

    private void ClickScene()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Animation_Battle");
    }

}

