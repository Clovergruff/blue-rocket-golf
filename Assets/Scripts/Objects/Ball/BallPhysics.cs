using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : BallComponent
{
	[SerializeField] private HoleCollisionSetter holeCollisionSetter;

	public new Rigidbody rigidbody {get; private set;}

	public bool isThrown {get; private set;}

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

	public void Throw(Vector3 velocity)
	{
		isThrown = true;

		rigidbody.isKinematic = false;
		rigidbody.useGravity = true;
		rigidbody.velocity = velocity;
		rigidbody.drag = 0;
		rigidbody.angularDrag = 0;
		rigidbody.constraints = RigidbodyConstraints.None;
	}
}
