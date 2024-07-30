using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSurface : MonoBehaviour
{
	[SerializeField] private new Collider collider;

	public Collider GetCollider() => collider;
}
