using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawAnimator : MonoBehaviour
{
	[SerializeField] private float speed = 100;
	[Space]
	[SerializeField] private Transform clockwisePivot;
	[SerializeField] private Transform counterClockwisePivot;

	private Vector3 clockwiseEuler = Vector3.zero;
	private Vector3 counterClockwiseEuler = Vector3.zero;

	private void Update()
	{
		clockwiseEuler.z = Time.time * -speed;
		counterClockwiseEuler.z = Time.time * speed;
		clockwisePivot.localEulerAngles = clockwiseEuler;
		counterClockwisePivot.localEulerAngles = counterClockwiseEuler;
	}
}
