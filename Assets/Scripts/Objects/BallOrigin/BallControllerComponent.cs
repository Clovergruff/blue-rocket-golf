using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallControllerComponent : MonoBehaviour
{
	public BallController controller {get; private set;}

	public virtual void Init(BallController owner) => this.controller = owner;
}
