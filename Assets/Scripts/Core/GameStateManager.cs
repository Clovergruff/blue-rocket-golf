using System;
using System.Collections;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	public enum State
	{
		MainMenu,
		Gameplay,
		LevelComplete,
		LevelFailed,
	}
	
	public static event Action<State> OnStateChanged;
	public static event Action OnLevelFinished;
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

	public static void LevelCompleted()
	{
		// PlayerManager.currentPlayer.time = TimerManager.spentTime;

		// UIScreenManager.Open<LevelCompleteScreen>();
		UIScreenManager.CloseAll();
		OnLevelFinished?.Invoke();

		Instance.StartCoroutine(LevelCompleteSequence());
		IEnumerator LevelCompleteSequence()
		{
			yield return new WaitForSeconds(2);
			SetState(State.LevelComplete);
		}
	}

	public static void LevelFailed()
	{
		UIScreenManager.CloseAll();
		OnLevelFailed?.Invoke();

		Instance.StartCoroutine(LevelFailedSequence());
		IEnumerator LevelFailedSequence()
		{
			yield return new WaitForSeconds(2);
			SetState(State.LevelFailed);
		}
	}

	public static void EndGame()
	{
		LevelCompleted();
	}
}