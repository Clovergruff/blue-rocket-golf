using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trigger))]
public class HolePitTrigger : MonoBehaviour
{
	[SerializeField] private HoleEntity hole;

	private void Awake()
	{
		if (TryGetComponent<Trigger>(out var trigger))
			trigger.OnBallEntered += ball => hole.ballDetector.BallEntered(ball);
	}
}
