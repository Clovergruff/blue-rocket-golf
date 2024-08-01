using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHealth : BallComponent
{
	[SerializeField] private GibGroup gibPrefab;

	public event Action OnDeath;

	public static event Action<BallEntity> OnBallDestroyed;

	private Coroutine _naturalDeathCoroutine;

	public override void Init(BallEntity ball)
	{
		base.Init(ball);

		ball.physics.OnThrown += OnThrown;
	}

	private void OnThrown(Vector3 velocity)
	{
		_naturalDeathCoroutine = StartCoroutine(NaturalDeathCountdown());
	}

	public void TakeDamage()
	{
		var gibs = Instantiate(gibPrefab, transform.position, transform.rotation);
		OnDeath?.Invoke();
		OnBallDestroyed?.Invoke(ball);

		StopNaturalDeath();
		gameObject.SetActive(false);
	}

	private IEnumerator NaturalDeathCountdown()
	{
		// Die after 10 seconds. Just in case the ball gets stuck somewhere
		yield return new WaitForSeconds(10);
		TakeDamage();
	}

	private void StopNaturalDeath()
	{
		if (_naturalDeathCoroutine != null)
		{
			StopCoroutine(_naturalDeathCoroutine);
			_naturalDeathCoroutine = null;
		}
	}
}
