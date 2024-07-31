using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
	, IPreInitialized
{
	public BallEntity currentBall {get; private set;}

	public BallControllerRespawner respawner {get; private set;}
	public BallControllerControls controls {get; private set;}
	public BallControllerThrower thrower {get; private set;}

	public event Action<BallEntity> OnRespawned;

	public void PreInitialize()
	{
		currentBall = GetComponentInChildren<BallEntity>();

		respawner = GetComponent<BallControllerRespawner>();
		controls = GetComponent<BallControllerControls>();
		thrower = GetComponent<BallControllerThrower>();

		var components = GetComponents<BallControllerComponent>();
		foreach (var comp in components)
			comp.Init(this);
	}
}