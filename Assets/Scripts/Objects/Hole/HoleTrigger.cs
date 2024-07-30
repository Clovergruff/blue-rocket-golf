using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
	[SerializeField] private HoleEntity hole;

	public HoleEntity GetHole() => hole;
}