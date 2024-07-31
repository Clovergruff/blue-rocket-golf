using System.Collections;
using System.Collections.Generic;
using Mynd.Common;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[CreateAssetMenu(menuName = "Gruffdev/Config/Gameplay", fileName ="Gameplay Config")]
public class GameplayConfig : SingletonScriptableObject<GameplayConfig>
{
	public int defaultTime = 60;
}
