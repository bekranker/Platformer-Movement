using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindingMaster : MonoBehaviour
{
    [SerializeField] List<KeybindingScript> keybindingScripts;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("First") == 0)
        {
            PlayerPrefs.SetInt("First", 1);
            ResetDefaults();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetDefaults()
    {
        foreach (KeybindingScript keybindingScript in keybindingScripts)
        {
            keybindingScript.ResetDefault();
        }
    }
}
