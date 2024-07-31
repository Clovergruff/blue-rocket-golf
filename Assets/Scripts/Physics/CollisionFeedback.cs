using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionFeedback : MonoBehaviour
{
	[SerializeField] private ParticleSystem collisionParticles;

	private void OnCollisionEnter(Collision collision)
	{
		var contact = collision.contacts[0];
		if (contact.impulse.magnitude > 3)
		{
			collisionParticles.transform.rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
			collisionParticles.transform.position = contact.point;
			collisionParticles.Play();
			// var particleSystem = Instantiate(collisionParticles, contact.point, Quaternion.identity);
			// var coll = particleSystem.collision;
		}
	}
}
