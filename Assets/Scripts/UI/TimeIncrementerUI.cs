using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeIncrementerUI : MonoBehaviour
	, IPreInitialized
{
	public enum IncrementSpace
	{
		Positive,
		Negative
	}
	
	[SerializeField] private IncrementSpace space;

	[Space]
	[SerializeField] private Animator animator;
	[SerializeField] private Canvas canvas;
	[SerializeField] private TMP_Text text;

	private Coroutine _animationCoroutine;

	public void PreInitialize()
	{
		animator.enabled = false;
		canvas.enabled = false;

		// TimerManager.OnTimeIncremented += TimeIncremented;
		GameStateManager.OnStateChanged += GameStateChanged;
	}

	private void GameStateChanged(GameStateManager.State state)
	{
		TimerManager.OnTimeIncremented -= TimeIncremented;
		if (state == GameStateManager.State.Gameplay)
			TimerManager.OnTimeIncremented += TimeIncremented;
	}

	private void TimeIncremented(int amount)
	{
		if (space == IncrementSpace.Positive && amount > 0)
		{
			text.SetText($"+{amount}<size=20>s</size>");
			PlayAnimation();
		}
		else if (space == IncrementSpace.Negative && amount < 0)
		{
			text.SetText($"-{-amount}<size=20>s</size>");
			PlayAnimation();
		}
	}

	private void PlayAnimation()
	{
		if (_animationCoroutine != null)
		{
			StopCoroutine(_animationCoroutine);
			_animationCoroutine = null;
		}

		_animationCoroutine = StartCoroutine(AnimationSequence());
		IEnumerator AnimationSequence()
		{
			animator.enabled = true;
			canvas.enabled = true;
			animator.SetTrigger(AnimHash.OPEN);

			yield return new WaitForSeconds(1);

			animator.enabled = false;
			canvas.enabled = false;
		}
	}
}
