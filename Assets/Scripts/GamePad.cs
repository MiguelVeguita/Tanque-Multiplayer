using UnityEngine;
using UnityEngine.InputSystem;

public class GamePad : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.aButton.wasPressedThisFrame)
            {
                Gamepad.current.SetMotorSpeeds(0.5f, 0.5f);
            }

            if (Gamepad.current.aButton.wasReleasedThisFrame)
            {
                Gamepad.current.SetMotorSpeeds(0f, 0f);
            }
        }
    }
}