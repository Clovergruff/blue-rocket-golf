using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : UIScreen
{
	public override void Init()
	{
	}

	public override void OnCloseButton()
	{
	}

	public void OnStartGameButton()
	{
		GameStateManager.StartGame();
	}
}
