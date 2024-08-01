using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleGraphics : HoleComponent
{
	[SerializeField] private ParticleSystem ballEnteredParticles;
	[SerializeField] private Animator holeAnimator;

	public override void Init(HoleEntity hole)
	{
		base.Init(hole);

		hole.ballDetector.OnBallEntered += BallEntered;
		hole.types.OnVisualsSwitched += VisualsSwitched;
	}

	private void VisualsSwitched(HoleTypes.HoleType type)
	{
		holeAnimator.SetTrigger(AnimHash.SWITCH);
	}

	public void BallEntered(BallEntity ball)
	{
		holeAnimator.SetTrigger(AnimHash.POP);
		ballEnteredParticles.Play();
	}
}
