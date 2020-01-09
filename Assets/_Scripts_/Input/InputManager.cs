#pragma warning disable CS0649
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class InputManager : MonoBehaviour, UserInput.IPlayerActions
{
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
    #region Variables
    public event Action<Vector2> movement;
    private UserInput userInput;
    #endregion

    private void Awake()
    {
        userInput = new UserInput();
        userInput.Player.SetCallbacks(this);
    }

    private void Update()
    {
        foreach (UnityEngine.Touch touch in UnityEngine.Input.touches)
        {
            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray))
                {
                    print("Touch at: " + touch.position);
                    Time.timeScale = 0.0f;
                }
            }
        }
    }


    private void OnEnable() => userInput.Player.Enable();
    private void OnDisable() => userInput.Player.Disable();
    public void OnMovement(InputAction.CallbackContext context) => movement?.Invoke(context.ReadValue<Vector2>());
}