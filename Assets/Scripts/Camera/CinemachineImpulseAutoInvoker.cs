using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CinemachineImpulseAutoInvoker : MonoBehaviour
{
	public enum SpaceType
	{
		Basic,
		Spatial,
	}
	
	[SerializeField] private SpaceType spaceType;
	[SerializeField] private CinemachineImpulseSource impulseSource;
	[SerializeField] private Vector3 velocity;

	private void Awake()
	{
		switch (spaceType)
		{
			case SpaceType.Basic:
				impulseSource.GenerateImpulse(velocity);
				break;
			case SpaceType.Spatial:
				impulseSource.GenerateImpulseAt(transform.position, velocity);
				break;
		}
	}
}
