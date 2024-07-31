using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleEntity : MonoBehaviour
{
	public HoleTypes types {get; private set;}
	public HoleTypeSwitcher typeSwitcher {get; private set;}
	public HoleGraphics graphics {get; private set;}
	public HoleBallDetection ballDetector {get; private set;}

	private void Awake()
	{
		types = GetComponent<HoleTypes>();
		typeSwitcher = GetComponent<HoleTypeSwitcher>();
		graphics = GetComponent<HoleGraphics>();
		ballDetector = GetComponent<HoleBallDetection>();

		var components = GetComponents<HoleComponent>();
		foreach (var comp in components)
			comp.Init(this);
	}
}