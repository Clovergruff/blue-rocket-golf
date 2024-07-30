using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallComponent : MonoBehaviour
{
	public BallEntity ball {get; private set;}

	public virtual void Init(BallEntity ball)
	{
		this.ball = ball;
	}
}