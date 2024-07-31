using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleGraphics : HoleComponent
{
	[SerializeField] private ParticleSystem ballEnteredParticles;

	public override void Init(HoleEntity hole)
	{
		base.Init(hole);

		hole.ballDetector.OnBallEntered += BallEntered;
	}

	public void BallEntered(BallEntity ball)
	{
		ballEnteredParticles.Play();
	}
}
