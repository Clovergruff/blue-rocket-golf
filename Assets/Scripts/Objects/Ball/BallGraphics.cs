using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGraphics : BallComponent
{
	[SerializeField] private MeshRenderer grabOutlineRenderer;

	public override void Init(BallEntity ball)
	{
		base.Init(ball);

		grabOutlineRenderer.enabled = false;
	}

	public void SetGrabbed()
	{
		grabOutlineRenderer.enabled = true;
	}

	public void SetReleased()
	{
		grabOutlineRenderer.enabled = false;
	}
}
