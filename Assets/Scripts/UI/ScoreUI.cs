using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
	[SerializeField] private RectTransform parentRect;

	[Space]
	[SerializeField] private TMP_Text text;
	[SerializeField] private string prefix;
	[SerializeField] private string suffix;

	private RectTransform _rect;
	private float _parentWidth;

	private void Awake()
	{
		_rect = (RectTransform)transform;
	}

	private void OnEnable()
	{
		ScoreManager.OnScoreUpdated -= ScoreUpdated;
		ScoreManager.OnScoreUpdated += ScoreUpdated;
		ScoreUpdated(ScoreManager.currentScore);
	}

	private void OnDisable()
	{
		ScoreManager.OnScoreUpdated -= ScoreUpdated;
	}

	private void Update()
	{
		// Similar issue as with TimerUI, except here we also move the parent rect to a zero position.
		// This will let us anchor the score ui anyhow we like
		_parentWidth = Mathf.Lerp(_parentWidth, _rect.sizeDelta.x, 15 * Time.deltaTime);
		parentRect.sizeDelta = new Vector2(_parentWidth, parentRect.sizeDelta.y);//Vector2.Lerp(parentRect.sizeDelta, _rect.sizeDelta, 15 * Time.deltaTime);
		parentRect.anchoredPosition = Vector2.Lerp(parentRect.anchoredPosition, Vector2.zero, 15 * Time.deltaTime);
	}

	private void ScoreUpdated(int amount)
	{
		text.SetText($"{prefix}{amount}{suffix}");
	}
}