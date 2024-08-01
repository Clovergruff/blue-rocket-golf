using UnityEngine;

[CreateAssetMenu(menuName = "Gruffdev/Config/Gameplay", fileName ="Gameplay Config")]
public class GameplayConfig : SingletonScriptableObject<GameplayConfig>
{
	public int defaultTime = 60;
	public int ShotSuccessfulTime = 5;
	public int ShotFailedTime = 10;

	[Space]
	public float holeSwitchTimerDuration = 2f;
	public float minHoleSwitchTime = 4f;
	public float maxHoleSwitchTime = 8f;
}
