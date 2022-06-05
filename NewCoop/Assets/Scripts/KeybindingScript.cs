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
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString(Player + button.ToString()) != "")
        {
            GetComponentInChildren<TextMeshProUGUI>().text = PlayerPrefs.GetString(Player + button.ToString());
        }
    }
        

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "Press a Button...";
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    GetComponentInChildren<TextMeshProUGUI>().text = vKey.ToString();
                    PlayerPrefs.SetString(Player + button.ToString(), vKey.ToString());
                    active = false;
                }
            }            
        }
    }

    public void OnClick()
    {
        active = true;
    }

    public void ResetDefault()
    {

    }
}
