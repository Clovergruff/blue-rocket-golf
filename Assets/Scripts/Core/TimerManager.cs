using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
	, IPreInitialized
{
	public static int remainingTime;
	public static event Action<int> OnTimeUpdated;

	private Coroutine _timerCoroutine;

	public void PreInitialize()
	{
		GameStateManager.OnStateChanged += OnStateChanged;
	}

	private void OnStateChanged(GameStateManager.State state)
	{
		StopTimer();

		if (state == GameStateManager.State.Gameplay)
		{
			remainingTime = GameplayConfig.Instance.defaultTime;
			_timerCoroutine = StartCoroutine(TimerCoroutine());
		}
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
				GameStateManager.EndGame();
				
			yield return new WaitForSeconds(1);
		}
	}
}
