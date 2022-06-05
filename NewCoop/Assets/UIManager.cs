using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    float delay;
    [SerializeField] float Maxdelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            delay = 0;
        }
    }

    public bool isReady()
    {
        Debug.Log("Kontrol");
        if (delay <= 0)
        {
            delay = Maxdelay;
            return true;
        }
        return false;
    }
}
