#pragma warning disable CS0649
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class InputManager : MonoBehaviour, UserInput.IPlayerActions
{
    #region Variables
    #region Singleton
    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();
            }
            return instance;
        }
    }
    #endregion
    public Action<Vector2> movement;
    private UserInput userInput;
    #endregion

    private void Awake()
    {
        userInput = new UserInput();
        userInput.Player.SetCallbacks(this);
    }

    private void Update()
    {
        /*foreach (Touch touch in Input.touches)
        {
            print(touch.position.ToString());
        }*/
    }

    private void OnEnable() => userInput.Player.Enable();
    private void OnDisable() => userInput.Player.Disable();
    public void OnMovement(InputAction.CallbackContext context) => movement?.Invoke(context.ReadValue<Vector2>());
}
