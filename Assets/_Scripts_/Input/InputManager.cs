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
    private float screenWidth;
    #endregion

    private void Awake()
    {
        userInput = new UserInput();
        userInput.Player.SetCallbacks(this);
    }

    private void Start()
    {
        screenWidth = Screen.width;
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
                    if (touch.position.x > screenWidth / 2)
                    {
                        movement?.Invoke(new Vector2(1, 0));
                    }
                    else if (touch.position.x < screenWidth / 2)
                    {
                        movement?.Invoke(new Vector2(-1, 0));
                    }
                }
            }
            if (touch.phase == UnityEngine.TouchPhase.Ended)
            {
                movement?.Invoke(new Vector2(0, 0));
            }
        }
    }


    private void OnEnable() { if (Debug.isDebugBuild) userInput.Player.Enable(); }
    private void OnDisable() { if (Debug.isDebugBuild) userInput.Player.Disable(); }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (Debug.isDebugBuild) { movement?.Invoke(context.ReadValue<Vector2>()); }
    }

}