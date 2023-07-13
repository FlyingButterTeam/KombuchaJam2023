using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour 
{
    private StateBase _state;
    public StateBase State
    {
        get { return _state; }

        set
        {
            if (_state == value)
                return;

            if(_state != null)
                _state.EndState();

            _state = value;
            _state.InitiateState();
        }
    }
}
