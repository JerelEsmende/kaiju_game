using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SpawnMonsters : MonoBehaviour
{

    public GameObject MonsterPrefab;
    public Vector3 center;
    public Vector3 size;

    public static int times = 5;            //number of encounters, due to static is not scaleable through inspector

    public static GameObject[] EnCount = new GameObject[times];          //Stores encounters into indexes for later useage

    void Start()
    {
        //DoDaThing();

        for (int i = 0; i < times; i++)
        {
            Vector3 pos = transform.localPosition + center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 
                                                                         Random.Range(-size.y / 2, size.y / 2),
                                                                         Random.Range(-size.z / 2, size.z / 2));
            EnCount[i] = (GameObject)Instantiate(MonsterPrefab, pos, Quaternion.identity);
        }
    }


    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("EnemyMonster").Length <= 0)
            SceneManager.LoadScene("NewMap");
    }

    /*public void DoDaThing()
    {
        for (int i = 0; i < times; i++)
        {
            spawnMonsters();
        }
    }

    public void spawnMonsters()
    {
        Vector3 pos = transform.localPosition + center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2),
            Random.Range(-size.z / 2, size.z / 2));
        EnCount[i] = Instantiate(MonsterPrefab, pos, Quaternion.identity);
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.07f, 0.16f, 0.48f);
        Gizmos.DrawCube(transform.localPosition + center,size);
    }
}
