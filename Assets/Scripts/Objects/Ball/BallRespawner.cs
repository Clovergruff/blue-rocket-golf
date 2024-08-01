using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawner : BallComponent
{
	public event Action OnRespawned;
	public static event Action<BallEntity> OnBallRespawned;

	public void Respawn()
	{
		OnRespawned?.Invoke();
		OnBallRespawned?.Invoke(ball);
	}
}
