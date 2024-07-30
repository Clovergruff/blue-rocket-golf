using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
	, IPreInitialized
{
	public BallEntity currentBall {get; private set;}

	public BallRespawner respawner {get; private set;}
	public BallControls controls {get; private set;}
	public BallThrower thrower {get; private set;}

	public void PreInitialize()
	{
		currentBall = GetComponentInChildren<BallEntity>();

		respawner = GetComponent<BallRespawner>();
		controls = GetComponent<BallControls>();
		thrower = GetComponent<BallThrower>();

		var components = GetComponents<BallControllerComponent>();
		foreach (var comp in components)
			comp.Init(this);
	}
}