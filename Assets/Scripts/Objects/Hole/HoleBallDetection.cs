using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBallDetection : HoleComponent
{
	public event Action<BallEntity> OnBallEntered;

	public void BallEntered(BallEntity ball)
	{
		ball.gameObject.SetActive(false);
		ball.holeDetector.HoleEntered(hole);
		OnBallEntered?.Invoke(ball);
	}
}