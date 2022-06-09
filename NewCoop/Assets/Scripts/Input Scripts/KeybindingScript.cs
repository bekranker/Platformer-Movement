using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

enum Buttons
{
    Up,
    Down,
    Right,
    Left,
    Jump,
    Attack,
    Cancel,
    RotateRight,
    RotateLeft,
    Options
}
public class KeybindingScript : MonoBehaviour
{
    [SerializeField] string Player;
    [SerializeField] Buttons button;
    [SerializeField] bool active;
    [SerializeField] string Default;

    [SerializeField] bool wait;

    KeybindingMaster keybindingMaster;
    string LastString;
    // Start is called before the first frame update
    void Start()
    {
        keybindingMaster = GameObject.FindGameObjectWithTag("KeybindingMaster").GetComponent<KeybindingMaster>();
        
        if (PlayerPrefs.GetString(Player + button.ToString()) != "")
        {
            GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString(Player + button.ToString());
        }
        LastString = GetComponentInChildren<TextMeshProUGUI>().text;
    }
        

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            keybindingMaster.IamOpened(this);
            GetComponentInChildren<TextMeshProUGUI>().text = "Press a Button...";
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    if (!vKey.ToString().Contains("Joystick"))
                    {
                        LastString = vKey.ToString();
                        GetComponentInChildren<TextMeshProUGUI>().text = vKey.ToString();
                        PlayerPrefs.SetString(Player + button.ToString(), vKey.ToString());
                        active = false;
                        if (button == Buttons.Jump)
                        {
                            wait = true;
                        }
                    }
                }
            }            
        }
    }

    public void OnClick()
    {
        Debug.Log("Click");
        if (wait)
        {          
            wait = false;
        }
        else
        {
            active = true;
        }
    }

    public void ResetDefault()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = Default;
        PlayerPrefs.SetString(Player + button.ToString(), Default);
        active = false;
    }

    public void Close()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = LastString;
        active = false;
    }
}
