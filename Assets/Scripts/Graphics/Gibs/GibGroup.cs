using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibGroup : MonoBehaviour
{
	[SerializeField] private Gib[] gibs;

	private void Awake()
	{
		foreach (var gib in gibs)
		{
			gib.SetVelocity(gib.transform.localPosition * Random.Range(5f, 10f) + new Vector3(0, Random.Range(4f, 8f), 0),
				Random.onUnitSphere * Random.Range(5f, 15f));
		}
	}

	private void FixedUpdate()
	{
		foreach (var gib in gibs)
			gib.UpdateGib();
	}
}
