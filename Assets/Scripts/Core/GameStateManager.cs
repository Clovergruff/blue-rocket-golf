using System;
using System.Collections;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	public enum State
	{
		Intro,
		MainMenu,
		Gameplay,
		LevelComplete,
		LevelFailed,
	}
	
	public static event Action<State> OnStateChanged;
	public static event Action OnLevelFinished;
	public static event Action OnLevelFailed;
	public static State CurrentState {get; private set;}

	public static GameStateManager Instance {get; private set;}

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		Application.targetFrameRate = 120;
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
}