using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrower : BallControllerComponent
{
	private readonly Vector2 DEFAULT_PULL_VALUE = new Vector2(0, 4);
	private const float BASE_UP_POWER = 5;

	[SerializeField] private TrajectoryRenderer trajectoryRenderer;
	private float _addedUpPower = 0;
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
		controller.respawner.OnBallRespawned += BallRespawned;
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
		ConstrainPullVector();

		_addedUpPower = _pullVector.y * 0.1f;
	}

	private void ConstrainPullVector()
	{
		float distAdd = _pullVector.y * 0.4f;
		const float sideMax = 2;
		if (_pullVector.magnitude < 4) _pullVector = _pullVector.normalized * 4;
		if (_pullVector.magnitude > 20) _pullVector = _pullVector.normalized * 20;
		if (_pullVector.y < 2) _pullVector.y = 2;
		if (_pullVector.x < -sideMax - distAdd) _pullVector.x = -sideMax - distAdd;
		if (_pullVector.x > sideMax + distAdd) _pullVector.x = sideMax + distAdd;
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

		ToggleInput(false);
	}

	private void ToggleInput(bool inputEnabled)
	{
		if (inputEnabled)
		{
			controller.controls.OnDrag += Drag;
			controller.controls.OnGrabbed += Grab;
			controller.controls.OnReleased += Release;
		}
		else
		{
			controller.controls.OnDrag -= Drag;
			controller.controls.OnGrabbed -= Grab;
			controller.controls.OnReleased -= Release;
		}
	}

	private void ThrowBall()
	{
		OnThrown?.Invoke();
		controller.currentBall.physics.Throw(new Vector3(_pullVector.x, BASE_UP_POWER + _addedUpPower, _pullVector.y));
	}

	private void BallRespawned(BallEntity entity)
	{
		ToggleInput(true);
	}

	private void UpdateTrajectory()
	{
		trajectoryRenderer.transform.position = controller.currentBall.transform.position;
		trajectoryRenderer.SetVelocity(new Vector3(_pullVector.x, BASE_UP_POWER +_addedUpPower, _pullVector.y));
	}
}
