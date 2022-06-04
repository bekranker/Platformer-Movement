using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("XboxLT") > 0)
        {
            Debug.Log("XboxLT");
        }
        if (Input.GetAxis("XboxRT") > 0)
        {
            Debug.Log("XboxRT");
        }
        if (Input.GetAxis("Xbox2LT") > 0)
        {
            Debug.Log("Xbox2LT");
        }
        if (Input.GetAxis("XboxButtonA") > 0)
        {
            Debug.Log("XboxButtonA");
        }
        if (Input.GetAxis("Xbox2ButtonA") > 0)
        {
            Debug.Log("Xbox2ButtonA");
        }
        if (Input.GetAxis("XboxHorizontal") != 0)
        {
            
        }
        if (Input.GetAxis("Xbox2Horizontal") != 0)
        {
            
        }
    }
}
