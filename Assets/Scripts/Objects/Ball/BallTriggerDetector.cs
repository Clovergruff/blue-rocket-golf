using UnityEngine;

public class BallTriggerDetector : BallComponent
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Trigger>(out var trigger))
			trigger.BallEntered(ball);
	}
}