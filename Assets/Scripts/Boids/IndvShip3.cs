using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndvShip3 : MonoBehaviour
{
    public float spd = 0.7f;
    float rotationSpd = 4f;
    float neighborDist = 5f;
    bool turning = false;

    void Start()
    {
        spd = Random.Range(5f, 5.5f);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= GlobArmada.spaceSize)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }
        if (turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                                                  Quaternion.LookRotation(direction), 
                                                  rotationSpd * Time.deltaTime);
            spd = Random.Range(5f, 5.5f);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
            {
                newRegulations();
            }
        }
        transform.Translate(0, 0, Time.deltaTime * spd);
    }

    void newRegulations()
    {
        //GameObject[] ships;
        GameObject[] ships3;

        //ships = GlobArmada.allFleet;
        ships3 = GlobArmada.allFleet3;

        Vector3 center = Vector3.zero;
        Vector3 socDist = Vector3.zero;

        Vector3 goal = GlobArmada.goal3;
        float gSpd = 0.1f;

        float distance;

        int squadSize = 0;

        /*if(Vector3.Distance(GlobArmada.goal, this.transform.position) > 
                            Vector3.Distance(GlobArmada.goal2, this.transform.position))
        {
            goal = GlobArmada.goal;
        }

        else
        {
            goal = GlobArmada.goal2;
        }*/

        foreach (GameObject ship in ships3)
        {
            if (ship != this.gameObject)
            {
                distance = Vector3.Distance(ship.transform.position, this.transform.position);
                if (distance <= neighborDist)
                {
                    center += ship.transform.position;
                    squadSize++;

                    if (distance < 1f)
                    {
                        socDist = socDist + (this.transform.position - ship.transform.position);
                    }

                    IndvShip3 otherShip = ship.GetComponent<IndvShip3>();
                    gSpd = gSpd + otherShip.spd;
                }
            }
        }

        if (squadSize > 0)
        {
            center = center / squadSize + (goal - this.transform.position);
            spd = gSpd / squadSize;

            Vector3 direction = (center + socDist) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, 
                                                      Quaternion.LookRotation(direction), 
                                                      rotationSpd * Time.deltaTime);
            }

            else if (GlobArmada.FollowTheLeader)
            {
                transform.RotateAround(goal, Vector3.forward, Time.deltaTime * rotationSpd);
            }
        }
    }
}
