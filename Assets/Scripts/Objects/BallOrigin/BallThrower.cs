using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrower : BallControllerComponent
{
	private readonly Vector2 DEFAULT_PULL_VALUE = new Vector2(0, 4);

	[SerializeField] private TrajectoryRenderer trajectoryRenderer;
	[SerializeField] private float upPower = 5;
	private Vector2 _pullVector = Vector2.zero;
	private bool _isPulling;

	public event Action OnGrabbed;
	public event Action OnThrown;

	public override void Init(BallController controller)
	{
		base.Init(controller);

		controller.controls.OnDrag += Drag;
		controller.controls.OnGrabbed += Grab;
		controller.controls.OnReleased += Release;
	}

	private void LateUpdate()
	{
		if (_isPulling)
		{
			UpdateTrajectory();
		}
	}

	private void FixedUpdate()
	{
		if (_isPulling)
		{
			controller.currentBall.physics.rigidbody.velocity -= new Vector3(_pullVector.x * 4, _pullVector.y * 2, _pullVector.y * 4) * Time.fixedDeltaTime;
		}
	}

	private void Drag(Vector2 delta)
	{
		_pullVector.x += delta.x * 5 * Time.deltaTime;
		_pullVector.y -= delta.y * 5 * Time.deltaTime;
	}

	private void Grab()
	{
		if (_isPulling)
			return;

		OnGrabbed?.Invoke();

		_isPulling = true;
		_pullVector = DEFAULT_PULL_VALUE;
		trajectoryRenderer.gameObject.SetActive(true);
		controller.currentBall.graphics.SetGrabbed();
		UpdateTrajectory();
	}

	private void Release()
	{
		if (!_isPulling)
			return;

		_isPulling = false;
		trajectoryRenderer.gameObject.SetActive(false);
		controller.currentBall.graphics.SetReleased();
		ThrowBall();
		_pullVector = DEFAULT_PULL_VALUE;
	}

	private void ThrowBall()
	{
		OnThrown?.Invoke();
		controller.currentBall.physics.Throw(new Vector3(_pullVector.x, upPower, _pullVector.y));
	}

	private void UpdateTrajectory()
	{
		trajectoryRenderer.transform.position = controller.currentBall.transform.position;
		trajectoryRenderer.SetVelocity(new Vector3(_pullVector.x, upPower, _pullVector.y));
	}
}
