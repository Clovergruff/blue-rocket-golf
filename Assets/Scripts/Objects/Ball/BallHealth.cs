using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHealth : BallComponent
{
	[SerializeField] private GibGroup gibPrefab;

	public event Action OnDeath;
	public static event Action<BallEntity> OnBallDestroyed;

	public void TakeDamage()
	{
		var gibs = Instantiate(gibPrefab, transform.position, transform.rotation);
		OnDeath?.Invoke();
		OnBallDestroyed?.Invoke(ball);

		gameObject.SetActive(false);
	}
}
