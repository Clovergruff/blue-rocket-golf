using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

[RequireComponent(typeof(Trigger))]
public class KillTrigger : MonoBehaviour
{
	private void Awake()
	{
		if (TryGetComponent<Trigger>(out var trigger))
			trigger.OnBallEntered += ball => ball.health.TakeDamage();
	}
}
