using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : UIScreen
{
	[SerializeField] private Button continueButton;

	public override void Init()
	{
		continueButton.onClick.AddListener(ContinueButtonPressed);
	}

	private void ContinueButtonPressed()
	{
		canvasGroup.blocksRaycasts = false;
		GameStateManager.RestartGame();
	}
}
