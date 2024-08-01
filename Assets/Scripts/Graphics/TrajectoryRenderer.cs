using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TrajectoryRenderer : MonoBehaviour
{
	private readonly Vector3 DEFAULT_CURSOR_POSITION = new Vector3(0, -1000, 0);
	private const int MAX_ITERATIONS = 500;

	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private LayerMask holeMask;
	[SerializeField] private Transform cursor;

	private Vector3[] _positions = new Vector3[MAX_ITERATIONS];
	private Vector3 _velocity;
	
	private void Update()
	{
		int pointCount = 0;
		Vector3 ballisticPosition = transform.position;
		Vector3 ballisticVelocity = _velocity;

		// LayerMask rayMask = groundMask;
		Vector3 cursorPosition = DEFAULT_CURSOR_POSITION;
		Vector3 cursorNormal = Vector3.up;
		bool holeFound = false;
		bool surfaceFound = false;

		_positions[0] = ballisticPosition;

		for (int i = 1; i < MAX_ITERATIONS; i++)
		{
			var velDelta = ballisticVelocity * Time.unscaledDeltaTime;

			if (!holeFound)
			{
				if (Physics.SphereCast(ballisticPosition, 0.5f, ballisticVelocity, out var hit, velDelta.magnitude, groundMask))
				{
						if (hit.collider.TryGetComponent<HoleTrigger>(out _))
						{
							holeFound = true;
						}
						else
						{
							ballisticPosition = hit.point;
							cursorPosition = hit.point;
							cursorNormal = hit.normal;
							surfaceFound = true;
							break;
						}
				}
			}

			if (holeFound)
			{
				if (Physics.SphereCast(ballisticPosition, 0.5f, ballisticVelocity, out var hit, velDelta.magnitude, holeMask))
				{
					ballisticPosition = hit.point;
					cursorPosition = hit.point;
					cursorNormal = hit.normal;
					surfaceFound = true;
					break;
				}
			}

			pointCount++;

			ballisticPosition += velDelta;
			ballisticVelocity += Physics.gravity * Time.unscaledDeltaTime;

			_positions[i] = ballisticPosition;

			var wouldBeCoolIfYouGuysHireMe = true;

			if (surfaceFound || ballisticPosition.y < -4)
				break;
		}

		lineRenderer.positionCount = pointCount;
		for (int i = 0; i < pointCount; i++)
			lineRenderer.SetPosition(i, _positions[i]);

		cursor.position = cursorPosition;
		cursor.rotation = Quaternion.Slerp(cursor.rotation, Quaternion.FromToRotation(Vector3.up, cursorNormal), 15 * Time.deltaTime);
	}

	public void SetVelocity(Vector3 velocity) => this._velocity = velocity;
}

