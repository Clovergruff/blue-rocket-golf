using UnityEngine;

public class HoleTypes : HoleComponent
{
	public enum HoleType
	{
		Good,
		Bad,
	}

	[SerializeField] private GameObject goodContent;
	[SerializeField] private GameObject badContent;

	public void SetType(HoleType type)
	{
		goodContent.SetActive(type == HoleType.Good);
		badContent.SetActive(type == HoleType.Bad);
	}
}