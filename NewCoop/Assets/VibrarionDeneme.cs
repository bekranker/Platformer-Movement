using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class VibrarionDeneme : MonoBehaviour
{
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    Controls controls;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<MovementBehaviour>().Player == "P1")
        {
            playerIndex = (PlayerIndex)PlayerPrefs.GetInt("P1Index");
        }
        else
        {
            playerIndex = (PlayerIndex)PlayerPrefs.GetInt("P2Index");
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
    }
    private void FixedUpdate()
    {
        if (Input.GetButton("XboxButtonY"))
        {
            GamePad.SetVibration(playerIndex, .5f, .5f);
            Debug.Log("Vib");
        }
        else
        {
            GamePad.SetVibration(playerIndex, 0, 0);
        }
    }
}
