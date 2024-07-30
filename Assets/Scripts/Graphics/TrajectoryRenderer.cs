using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TrajectoryRenderer : MonoBehaviour
{
	private readonly Vector3 DEFAULT_CURSOR_POSITION = new Vector3(0, -1000, 0);
	private const int MAX_ITERATIONS = 500;

	[SerializeField] private Vector3 velocity;
	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private LayerMask holeMask;
	[SerializeField] private Transform cursor;

	private Vector3[] _positions = new Vector3[MAX_ITERATIONS];
	
	private void Update()
	{
		int pointCount = 0;
		Vector3 ballisticPosition = transform.position;
		Vector3 ballisticVelocity = velocity;

		LayerMask rayMask = groundMask;
		Vector3 cursorPosition = DEFAULT_CURSOR_POSITION;
		Vector3 cursorNormal = Vector3.up;

		for (int i = 0; i < MAX_ITERATIONS; i++)
		{
			var velDelta = ballisticVelocity * Time.fixedDeltaTime;

			if (Physics.Raycast(ballisticPosition - velDelta, ballisticVelocity, out var hit, velDelta.magnitude, rayMask))
			{
				if (hit.collider.TryGetComponent<HoleTrigger>(out _))
				{
					rayMask = holeMask;
					if (Physics.Raycast(hit.point, ballisticVelocity, out var hit2, velDelta.magnitude, holeMask))
					{
						cursorPosition = hit2.point;
						cursorNormal = hit2.normal;
						break;
					}
				}
				else
				{
					cursorPosition = hit.point;
					cursorNormal = hit.normal;
					break;
				}
			}

			pointCount++;
			ballisticVelocity += Physics.gravity * Time.fixedDeltaTime;
			ballisticPosition += velDelta;

			_positions[i] = ballisticPosition;

			if (ballisticPosition.y < -4)
				break;
		}

		lineRenderer.positionCount = pointCount;
		for (int i = 0; i < pointCount; i++)
			lineRenderer.SetPosition(i, _positions[i]);

		cursor.position = cursorPosition;
		cursor.rotation = Quaternion.Slerp(cursor.rotation, Quaternion.FromToRotation(Vector3.up, cursorNormal), 15 * Time.deltaTime);
	}

	public void SetVelocity(Vector3 velocity) => this.velocity = velocity;
}

