using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
	public event Action<BallEntity> OnBallEntered;
	public void BallEntered(BallEntity ball) => OnBallEntered?.Invoke(ball);
}
