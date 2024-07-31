using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
	, IPreInitialized
{
	[SerializeField] private TMP_Text timerText;

	public void PreInitialize()
	{
		TimerManager.OnTimeUpdated += OnTimeUpdated;
	}

	private void OnTimeUpdated(int seconds)
	{
		timerText.SetText(seconds.ToString("F0"));
	}
}
