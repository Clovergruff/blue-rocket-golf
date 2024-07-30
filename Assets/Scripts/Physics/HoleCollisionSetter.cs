using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollisionSetter : MonoBehaviour
{
	[SerializeField] private LayerMask groundRayLayers;
	[SerializeField] private new Collider collider;

	private bool _holeFound;
	private bool _floorFound;
	private FloorSurface _currentFloor;

	private void Reset()
	{
		groundRayLayers = LayerMask.GetMask("Default", "Ground");
		collider = GetComponentInChildren<Collider>();
	}

	public void UpdateCollisions()
	{
		Vector3 pos = transform.position;
		var hits = Physics.RaycastAll(new Vector3(pos.x, 20, pos.z), Vector3.down, 100, groundRayLayers);
		bool holeFound = false;
		HoleTrigger foundHoleTrigger = null;

		if (hits.Length > 0)
		{
			foreach (var hit in hits)
			{
				if (_floorFound && hit.collider.TryGetComponent<HoleTrigger>(out var holeTrigger))
				{
					holeFound = true;
					foundHoleTrigger = holeTrigger;
				}
				
				if (hit.collider.TryGetComponent<FloorSurface>(out var floor))
					SetFloor(floor);
				else
					SetHole(null);
			}
		}
		else
		{
			SetHole(null);
		}

		if (holeFound)
		{
			SetHole(foundHoleTrigger.GetHole());
		}
	}

	private void SetHole(HoleEntity hole)
	{
		if (!_holeFound && hole)
		{
			_holeFound = true;
			SetFloorCollision(false);
		}
		else if (_holeFound && !hole)
		{
			_holeFound = false;
			SetFloorCollision(true);
		}
	}
	private void SetFloor(FloorSurface floor)
	{
		if (!_floorFound && floor)
		{
			_currentFloor = floor;
			_floorFound = true;
		}
		else if (_floorFound && !floor)
		{
			_currentFloor = null;
			_floorFound = false;
		}
	}

	private void SetFloorCollision(bool collisionEnabled)
	{
		if (!_floorFound)
			return;

		Physics.IgnoreCollision(collider, _currentFloor.GetCollider(), !collisionEnabled);
	}
}
