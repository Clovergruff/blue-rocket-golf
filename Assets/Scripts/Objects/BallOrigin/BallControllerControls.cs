using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallControllerControls : BallControllerComponent
{
	public event Action OnGrabbed;
	public event Action OnReleased;
	public event Action<Vector2> OnDrag;

	public override void Init(BallController owner)
	{
		base.Init(owner);

		GameStateManager.OnStateChanged += GameStateChanged;		
		GameStateManager.OnGameOver += UnsubscribeControls;		
	}

	private void GameStateChanged(GameStateManager.State state)
	{
		if (state == GameStateManager.State.Gameplay)
		{
			SubscribeControls();
			OnGrabbed?.Invoke();
		}
		else
		{
			UnsubscribeControls();
		}
	}

	private void SubscribeControls()
	{
		UnsubscribeControls();
		InputSystem.actions["Attack"].performed += Grab;
		InputSystem.actions["Attack"].canceled += Release;
		InputSystem.actions["Look"].performed += Drag;
	}

	private void UnsubscribeControls()
	{
		InputSystem.actions["Attack"].performed -= Grab;
		InputSystem.actions["Attack"].canceled -= Release;
		InputSystem.actions["Look"].performed -= Drag;
	}

	private void Drag(InputAction.CallbackContext context) => OnDrag?.Invoke(context.ReadValue<Vector2>());
	private void Release(InputAction.CallbackContext context) => OnReleased?.Invoke();
	private void Grab(InputAction.CallbackContext context) => OnGrabbed?.Invoke();
}
