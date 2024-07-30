using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawner : BallControllerComponent
{
	private readonly Vector3 RESPAWN_OFFSET = new Vector3(2, -3, 0);
	private readonly Vector3 RESPAWN_VELOCITY = new Vector3(-10, 0, 0);

	[SerializeField] private SpringJoint springJoint;

	public event Action<BallEntity> OnBallRespawned;

	public override void Init(BallController owner)
	{
		base.Init(owner);

		owner.thrower.OnThrown += OnThrown;

		BallHealth.OnBallDestroyed += RespawnBall;
	}
	private void OnThrown()
	{
		springJoint.connectedBody = null;
		springJoint.gameObject.SetActive(false);
	}

	private void RespawnBall(BallEntity ball)
	{
		StartCoroutine(RespawnSequence());

		IEnumerator RespawnSequence()
		{
			yield return new WaitForSeconds(0.5f);

			springJoint.connectedBody = ball.physics.rigidbody;
			ball.transform.position = transform.position + RESPAWN_OFFSET;
			ball.gameObject.SetActive(true);
			yield return null;
			springJoint.gameObject.SetActive(true);

			controller.currentBall.physics.SetAnchored();
			ball.physics.rigidbody.velocity = RESPAWN_VELOCITY;

			OnBallRespawned?.Invoke(ball);
		}
	}
}