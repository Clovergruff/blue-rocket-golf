using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHoleDetector : BallComponent
{
	public event Action<HoleEntity> OnHoleEntered;
	public static event Action<BallEntity, HoleEntity> OnBallEnteredHole;

	public void HoleEntered(HoleEntity hole)
	{
		gameObject.SetActive(false);
		OnHoleEntered?.Invoke(hole);
		OnBallEnteredHole?.Invoke(ball, hole);
	}
}
