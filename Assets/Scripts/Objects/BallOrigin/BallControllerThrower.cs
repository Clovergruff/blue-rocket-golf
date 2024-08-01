using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerThrower : BallControllerComponent
{
	private readonly Vector2 DEFAULT_PULL_VALUE = new Vector2(0, 4);
	private const float BASE_UP_POWER = 5;

	[SerializeField] private TrajectoryRenderer trajectoryRenderer;
	private float _addedUpPower = 0;
	private Vector2 _pullVector = Vector2.zero;
	private bool _isPulling;
	private Vector3 _trajectoryVelocity;

	public event Action OnGrabbed;
	public event Action OnThrown;

	public override void Init(BallController controller)
	{
		base.Init(controller);

		controller.controls.OnDrag += Drag;
		controller.controls.OnGrabbed += Grab;
		controller.controls.OnReleased += Release;
		controller.respawner.OnBallRespawned += BallRespawned;

		GameStateManager.OnGameOver += CancelPulling;

		trajectoryRenderer.gameObject.SetActive(false);
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
			var vel = GetPullVelocity();
			var rb = controller.currentBall.physics.rigidbody;
			controller.currentBall.physics.rigidbody.velocity -= vel * 8 * Time.fixedDeltaTime;
			rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(vel.normalized), 20 * Time.deltaTime);
		}
	}

	private void CancelPulling()
	{
		trajectoryRenderer.gameObject.SetActive(false);
		_pullVector = Vector2.zero;
		_isPulling = false;
	}

	private void Drag(Vector2 delta)
	{
		_pullVector.x -= delta.x * 5 * Time.deltaTime;
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
		trajectoryRenderer.gameObject.SetActive(true);
		controller.currentBall.graphics.SetGrabbed();
		controller.currentBall.physics.Grab();

		_pullVector = DEFAULT_PULL_VALUE;
		_trajectoryVelocity = GetPullVelocity();
		UpdateTrajectory();
	}

	private void Release()
	{
		if (!_isPulling)
			return;

		OnThrown?.Invoke();

		_isPulling = false;
		trajectoryRenderer.gameObject.SetActive(false);
		controller.currentBall.graphics.SetReleased();
		controller.currentBall.physics.Throw(GetPullVelocity());

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

	private void BallRespawned(BallEntity entity) => ToggleInput(true);

	private Vector3 GetPullVelocity() => new Vector3(_pullVector.x, BASE_UP_POWER + _addedUpPower, _pullVector.y); //new Vector3(_pullVector.x * 4, _pullVector.y * 2, _pullVector.y * 4);

	private void UpdateTrajectory()
	{
		_trajectoryVelocity = Vector3.Lerp(_trajectoryVelocity, GetPullVelocity(), 25 * Time.deltaTime);

		trajectoryRenderer.transform.position = controller.currentBall.transform.position;
		trajectoryRenderer.SetVelocity(_trajectoryVelocity);
	}
}
