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
            Debug.Log("A");
        }
    }
}
