using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : BallComponent
{
	private readonly Vector3 RESPAWN_VELOCITY = new Vector3(-10, 0, 0);
	[SerializeField] private HoleCollisionSetter holeCollisionSetter;

	public new Rigidbody rigidbody {get; private set;}

	public bool isThrown {get; private set;}
	public event Action OnGrabbed;
	public event Action<Vector3> OnThrown;

	public override void Init(BallEntity ball)
	{
		base.Init(ball);

		ball.respawner.OnRespawned += Respawned;
	}

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
		SetAnchored();
	}

	private void FixedUpdate()
	{
		holeCollisionSetter.UpdateCollisions();
	}

	public void SetAnchored()
	{
		isThrown = false;

		rigidbody.isKinematic = false;
		rigidbody.useGravity = false;
		rigidbody.drag = 8;
		rigidbody.angularDrag = 5;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}

	public void Grab()
	{
		isThrown = false;
		
		OnGrabbed?.Invoke();
	}

	public void Throw(Vector3 velocity)
	{
		isThrown = true;

		OnThrown?.Invoke(velocity);

		rigidbody.isKinematic = false;
		rigidbody.useGravity = true;
		rigidbody.velocity = velocity;
		rigidbody.drag = 0;
		rigidbody.angularDrag = 0;
		rigidbody.constraints = RigidbodyConstraints.None;

		rigidbody.angularVelocity = new Vector3(velocity.z * 5, 0, -velocity.x * 5);
	}

	private void Respawned()
	{
		SetAnchored();
		rigidbody.velocity = RESPAWN_VELOCITY;
	}
}
