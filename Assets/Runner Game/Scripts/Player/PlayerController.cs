using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
	#region Unity Fields
	[SerializeField] float forwardSpeed;
	[SerializeField] float sideSpeed;
    [SerializeField] Joystick gameJoystick;
    #endregion
    #region Fields
    Vector3 _moveDirection;
    #endregion
    #region Unity Methods
    protected override void Start()
    {
        base.Start();
        isPlayedDead = false;
    }
    private void FixedUpdate()
    {
        if (!isControlEnabled || isPlayedDead)
        {
            if(rb.velocity.magnitude != 0)
                rb.velocity = Vector3.zero;
            return;
        }

        _moveDirection.z = forwardSpeed * Time.fixedDeltaTime;
        _moveDirection.y = 0f;
        _moveDirection.x = gameJoystick.Direction.x * sideSpeed * Time.fixedDeltaTime;
        rb.AddForce(_moveDirection, ForceMode.VelocityChange);
    }
    #endregion
}
