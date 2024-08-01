using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HoleTypeSwitcher : HoleComponent
{
	public event Action<float> OnUpdateTime;
	public event Action OnTimerStarted;
	public event Action OnTimerEnded;
	public event Action OnHoleSwitched;

	private Coroutine _switchLoopCoroutine;

	public override void Init(HoleEntity hole)
	{
		base.Init(hole);

		GameStateManager.OnStateChanged += GameStateChanged;
	}

	private void GameStateChanged(GameStateManager.State state)
	{
		if (state == GameStateManager.State.Gameplay)
			StartSwitchLoop();
		else
			StopSwitchLoop();
	}

	private void StopSwitchLoop()
	{
		if (_switchLoopCoroutine != null)
		{
			StopCoroutine(_switchLoopCoroutine);
			_switchLoopCoroutine = null;
		}
	}

	private void StartSwitchLoop()
	{
		StopSwitchLoop();

		_switchLoopCoroutine = StartCoroutine(SwitchLoop());
		IEnumerator SwitchLoop()
		{
			var config = GameplayConfig.Instance;
			while (true)
			{
				yield return new WaitForSeconds(Random.Range(config.minHoleSwitchTime, config.maxHoleSwitchTime));

				SwitchHopeType();
				yield return new WaitForSeconds(config.holeSwitchTimerDuration);
			}
		}
	}

	private void SwitchHopeType()
	{
		OnTimerStarted?.Invoke();
		StartCoroutine(SwitchSequence());
		IEnumerator SwitchSequence()
		{
			float t = 0;
			float duration = GameplayConfig.Instance.holeSwitchTimerDuration;

			while (t < 1)
			{
				t += Time.deltaTime / duration;
				OnUpdateTime?.Invoke(t);
				yield return null;
			}

			OnTimerEnded?.Invoke();
			OnHoleSwitched?.Invoke();
		}
	}
}
