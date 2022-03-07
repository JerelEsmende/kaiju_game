using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLand : MonoBehaviour
{


    void Update () {

    }
    void Start()
    {

    }


    private void OnCollisionEnter(Collision collision)

    {
        if(collision.gameObject.CompareTag("Land"))
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("water"))
        {
            Debug.Log("In water");
            GetComponent<SmoothSineMovement>().enabled = true;
        }
    } 
}
