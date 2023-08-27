using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    public Rigidbody Rigidbody => _rb ??= GetComponent<Rigidbody>();


    [SerializeField] private JoystickController joystick;
    [SerializeField] private float speed = 1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState != GameState.Play) return;

        Rigidbody.velocity = DirectionPose() * speed;
        transform.rotation = RotationPose();
        //PlayerController.SetFloat("MoveParam", Rigidbody.velocity.magnitude / speed);
        CollectableEvents.Fire_OnMovementLerp();
    }


    Vector3 DirectionPose()
    {

        return Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal; ;
    }

    Quaternion RotationPose()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            return Quaternion.LookRotation(Rigidbody.velocity);

        return transform.rotation;
    }
}
