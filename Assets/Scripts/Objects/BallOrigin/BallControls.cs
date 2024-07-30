using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallControls : BallControllerComponent
{
	public event Action OnGrabbed;
	public event Action OnReleased;
	public event Action<Vector2> OnDrag;

	public override void Init(BallController owner)
	{
		base.Init(owner);
		
		InputSystem.actions["Attack"].performed += context => OnGrabbed?.Invoke();
		InputSystem.actions["Attack"].canceled += context => OnReleased?.Invoke();
		InputSystem.actions["Look"].performed += context => OnDrag?.Invoke(context.ReadValue<Vector2>());
	}
}
