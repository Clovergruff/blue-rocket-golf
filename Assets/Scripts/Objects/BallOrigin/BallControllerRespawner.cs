using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerRespawner : BallControllerComponent
{
	private readonly Vector3 RESPAWN_OFFSET = new Vector3(2, -3, 0);

	[SerializeField] private SpringJoint springJoint;

	public event Action<BallEntity> OnBallRespawned;

	public override void Init(BallController controller)
	{
		base.Init(controller);

		controller.thrower.OnThrown += OnThrown;

		StartCoroutine(DelayedInit());
		IEnumerator DelayedInit()
		{
			// Skin a frame to ensure that these events are subscribed after the ball has been set up fully.
			// This is super hacky, and would be avoided by having a better architecture, but oh well.
			yield return null;
			controller.currentBall.health.OnDeath += RespawnBall;
			controller.currentBall.holeDetector.OnHoleEntered += x => RespawnBall();
		}
	}

	private void OnThrown()
	{
		springJoint.connectedBody = null;
		springJoint.gameObject.SetActive(false);
	}

	private void RespawnBall()
	{
		var ball = controller.currentBall;
		StartCoroutine(RespawnSequence());

		IEnumerator RespawnSequence()
		{
			yield return new WaitForSeconds(0.5f);

			springJoint.connectedBody = ball.physics.rigidbody;
			ball.transform.position = transform.position + RESPAWN_OFFSET;
			ball.gameObject.SetActive(true);
			yield return null;
			springJoint.gameObject.SetActive(true);

			ball.respawner.Respawn();
			OnBallRespawned?.Invoke(ball);
		}
	}
}