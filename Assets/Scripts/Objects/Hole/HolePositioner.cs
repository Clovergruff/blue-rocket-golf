using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolePositioner : HoleComponent
{
	private const float APPROXIMATE_HOLE_SIZE = 1.3f;

	[SerializeField] private float radius;
	[SerializeField] private AnimationCurve moveCurve;

	private Vector3 _initialPosition;
	private Coroutine _moveCoroutine;

	public override void Init(HoleEntity hole)
	{
		base.Init(hole);

		_initialPosition = transform.position;

		BallRespawner.OnBallRespawned += BallRespawned;
	}

	private void BallRespawned(BallEntity entity)
	{
		if (Random.value < 0.25f)
			SetRandomPosition();
	}

	private void SetRandomPosition()
	{
		if (_moveCoroutine != null)
		{
			StopCoroutine(_moveCoroutine);
			_moveCoroutine = null;
		}

		_moveCoroutine = StartCoroutine(MoveCoroutine());
		IEnumerator MoveCoroutine()
		{
			var randomPos2D = Random.insideUnitCircle * (radius - APPROXIMATE_HOLE_SIZE) * 0.5f;
			Vector3 startPosition = transform.position;
			Vector3 goalPosition = _initialPosition + new Vector3(randomPos2D.x, 0, randomPos2D.y);
			float t = 0;
			while (t < 1)
			{
				t += Time.deltaTime;
				transform.position = Vector3.LerpUnclamped(startPosition, goalPosition, moveCurve.Evaluate(t));
				yield return null;
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;

		var previousMatrix = Gizmos.matrix;
		Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1, 0, 1));
		Gizmos.DrawWireSphere(Vector3.zero, radius);
		Gizmos.matrix = previousMatrix;
	}
}
