using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
	, IPreInitialized
{
	[SerializeField] private RectTransform parentRect;
	[SerializeField] private TMP_Text timerText;

	private RectTransform _rect;

	public void PreInitialize()
	{
		TimerManager.OnTimeUpdated += OnTimeUpdated;
	}

	private void Awake()
	{
		_rect = (RectTransform)transform;
	}

	private void Update()
	{
		// Because the ContentSizeFitter updates the rectTransform sizes unreliably (Say, a frame later),
		// we also also just smooth lerp the parent rect, so we get a nice size transition on every second
		parentRect.sizeDelta = Vector2.Lerp(parentRect.sizeDelta, _rect.sizeDelta, 15 * Time.deltaTime);
	}

	private void OnTimeUpdated(int seconds)
	{
		var secondsText = seconds > 0
			? $"{seconds}<size=20>s</size>"
			: "Time's Up!";
		timerText.SetText(secondsText);
	}
}
