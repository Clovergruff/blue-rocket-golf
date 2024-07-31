using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawner : BallComponent
{
	public event Action OnRespawned;

	public void Respawn()
	{
		OnRespawned?.Invoke();
	}
}
