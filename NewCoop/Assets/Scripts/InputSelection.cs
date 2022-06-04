using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputSelection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI P1Text; 
    [SerializeField] TextMeshProUGUI P2Text;

    [SerializeField] TextMeshProUGUI ControllerDebug1;
    [SerializeField] TextMeshProUGUI ControllerDebug2;

    [SerializeField] string[] Players = new string[2];
    [SerializeField] string[] Controllers = new string[2];

    [SerializeField] GameObject SelectionPanel;
    [SerializeField] TextMeshProUGUI SelectionText;

    bool Wait;
    int CurrentController;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("Keyboard1" + "Jump"));
    }

    // Update is called once per frame
    void Update()
    {
        GetController();

        ControllerDebug1.text = Controllers[0];
        ControllerDebug2.text = Controllers[1];

        #region Join
        //Join
        if(PlayerPrefs.GetString("Keyboard1" + "Jump") != "")
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard1" + "Jump"))))
            {
                GetPlayer("Keyboard1");
            }
        }
        if (PlayerPrefs.GetString("Keyboard2" + "Jump") != "")
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard2" + "Jump"))))
            {
                GetPlayer("Keyboard2");
            }
        }

        if (Input.GetAxis("XboxButtonA") > 0 && Controllers[0] == "Xbox")
        {
            GetPlayer("Xbox");
        }
        if (Input.GetAxis("Xbox2ButtonA") > 0 && Controllers[1] == "Xbox") 
        {
            GetPlayer("Xbox2");
        }
        if (Input.GetAxis("PsButtonX") > 0 && Controllers[0] == "Ps")
        {
            GetPlayer("Ps");
        }
        if (Input.GetAxis("Ps2ButtonX") > 0 && Controllers[1] == "Ps")
        {
            GetPlayer("Ps2");
        }

        #endregion

        #region Exit
        //Exit
        if (PlayerPrefs.GetString("Keyboard1" + "Jump") != "")
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard1" + "Cancel"))))
            {
                if(Players[0] == "Keyboard1")
                {
                    Players[0] = "";
                    P1Text.text = "P1";
                }
                else if(Players[1] == "Keyboard1")
                {
                    Players[1] = "";
                    P2Text.text = "P2";
                }
            }
        }

        if (PlayerPrefs.GetString("Keyboard2" + "Jump") != "")
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard2" + "Cancel"))))
            {
                if (Players[0] == "Keyboard2")
                {
                    Players[0] = "";
                    P1Text.text = "P1";
                }
                else if (Players[1] == "Keyboard2")
                {
                    Players[1] = "";
                    P2Text.text = "P2";
                }
            }
        }

        if (Input.GetAxis("XboxButtonB") > 0)
        {
            if (Players[0] == "Xbox")
            {
                Players[0] = "";
                P1Text.text = "P1";
            }
            else if (Players[1] == "Xbox")
            {
                Players[1] = "";
                P2Text.text = "P2";
            }
        }
        if (Input.GetAxis("Xbox2ButtonB") > 0)
        {
            if (Players[0] == "Xbox2")
            {
                Players[0] = "";
                P1Text.text = "P1";
            }
            else if (Players[1] == "Xbox2")
            {
                Players[1] = "";
                P2Text.text = "P2";
            }
        }
        if (Input.GetAxis("PsButtonO") > 0)
        {
            if (Players[0] == "Ps")
            {
                Players[0] = "";
                P1Text.text = "P1";
            }
            else if (Players[1] == "Ps")
            {
                Players[1] = "";
                P2Text.text = "P2";
            }
        }
        if (Input.GetAxis("Ps2ButtonO") > 0)
        {
            if (Players[0] == "Ps2")
            {
                Players[0] = "";
                P1Text.text = "P1";
            }
            else if (Players[1] == "Ps2")
            {
                Players[1] = "";
                P2Text.text = "P2";
            }
        }

        #endregion
    }

    void GetPlayer(string ControllerName)
    {
        if(Players[0] == "" && Players[1] != ControllerName)
        {
            Players[0] = ControllerName;
            P1Text.text = ControllerName;
            PlayerPrefs.SetString("P1", ControllerName);
        }
        else if(Players[1] == "" && Players[0] != ControllerName)
        {
            Players[1] = ControllerName;
            P2Text.text = ControllerName;
            PlayerPrefs.SetString("P2", ControllerName);
        }
    }

    void GetController()
    {

        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if(Input.GetJoystickNames()[i] != "" && Controllers[i] != Input.GetJoystickNames()[i])
            {
                Bas:
                SelectionPanel.SetActive(true);
                SelectionText.text = Input.GetJoystickNames()[i];
                Wait = true;
                if (Wait)
                {
                    goto Bas;
                }
            }
            else if (Input.GetJoystickNames()[i] == "")
            {
                Controllers[i] = "";
            }
        }

        //for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        //{
        //    if (Input.GetJoystickNames()[i].ToLower().Contains("xbox"))
        //    {
        //        Controllers[i] = "Xbox";
        //    }
        //    else if (Input.GetJoystickNames()[i].ToLower().Contains("dual"))
        //    {
        //        Controllers[i] = "Ps";
        //    }
        //    else
        //    {
        //        Controllers[i] = "";
        //    }
        //}
    }

    public void SelectXbox()
    {
        Wait = false;
        Controllers[CurrentController] = "Xbox";
    }
    public void SelectPs()
    {
        Wait = false;
        Controllers[CurrentController] = "Ps";
    }
}
