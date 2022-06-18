using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMenuScript : MonoBehaviour
{
    PlayerInput playerInput;
    GetPlayers GetPlayersScript;
    [SerializeField] string Map;
    [SerializeField] GameObject PlayPanel;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        GetPlayersScript = GameObject.Find("GetPlayers").GetComponent<GetPlayers>();
    }

    // Update is called once per frame
    void Update()
    {
        InputAction StartAction = playerInput.actions["Range"];
        int Start = StartAction.triggered ? 1 : 0;

        if (Start == 1)
        {
            Debug.Log("start");
            GetPlayersScript.ConnectPlayer(playerInput);
        }

        InputAction CancelAction = playerInput.actions["Dash"];
        int Cancel = CancelAction.triggered ? 1 : 0;

        if (Cancel == 1)
        {
            Debug.Log("Cancel");
            GetPlayersScript.DisconnectPlayer(playerInput);
        }

        Map = GetComponent<PlayerInput>().currentActionMap.name;

        if (playerInput.devices.Count == 0)
        {
            Destroy(gameObject);
        }

        if (PlayPanel != null && playerInput != null && PlayPanel.activeSelf && playerInput.currentActionMap.name == "UI")
        {
            ChangeCurrentActionMap("Player");
        }
    }

    public void GetPlayPanel(GameObject panel)
    {
        PlayPanel = panel;
    }

    public void ChangeCurrentActionMap(string ActionMap)
    {
        playerInput.SwitchCurrentActionMap(ActionMap);
    }
}
