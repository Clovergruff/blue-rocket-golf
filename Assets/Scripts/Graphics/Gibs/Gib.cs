using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gib : MonoBehaviour
{
	private readonly Vector3 SAW_RAY_OFFSET = new Vector3(0, 0.1f, 0);

	[SerializeField] private new MeshRenderer renderer;
	[SerializeField] private new Rigidbody rigidbody;
	[SerializeField] private new Collider collider;
	[SerializeField] private HoleCollisionSetter holeCollisionSetter;

	private LayerMask _sawLayer;

	private void Awake()
	{
		_sawLayer = LayerMask.GetMask("Default");
	}

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

		// This is a bit hacky, but gives nice results.
		if (Physics.Raycast(transform.position + SAW_RAY_OFFSET, Vector3.down, out var hit, 0.3f, _sawLayer))
		{
			if (hit.collider.TryGetComponent<SawCollider>(out _))
				rigidbody.velocity += new Vector3(Random.Range(-2, 2), Random.Range(2, 4), Random.Range(-2, 2));
		}
	}

	public void SetVelocity(Vector3 velocity, Vector3 angularVelocity)
	{
		rigidbody.velocity = velocity;
		rigidbody.angularVelocity = angularVelocity;
	}

	public void DisablePhysics()
	{
		rigidbody.isKinematic = true;
		rigidbody.useGravity = false;
		collider.enabled = false;
	}
}
