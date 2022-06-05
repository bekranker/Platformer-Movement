using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XInputDotNetPure;

public class InputSelection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI P1Text; 
    [SerializeField] TextMeshProUGUI P2Text;

    [SerializeField] TextMeshProUGUI ControllerDebug1;
    [SerializeField] TextMeshProUGUI ControllerDebug2;

    [SerializeField] string[] Players = new string[2];
    public List<string> Controllers = new List<string>(2);

    [SerializeField] GameObject SelectionPanel;
    [SerializeField] TextMeshProUGUI SelectionText;

    public Button XboxButton;
    public Button PsButton;

    [SerializeField] GameObject PlayButton;


    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    [SerializeField] Menu menu;

    bool firstOpening;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GetController());

        if(Controllers[0] != "")
        {
            ControllerDebug1.text = Controllers[0] + Input.GetJoystickNames()[0];
        }
        else
        {
            ControllerDebug1.text = "No Controller";
        }

        if(Controllers[1] != "")
        {
            ControllerDebug2.text = Controllers[1] + Input.GetJoystickNames()[1];
        }
        else
        {
            ControllerDebug2.text = "No Controller";
        }
       

        if (Players[0] != "" && Players[1] != "")
        {
            PlayButton.SetActive(true);
        }
        else
        {
            PlayButton.SetActive(false);
        }

        #region Join
        //Join
        if(PlayerPrefs.GetString("Keyboard1" + "Jump") != "")
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard1" + "Jump"))))
            {
                GetPlayer("Keyboard1",-1);
            }
        }
        if (PlayerPrefs.GetString("Keyboard2" + "Jump") != "")
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Keyboard2" + "Jump"))))
            {
                GetPlayer("Keyboard2",-1);
            }
        }

        if (Input.GetAxis("XboxButtonA") > 0 && Controllers[0] == "Xbox")
        {
            GetPlayer("Xbox", PlayerPrefs.GetInt("PlayerIndex" + 0));
        }
        if (Input.GetAxis("Xbox2ButtonA") > 0 && Controllers[1] == "Xbox") 
        {
            GetPlayer("Xbox2", PlayerPrefs.GetInt("PlayerIndex" + 1));
        }
        if (Input.GetAxis("PsButtonX") > 0 && Controllers[0] == "Ps")
        {
            GetPlayer("Ps", PlayerPrefs.GetInt("PlayerIndex" + 0));
        }
        if (Input.GetAxis("Ps2ButtonX") > 0 && Controllers[1] == "Ps")
        {
            GetPlayer("Ps2", PlayerPrefs.GetInt("PlayerIndex" + 1));
        }

        #endregion

        #region Exit
        //Exit
        if (PlayerPrefs.GetString("Keyboard1" + "Cancel") != "")
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

        if (PlayerPrefs.GetString("Keyboard2" + "Cancel") != "")
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

    void GetPlayer(string ControllerName,int PlayerIndex)
    {
        if(Players[0] == "" && Players[1] != ControllerName)
        {
            Players[0] = ControllerName;
            P1Text.text = ControllerName;
            PlayerPrefs.SetString("P1", ControllerName);
            PlayerPrefs.SetInt("P1Index", PlayerIndex);
        }
        else if(Players[1] == "" && Players[0] != ControllerName)
        {
            Players[1] = ControllerName;
            P2Text.text = ControllerName;
            PlayerPrefs.SetString("P2", ControllerName);
            PlayerPrefs.SetInt("P2Index", PlayerIndex);
        }
    }

    IEnumerator GetController()
    {

        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if(Input.GetJoystickNames()[i] != "" && Controllers[i] == "")
            { 
                SelectionPanel.SetActive(true);
                if (!firstOpening)
                {
                    menu.SelectSelectionButton();
                    firstOpening = true;
                }
                
                SelectionText.text = Input.GetJoystickNames()[i];
                var waitForButton = new WaitForUIButtons(XboxButton,PsButton);
                yield return waitForButton.Reset();

                //kontrolcuyu sectirtmek yerine A veya X e bastýrtýp algýlayabiliriz.
                if (waitForButton.PressedButton == XboxButton)
                {
                    Controllers[i] = "Xbox";
                }
                else
                {
                    Controllers[i] = "Ps";
                }            

                Debug.Log(Input.GetJoystickNames()[i] +" "+ Controllers[i]);
                SelectionPanel.SetActive(false);
                firstOpening = false;

                menu.SelectFirstStartButton();
            }
            else if (Input.GetJoystickNames()[i] == "")
            {
                SelectionPanel.SetActive(false);
                Controllers[i] = "";
                PlayerPrefs.SetInt("PlayerIndex" + i, -1);
            }

            if (!playerIndexSet || !prevState.IsConnected)
            {
                for (int j = 0; j < 4; ++j)
                {
                    PlayerIndex testPlayerIndex = (PlayerIndex)j;
                    GamePadState testState = GamePad.GetState(testPlayerIndex);
                    if (testState.IsConnected)
                    {
                        Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                        playerIndex = testPlayerIndex;
                        playerIndexSet = true;

                        PlayerPrefs.SetInt("PlayerIndex" + i, j);
                    }
                }
            }

            prevState = state;
            state = GamePad.GetState(playerIndex);
        }      
    }
}
