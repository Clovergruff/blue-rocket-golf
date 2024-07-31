using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenSwitcher : MonoBehaviour, IPreInitialized
{
	public void PreInitialize()
	{
		GameStateManager.OnStateChanged += OnStateChanged;
	}

	private void OnStateChanged(GameStateManager.State state)
	{
		switch (state)
		{
			case GameStateManager.State.MainMenu:
				UIScreenManager.Open<MainMenuScreen>();
				break;
			case GameStateManager.State.Gameplay:
				UIScreenManager.Open<GameplayScreen>();
				break;
			case GameStateManager.State.LevelComplete:
				UIScreenManager.Open<LevelCompleteScreen>();
				break;
			case GameStateManager.State.LevelFailed:
				UIScreenManager.Open<LevelFailedScreen>();
				break;
		}
	}
}
