using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Controls
{
    Keyboard1,
    Keyboard2,
    Xbox,
    Xbox2,
    Ps,
    Ps2
}
public class Movement : MonoBehaviour
{
    [SerializeField] Controls control;
    public float x, y, RR, RL, Jump, Attack, Settings, Cancel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float x,y,RR,RL,Jump,Attack,Settings,Cancel;

        #region GetInput
        if (control != Controls.Keyboard1 && control != Controls.Keyboard2)
        {
            x = Input.GetAxis(control+ "Horizontal");
            y = Input.GetAxis(control + "Vertical");
            RR = Input.GetAxis(control + "RT");
            RL = Input.GetAxis(control + "LT");
            Settings = Input.GetAxis(control + "Options"); // RT
            
            if (control == Controls.Xbox || control == Controls.Xbox2)
            {
                Jump = Input.GetAxis(control + "ButtonA");
                Attack = Input.GetAxis(control + "ButtonX"); //Y
                Cancel = Input.GetAxis(control + "ButtonB");
            }
            else if (control == Controls.Ps || control == Controls.Ps2)
            {
                Jump = Input.GetAxis(control + "ButtonX");
                Attack = Input.GetAxis(control + "ButtonSquare");
                Cancel = Input.GetAxis(control + "ButtonO");
            }
        }
        else
        {
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString(control + "Right"))))
            {
                x = 1;
            }
            else if(Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "Left"))))
            {
                x = -1;
            }
            else
            {
                x = 0;
            }

            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "Up"))))
            {
                y = 1;
            }
            else if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "Down"))))
            {
                y = -1;
            }
            else
            {
                y = 0;
            }

            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "RotateRight"))))
            {
                RR = 1;
            }
            else
            {
                RR = 0;
            }
            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "RotateLeft"))))
            {
                RL = 1;
            }
            else
            {
                RL = 0;
            }

            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "Options"))))
            {
                Settings = 1;
            }
            else
            {
                Settings = 0;
            }

            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "Cancel"))))
            {
                Cancel = 1;
            }
            else
            {
                Cancel = 0;
            }

            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "Attack"))))
            {
                Attack = 1;
            }
            else
            {
                Attack = 0;
            }

            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "Jump"))))
            {
                Jump = 1;
            }
            else
            {
                Jump = 0;
            }
        }

        #endregion
    }

    public void GetControl(Controls Newcontrols)
    {
        control = Newcontrols;
    }
}
