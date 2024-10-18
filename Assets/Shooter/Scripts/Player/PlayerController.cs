using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
	#region Unity Fields
	[SerializeField] float moveSpeed;
	[SerializeField] float turnSpeed;
    [SerializeField] Joystick gameJoystick;
    #endregion
    #region Fields
    Vector3 _moveDirection;
    bool _canMove = true;
    #endregion
    #region Unity Methods
    protected override void Start()
    {
        base.Start();
        isPlayedDead = false;
        isControlEnabled = true;
        ButtonHold.onPressedFire += SetMove;
    }
    private void FixedUpdate()
    {
        if (!isControlEnabled || isPlayedDead)
        {
            if(rb.velocity.magnitude != 0)
                rb.velocity = Vector3.zero;
            return;
        }

        if (gameJoystick.Direction.x != 0 && gameJoystick.Direction.y != 0) 
        {
            animator.SetBool(CommonVariables.PlayerAnimBools.Run.ToString(), true);
        }
        else
        {
            animator.SetBool(CommonVariables.PlayerAnimBools.Run.ToString(), false);
        }

        transform.Rotate(0, gameJoystick.Direction.x * turnSpeed * Time.deltaTime, 0);
        Vector3 movement = new Vector3(gameJoystick.Direction.x, 0, gameJoystick.Direction.y);
        if (_canMove)
        {
            movement = transform.TransformDirection(movement) * moveSpeed;
            rb.AddForce(movement, ForceMode.Acceleration);
        }       
    }
    #endregion
    private void SetMove(bool isShooting) => _canMove = !isShooting;
}
