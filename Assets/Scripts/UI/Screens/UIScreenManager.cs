using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : MonoBehaviour
	, IPreInitialized
{
	public static UIScreenManager Instance { get; private set; }

	public Curves curves;

	private UIScreen[] allScreens;

	public void PreInitialize()
	{
		Instance = this;

		allScreens = GetComponentsInChildren<UIScreen>(true);

		foreach (var screen in allScreens)
			screen.Init();

		foreach (var screen in allScreens)
			screen.gameObject.SetActive(false);
	}

	public static void Open(UIScreen specificScreen)
	{
		specificScreen.Open();

		if (!specificScreen.isModal)
		{
			foreach (var screen in Instance.allScreens)
			{
				if (screen != specificScreen)
					screen.Close();
			}
		}
	}

	public static T Open<T>() where T : UIScreen
	{
		foreach (var screen in Instance.allScreens)
		{
			if (screen is T tScreen)
			{
				Open(tScreen);
				return tScreen;
			}
		}

		return null;
	}

	public static T Get<T>() where T : UIScreen
	{
		foreach (var screen in Instance.allScreens)
		{
			if (screen is T)
				return (T)screen;
		}

		return null;
	}

	public static void CloseAll()
	{
		foreach (var screen in Instance.allScreens)
			screen.Close();
	}

	[Serializable]
	public class Curves
	{
		public AnimationCurve fadeIn;
		public AnimationCurve fadeOut;

		[Space]
		public AnimationCurve scaleIn;
		public AnimationCurve scaleOut;
	}
}