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
public class MovementBehaviour : MonoBehaviour
{
    public string PlayerName;
    [SerializeField] Controls control;
    public float x, y, RR, RL, JumpDown,Jump, Attack,AttackDown, Settings, Cancel,CancelDown;

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
            Settings = Input.GetAxis(control + "Options");
            
            if (control == Controls.Xbox || control == Controls.Xbox2)
            {              
                if(Input.GetButtonDown(control + "ButtonA"))
                {
                    JumpDown = 1;
                }
                else
                {
                    JumpDown = 0;
                }

                if (Input.GetButtonDown(control + "ButtonB"))
                {
                    CancelDown = 1;
                }
                else
                {
                    CancelDown = 0;
                }

                if (Input.GetButtonDown(control + "ButtonX"))
                {
                    AttackDown = 1;
                }
                else
                {
                    AttackDown = 0;
                }
                Jump = Input.GetAxis(control + "ButtonA");
                Attack = Input.GetAxis(control + "ButtonX");
                Cancel = Input.GetAxis(control + "ButtonB");
            }
            else if (control == Controls.Ps || control == Controls.Ps2)
            {
                if(Input.GetButtonDown(control + "ButtonX"))
                {
                    JumpDown = 1;
                }
                else
                {
                    JumpDown = 0;
                }

                if (Input.GetButtonDown(control + "ButtonO"))
                {
                    CancelDown = 1;
                }
                else
                {
                    CancelDown = 0;
                }


                if (Input.GetButtonDown(control + "ButtonSquare"))
                {
                    AttackDown = 1;
                }
                else
                {
                    AttackDown = 0;
                }
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

            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "Jump"))))
            {
                JumpDown = 1;
            }
            else
            {
                JumpDown = 0;
            }

            if (Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "Jump"))))
            {
                Jump = 1;
            }
            else
            {
                Jump = 0;
            }

            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "Cancel"))))
            {
                CancelDown = 1;
            }
            else
            {
                CancelDown = 0;
            }

            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(control + "Attack"))))
            {
                AttackDown = 1;
            }
            else
            {
                AttackDown = 0;
            }
        }

        #endregion
    }

    public void GetControl(Controls Newcontrols)
    {
        control = Newcontrols;
    }
    public Controls GiveControl()
    {
        return control;
    }
}