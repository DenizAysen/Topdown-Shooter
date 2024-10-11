using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter 
{
	#region Properties
	public float TotalTime { get; private set; }
	private float _timeLeft;
    #endregion
    #region Constructor
    public TimeCounter(float totalTime)
    {
        TotalTime = totalTime;
        _timeLeft = totalTime;
    }
    #endregion
    #region Public Methods
    public bool IsThickFinished(float deltaTime)
    {
        if (_timeLeft > 0) 
        {
            _timeLeft -= deltaTime;
            return false;
        }
        else
        {
            _timeLeft = TotalTime;
        }
        return true;
    }
    public float GetTimeLeft() => _timeLeft;
    #endregion
}
