using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStateManager;

public class SplashScreen : UIScreen
{
	private Coroutine _introCoroutine;

	public override void Init()
	{
	}

	public override void OnCloseButton()
	{
		// No going back now.
	}

	protected override void OnOpened()
	{
		StopIntroCoroutine();
		_introCoroutine = StartCoroutine(OpenCoroutine());
	}

	private void StopIntroCoroutine()
	{
		if (_introCoroutine != null)
		{
			StopCoroutine(_introCoroutine);
			_introCoroutine = null;
		}
	}

	public void OnTap()
	{
		StopIntroCoroutine();
		SetState(State.MainMenu);
	}

	private IEnumerator OpenCoroutine()
	{
		yield return new WaitForSeconds(3);
		SetState(State.MainMenu);
	}
}
