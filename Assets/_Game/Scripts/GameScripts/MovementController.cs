using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    
    
    
    
    public abstract class MovementControls : MonoBehaviour
    {
        public event System.Action Dash;
        public event System.Action Move;
        public Vector2 MoveDirection;

        public abstract void Initialize();

        protected virtual void OnMove()
        {
            Move?.Invoke();
        }

        protected virtual void OnJump()
        {
            Dash?.Invoke();
        }
    }
}
