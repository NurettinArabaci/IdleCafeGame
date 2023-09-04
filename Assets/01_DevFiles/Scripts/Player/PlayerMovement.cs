using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Cachable<PlayerMovement>
{
    private Rigidbody _rb;
    public Rigidbody Rigidbody => _rb ??= GetComponent<Rigidbody>();


    [SerializeField] private JoystickController joystick;
    [SerializeField] private float speed = 1;


    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState != GameState.Play) return;

        Rigidbody.velocity = DirectionPose() * speed;
        transform.rotation = RotationPose();
        playerController._AnimationController.SetFloatAnim("MoveParam", Rigidbody.velocity.magnitude / speed);
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
