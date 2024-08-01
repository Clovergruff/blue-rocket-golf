using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
	, IPreInitialized
{
	public static int remainingTime;
	public static event Action<int> OnTimeUpdated;
	public static event Action<int> OnTimeIncremented;

	private Coroutine _timerCoroutine;

	public void PreInitialize()
	{
		GameStateManager.OnStateChanged += GameStateChanged;
	}

	private void BallEnteredHole(BallEntity ball, HoleEntity hole) => IncrementTime(GameplayConfig.Instance.ShotSuccessfulTime);
	private void BallDestroyed(BallEntity ball) => IncrementTime(-GameplayConfig.Instance.ShotFailedTime);

	private void IncrementTime(int amount)
	{
		OnTimeIncremented?.Invoke(amount);
		remainingTime += amount;
		OnTimeUpdated?.Invoke(remainingTime);
	}

	private void GameStateChanged(GameStateManager.State state)
	{
		StopTimer();

		if (state == GameStateManager.State.Gameplay)
		{
			SubscribeBallEvents();
			remainingTime = GameplayConfig.Instance.defaultTime;
			_timerCoroutine = StartCoroutine(TimerCoroutine());
		}
		else
		{
			UnsubscribeBallEvents();
		}
	}

	private void UnsubscribeBallEvents()
	{
		HoleBallDetection.OnBallEnteredInHole -= BallEnteredHole;
		BallHealth.OnBallDestroyed -= BallDestroyed;
	}

	private void SubscribeBallEvents()
	{
		UnsubscribeBallEvents();
		HoleBallDetection.OnBallEnteredInHole += BallEnteredHole;
		BallHealth.OnBallDestroyed += BallDestroyed;
	}

	private void StopTimer()
	{
		if (_timerCoroutine != null)
		{
			StopCoroutine(_timerCoroutine);
			_timerCoroutine = null;
		}
	}

	private IEnumerator TimerCoroutine()
	{
		while (true)
		{
			remainingTime--;
			OnTimeUpdated?.Invoke(remainingTime);

			if (remainingTime < 0)
			{
				GameStateManager.GameOver();
				StopTimer();
				break;
			}
				
			yield return new WaitForSeconds(1);
		}
	}
}
