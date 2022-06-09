using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class VibrarionDeneme : MonoBehaviour
{
    PlayerIndex playerIndex;
    bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        if (GetComponent<MovementBehaviour>().PlayerName == "P1")
        {
            playerIndex = (PlayerIndex)PlayerPrefs.GetInt("P1Index");

            if(PlayerPrefs.GetInt("P1Index") != -1)
            {
                isActive = true;
            }
        }
        else
        {
            playerIndex = (PlayerIndex)PlayerPrefs.GetInt("P2Index");

            if (PlayerPrefs.GetInt("P2Index") != -1)
            {
                isActive = true;
            }
        }        
    }
    private void FixedUpdate()
    {
        if (Input.GetButton("XboxButtonY") && isActive)
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
