using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;

public class GetPlayers : MonoBehaviour
{
    int PlayerCount;
    [SerializeField] List<PlayerInput> PlayerInputs;
    [SerializeField] List<TextMeshProUGUI> PlayerNameTexts;
    [SerializeField] List<TextMeshProUGUI> ControllerNameTexts;
    [SerializeField] List<TextMeshProUGUI> PlayerReadyTexts;
    [SerializeField] List<string> PlayerReady;

    [SerializeField] TextMeshProUGUI TimerText;
    float timer;

    private void Start()
    {
        for (int i = 1; i < 5; i++)
        {
            PlayerPrefs.SetString("P" + i, "");
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region UI
        for (int i = 0; i < 4; i++)
        {
            PlayerNameTexts[i].text = PlayerInputs[i] != null ? PlayerInputs[i].currentControlScheme : "";
            ControllerNameTexts[i].text = PlayerInputs[i] != null ? "("+PlayerInputs[i].devices[0].displayName+")" : "";
            PlayerReadyTexts[i].text = PlayerInputs[i] != null ? PlayerReady[i] : "";
        }

        TimerText.text = ((int)timer).ToString();

        if (ControlReady())
        {
            TimerText.gameObject.SetActive(true);
            timer -= Time.deltaTime;
        }
        else
        {
            TimerText.gameObject.SetActive(false);
            timer = 5.9f;
        }
        #endregion

        if(timer<= 0)
        {
            NextScene();
        }
    }

    public void ConnectPlayer(PlayerInput playerInput)
    {
        if (!PlayerInputs.Contains(playerInput))
        {
            if (PlayerCount < 5)
            {
                for (int i = 0; i < PlayerInputs.Count; i++)
                {
                    //Find Emty Player Slot
                    if (PlayerInputs[i] == null)
                    {
                        PlayerInputs[i] = playerInput;
                        PlayerPrefs.SetString("P" + (i + 1), playerInput.currentControlScheme);

                        //Gamepad link to Player for vibration
                        if (playerInput.currentControlScheme.Contains("Controller"))
                        {
                            Debug.Log("Kontrolcü" + playerInput.devices[0]);
                            InputSystem.SetDeviceUsage(playerInput.devices[0], "P" + (i + 1));
                        }
                        PlayerCount++;
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < PlayerInputs.Count; i++)
            {
                if (PlayerInputs[i] == playerInput)
                {
                    PlayerReady[i] = "Ready";
                }
            }
        }
    }

    public void DisconnectPlayer(PlayerInput playerInput)
    {
        if (PlayerInputs.Contains(playerInput))
        {
            for (int i = 0; i < PlayerInputs.Count; i++)
            {
                //Find the Player Slot
                if (PlayerInputs[i] == playerInput)
                {
                    if (PlayerReady[i] != "Ready")
                    {
                        PlayerPrefs.SetString("P" + (i + 1), "");
                        PlayerInputs[i] = null;

                        //Gamepad delink to Player for vibration
                        if (playerInput.currentControlScheme.Contains("Controller"))
                        {
                            InputSystem.SetDeviceUsage(playerInput.devices[0], "");
                        }
                        PlayerCount--;
                        break;
                    }
                    else
                    {
                        PlayerReady[i] = "Not Ready";
                    }
                }
            }
        }
    }

    bool ControlReady()
    {
        bool isReady = true;
        for (int i = 0; i < 4; i++)
        {
            if ((PlayerInputs[i] != null && PlayerReady[i] != "Ready") || PlayerCount < 2)
            {
                isReady = false;
            }
        }
        return isReady;
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
