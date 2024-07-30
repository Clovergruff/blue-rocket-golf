using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gib : MonoBehaviour
{
	[SerializeField] private new MeshRenderer renderer;
	[SerializeField] private new Rigidbody rigidbody;
	[SerializeField] private new Collider collider;
	[SerializeField] private HoleCollisionSetter holeCollisionSetter;

	private void Reset()
	{
		renderer = GetComponent<MeshRenderer>();
		collider = GetComponent<Collider>();
		rigidbody = GetComponent<Rigidbody>();
		holeCollisionSetter = GetComponent<HoleCollisionSetter>();
	}

	public void UpdateGib()
	{
		holeCollisionSetter.UpdateCollisions();
	}

	public void SetVelocity(Vector3 velocity, Vector3 angularVelocity)
	{
		rigidbody.velocity = velocity;
		rigidbody.angularVelocity = angularVelocity;
	}
}
