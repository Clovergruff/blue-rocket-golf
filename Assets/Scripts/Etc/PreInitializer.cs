using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreInitializer : MonoBehaviour
{
	private void Awake()
	{
		IPreInitialized[] initializables = FindObjectsOfType<MonoBehaviour>(true).OfType<IPreInitialized>().ToArray();
		foreach (var i in initializables)
			i.PreInitialize();
	}
}

public interface IPreInitialized
{
	public void PreInitialize();
}
