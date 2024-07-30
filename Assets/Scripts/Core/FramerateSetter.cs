using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateSetter : MonoBehaviour
	, IPreInitialized
{
	[SerializeField] private int targetFramerate = 60;

	public void PreInitialize() => Application.targetFrameRate = targetFramerate;
}
