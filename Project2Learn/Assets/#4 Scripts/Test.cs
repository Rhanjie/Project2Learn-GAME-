using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{

    GameObject objectTest = null;

    // Use this for initialization
    void Start()
    {
        objectTest = GameObject.FindGameObjectWithTag("MainCamera");

        //objectTest.GetComponent<LookAt>().speedX=20;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            objectTest.GetComponent<LookAt>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            objectTest.GetComponent<LookAt>().enabled = true;
        }
    }
}