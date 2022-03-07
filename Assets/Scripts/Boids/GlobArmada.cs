using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobArmada : MonoBehaviour
{
    public GameObject shipFab;
    public GameObject shipFab2;
    public GameObject shipFab3;
    public GameObject shipFab4;
    public GameObject shipFab5;

    /*public GameObject goalFab;
    public GameObject goalFab2;
    public GameObject goalFab3;
    public GameObject goalFab4;
    public GameObject goalFab5;*/

    public static int radius = 5;
    public static int spaceSize = 300;

    static int fleetSize = 55;
    static int fleetSize2 = 55;
    static int fleetSize3 = 55;
    static int fleetSize4 = 55;
    static int fleetSize5 = 55;

    public static GameObject[] allFleet = new GameObject[fleetSize];
    public static GameObject[] allFleet2 = new GameObject[fleetSize2];
    public static GameObject[] allFleet3 = new GameObject[fleetSize3];
    public static GameObject[] allFleet4 = new GameObject[fleetSize4];
    public static GameObject[] allFleet5 = new GameObject[fleetSize5];

    public static Vector3 goal = Vector3.zero;
    public static Vector3 goal2 = Vector3.zero;
    public static Vector3 goal3 = Vector3.zero;
    public static Vector3 goal4 = Vector3.zero;
    public static Vector3 goal5 = Vector3.zero;

    public static bool FollowTheLeader = false;

    void Start()
    {
        for (int i = 0; i < fleetSize; i++)
        {
            allFleet[i] = (GameObject)Instantiate(shipFab,
                                                   Random.insideUnitSphere * radius + SpawnMonsters.EnCount[0].transform.position,
                                                   Random.rotation);
        }

        for (int j = 0; j < fleetSize2; j++)
        {
            allFleet2[j] = (GameObject)Instantiate(shipFab2,
                                                   Random.insideUnitSphere * radius + SpawnMonsters.EnCount[1].transform.position,
                                                   Random.rotation);
        }

        for (int k = 0; k < fleetSize2; k++)
        {
            allFleet3[k] = (GameObject)Instantiate(shipFab3,
                                                   Random.insideUnitSphere * radius + SpawnMonsters.EnCount[2].transform.position,
                                                   Random.rotation);
        }

        for (int l = 0; l < fleetSize2; l++)
        {
            allFleet4[l] = (GameObject)Instantiate(shipFab4,
                                                   Random.insideUnitSphere * radius + SpawnMonsters.EnCount[3].transform.position,
                                                   Random.rotation);
        }

        for (int m = 0; m < fleetSize2; m++)
        {
            allFleet5[m] = (GameObject)Instantiate(shipFab5,
                                                   Random.insideUnitSphere * radius + SpawnMonsters.EnCount[4].transform.position,
                                                   Random.rotation);
        }
    }

    void Update()
    {
        goal = SpawnMonsters.EnCount[0].transform.position;
        goal2 = SpawnMonsters.EnCount[1].transform.position;
        goal3 = SpawnMonsters.EnCount[2].transform.position;
        goal4 = SpawnMonsters.EnCount[3].transform.position;
        goal5 = SpawnMonsters.EnCount[4].transform.position;
    }
}
