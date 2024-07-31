using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGraphics : BallComponent
{
	[SerializeField] private MeshRenderer grabOutlineRenderer;
	[SerializeField] private Transform pivot;
	[SerializeField] private Transform modelTransform;

	[Space]
	[SerializeField] private ParticleSystem throwParticles;
	[SerializeField] private ParticleSystem respawnParticles;

	private float delayedVelocityT = 0;

	public override void Init(BallEntity ball)
	{
		base.Init(ball);

		grabOutlineRenderer.enabled = false;

		ball.physics.OnThrown += Thrown;
		ball.respawner.OnRespawned += OnRespawned;
	}

	private void Thrown(Vector3 velocity)
	{
		throwParticles.Play();
	}

	private void OnRespawned()
	{
		respawnParticles.Play();
	}

	private void Update()
	{
		float velocityMagnitude = ball.physics.rigidbody.velocity.magnitude;
		float velocityT = Mathf.InverseLerp(0, 7, velocityMagnitude);
		delayedVelocityT = Mathf.Lerp(delayedVelocityT, velocityT, 2 * Time.deltaTime);

		pivot.forward = ball.physics.rigidbody.velocity.normalized;
		pivot.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.6f, 0.6f, 1.4f), velocityT - delayedVelocityT);

		modelTransform.rotation = transform.rotation;

		respawnParticles.transform.rotation = Quaternion.identity;
	}

	public void SetGrabbed()
	{
		grabOutlineRenderer.enabled = true;
	}

	public void SetReleased()
	{
		grabOutlineRenderer.enabled = false;
	}
}
