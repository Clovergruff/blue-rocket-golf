using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
	, IPreInitialized
{
	public static int currentScore = 0;
	public static int bestScore = 0;

	public static event Action<int> OnScoreUpdated;

	public void PreInitialize()
	{
		GameStateManager.OnStateChanged += GameStateChanged;
		BallHoleDetector.OnBallEnteredHole += BallEnteredHole;
	}

	private void BallEnteredHole(BallEntity ball, HoleEntity hole)
	{
		AddScore(1);
	}

	private void GameStateChanged(GameStateManager.State state)
	{
		if (state == GameStateManager.State.Gameplay)
		{
			currentScore = 0;
		}
	}

	private void AddScore(int amount)
	{
		currentScore += amount;
		OnScoreUpdated?.Invoke(currentScore);
	}
}
