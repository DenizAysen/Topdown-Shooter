using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBase : MonoBehaviour
{
	#region Properties
	public IState CurrentState {  get; private set; }
	public IState _previousState;
	bool _inTransition = false;
	#endregion
	#region Public Methods
	public void ChangeState(IState newState)
	{
		if(CurrentState == newState || _inTransition)
		{
			return;
		}
        ChangeStateRoutine(newState);
    }
    #endregion
    #region Private Methods
    private void ChangeStateRoutine(IState newState)
    {
        _inTransition = true;

        if (CurrentState != null)
            CurrentState.Exit();

        if (_previousState != null)
            _previousState = CurrentState;

        CurrentState = newState;

        CurrentState.Enter();

        _inTransition = false;
    }
    #endregion
    #region Unity Methods
    private void Update()
    {
        if(CurrentState != null && !_inTransition)
            CurrentState.Tick();
    }
    private void FixedUpdate()
    {
        if (CurrentState != null && !_inTransition)
            CurrentState.FixedTick();
    }
    #endregion
}
