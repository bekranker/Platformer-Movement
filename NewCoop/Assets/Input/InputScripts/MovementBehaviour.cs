using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementBehaviour : MonoBehaviour
{
    string MyName;
    PlayerInput playerInput;
    public float x, y, r, JumpDown, Jump, Attack, MeleeDown, AttackDown, Start, Cancel, CancelDown;
    Gamepad MyGamepad;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    // Update is called once per frame
    void Update()
    {
        InputAction  MovementAction = playerInput.actions["Movement"];
        Vector2 Movement = MovementAction.ReadValue<Vector2>();
        x = Movement.x;
        y = Movement.y;

        InputAction JumpAction = playerInput.actions["Jump"];
        Jump = JumpAction.ReadValue<float>();

        bool jumptrigger = JumpAction.triggered;
        JumpDown = jumptrigger ? 1 : 0;

        InputAction RotationAction = playerInput.actions["Rotate"];
        r = RotationAction.ReadValue<float>();

        InputAction AttackAction = playerInput.actions["Range"];
        Attack = AttackAction.ReadValue<float>();

        bool attacktrigger = AttackAction.triggered;
        AttackDown = attacktrigger ? 1 : 0;

        InputAction MeleeAction = playerInput.actions["Melee"];
        bool meleetrigger = MeleeAction.triggered;
        MeleeDown = meleetrigger ? 1 : 0;

        InputAction CancelAction = playerInput.actions["Dash"];
        Cancel = CancelAction.ReadValue<float>();

        bool canceltrigger = CancelAction.triggered;
        CancelDown = canceltrigger ? 1 : 0;

        InputAction SettingsAction = playerInput.actions["Start"];
        bool starttrigger = SettingsAction.triggered;
        Start = starttrigger ? 1 : 0;

        if (MyGamepad != null)
        {
            if(Attack == 1)
            {
                MyGamepad.SetMotorSpeeds(0.5f, 0.5f);
            }
            else
            {
                MyGamepad.SetMotorSpeeds(0, 0);
            }
        }
    }

    public void GetData(string Name,Gamepad gamepad)
    {
        MyName = Name;
        MyGamepad = gamepad;
    }
}
