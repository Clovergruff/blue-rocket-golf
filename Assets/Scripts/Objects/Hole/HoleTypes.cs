using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HoleTypes : HoleComponent
{
	public enum HoleType
	{
		Good,
		Bad,
	}

	[SerializeField] private GameObject[] goodContents;
	[SerializeField] private GameObject[] badContents;

	private HoleType _currentHoleType = HoleType.Bad;

	public event Action<HoleType> OnVisualsSwitched;

	public override void Init(HoleEntity hole)
	{
		base.Init(hole);

		hole.typeSwitcher.OnHoleSwitched += HoleSwitched;

		if (Random.value > 0.5f)
			ForceSetHoleGraphicContent(HoleType.Good);
	}

	private void HoleSwitched() => SetType(_currentHoleType == HoleType.Good ? HoleType.Bad : HoleType.Good);
	public void SetType(HoleType type)
	{
		if (_currentHoleType == type)
			return;

		OnVisualsSwitched?.Invoke(type);
		ForceSetHoleGraphicContent(type);
	}

	private void ForceSetHoleGraphicContent(HoleType type)
	{
		_currentHoleType = type;
		foreach (var goodContent in goodContents)
			goodContent.SetActive(type == HoleType.Good);
		foreach (var badContent in badContents)
			badContent.SetActive(type == HoleType.Bad);
	}
}