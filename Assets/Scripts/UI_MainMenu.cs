using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    public GameObject Panel_Menu;
    public GameObject Panel_Instructions;
    public GameObject Panel_MonsterMenu;
    public GameObject Menu_Background;

    public GameObject spawn;

    private void Awake()
    {
        Panel_Menu.gameObject.SetActive(true);
        Panel_Instructions.gameObject.SetActive(false);
        Panel_MonsterMenu.gameObject.SetActive(false);
        Menu_Background.gameObject.SetActive(true);
        
    }

    public void Start()
    {  
        ClearMonster();
    }

    public void Intructions()
    {
        Panel_Menu.gameObject.SetActive(false);
        Panel_Instructions.gameObject.SetActive(true);
        Panel_MonsterMenu.gameObject.SetActive(false);
        Menu_Background.gameObject.SetActive(true);
    }

    public void MainMenu()
    {
        Panel_Menu.gameObject.SetActive(true);
        Panel_Instructions.gameObject.SetActive(false);
        Panel_MonsterMenu.gameObject.SetActive(false);
        Menu_Background.gameObject.SetActive(true);
    }
    

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void StartOnline()
    {
        SceneManager.LoadScene("OnlineMenu");
    }

    public void StartGame()
    {
        ClearMonster();
        //SceneManager.LoadScene("MonsterSelect");
        Panel_Menu.gameObject.SetActive(false);
        Panel_Instructions.gameObject.SetActive(false);
        Panel_MonsterMenu.gameObject.SetActive(true);
        Menu_Background.gameObject.SetActive(false);
    }
    
    public void ClearMonster()
    {
        GameObject tmp  = GameObject.FindWithTag("PlayerMonster");
        Destroy(tmp);
    }
    

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = (false);

#else
        Application.Quit();
#endif

    }

}
