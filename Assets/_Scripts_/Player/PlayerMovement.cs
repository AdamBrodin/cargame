#pragma warning disable CS0649
using UnityEngine;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    #region Variables
    private Rigidbody rgbd => GetComponent<Rigidbody>();
    private Vector2 movement;
    [SerializeField] private float moveSpeed, tiltAmount, tiltSpeed;
    #endregion

    private void Start() => InputManager.Instance.movement += GetPlayerInput;
    private void GetPlayerInput(Vector2 input) => movement = input;
    private void FixedUpdate() => Move();
    private void Move()
    {
        rgbd.velocity = new Vector3((-movement.x * moveSpeed), 0, 0);
        Quaternion startRot = rgbd.rotation;
        Quaternion endRot = Quaternion.Euler(0, movement.x * tiltAmount, 0);
        rgbd.rotation = Quaternion.Slerp(startRot, endRot, tiltSpeed);
    }
}
