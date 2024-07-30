using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCompleteScreen : UIScreen
{
	public override void Init()
	{
		GameStateManager.OnStateChanged += OnStateChanged;
	}

	private void OnStateChanged(GameStateManager.State state)
	{
		if (state == GameStateManager.State.LevelComplete)
		{
			
		}
	}
}
