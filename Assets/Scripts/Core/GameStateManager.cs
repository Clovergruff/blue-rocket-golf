using System;
using System.Collections;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	public enum State
	{
		MainMenu,
		Gameplay,
		GameOver,
	}
	
	public static event Action<State> OnStateChanged;
	public static event Action OnGameOver;
	public static event Action OnLevelFailed;
	public static State CurrentState {get; private set;}

	private static GameStateManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		SetState(State.MainMenu);
	}

	public static void SetState(State newState)
	{
		CurrentState = newState;
		OnStateChanged?.Invoke(CurrentState);
	}

	public static void StartGame()
	{
		SetState(State.Gameplay);
	}

	public static void GameOver()
	{
		// UIScreenManager.CloseAll();
		OnGameOver?.Invoke();

		Instance.StartCoroutine(LevelCompleteSequence());
		IEnumerator LevelCompleteSequence()
		{
			yield return new WaitForSeconds(2);
			SetState(State.GameOver);
		}
	}

	public static void RestartGame()
	{
		UIScreenManager.CloseAll();

		Instance.StartCoroutine(GameRestartSequence());
		IEnumerator GameRestartSequence()
		{
			yield return new WaitForSeconds(1);
			SetState(State.MainMenu);
		}
	}
}