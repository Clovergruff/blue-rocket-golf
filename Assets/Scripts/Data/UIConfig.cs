using UnityEngine;

[CreateAssetMenu(menuName = "Gruffdev/Config/UI", fileName ="UI Config")]
public class UIConfig : SingletonScriptableObject<UIConfig>
{
	public AnimationCurve buttonPressScaleCurve;
	public AnimationCurve buttonReleaseScaleCurve;
}
