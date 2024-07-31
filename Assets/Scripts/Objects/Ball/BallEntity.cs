using UnityEngine;

public class BallEntity : MonoBehaviour
{
	public BallPhysics physics {get; private set;}
	public BallAnimator animator {get; private set;}
	public BallHealth health {get; private set;}
	public BallTriggerDetector triggerDetector {get; private set;}
	public BallGraphics graphics {get; private set;}
	public BallRespawner respawner {get; private set;}
	public BallHoleDetector holeDetector {get; private set;}

	private void Awake()
	{
		physics = GetComponent<BallPhysics>();
		animator = GetComponent<BallAnimator>();
		health = GetComponent<BallHealth>();
		graphics = GetComponent<BallGraphics>();
		triggerDetector = GetComponent<BallTriggerDetector>();
		respawner = GetComponent<BallRespawner>();
		holeDetector = GetComponent<BallHoleDetector>();

		var components = GetComponents<BallComponent>();
		foreach (var comp in components)
			comp.Init(this);
	}
}
