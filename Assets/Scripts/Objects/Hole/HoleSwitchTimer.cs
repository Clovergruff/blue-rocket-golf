using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoleSwitchTimer : MonoBehaviour
	,IPreInitialized
{
	[SerializeField] private GameObject contents;
	[SerializeField] private HoleTypeSwitcher typeSwitcher;
	[SerializeField] private Image timerBar;
	[SerializeField] private CanvasGroup canvasGroup;

	public void PreInitialize()
	{
		typeSwitcher.OnUpdateTime += TimeUpdated;
		typeSwitcher.OnTimerStarted += StartTimer;
		typeSwitcher.OnTimerEnded += EndTimer;

		contents.SetActive(false);
	}

	private void TimeUpdated(float amount)
	{
		timerBar.fillAmount = amount;
	}

	private void EndTimer() => contents.SetActive(false);
	private void StartTimer() => contents.SetActive(true);
}