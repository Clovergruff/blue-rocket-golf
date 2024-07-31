using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibGroup : MonoBehaviour
{
	private readonly Vector3 DISAPPEAR_OFFSET = new Vector3(0, -3, 0);

	[SerializeField] private Gib[] gibs;

	private void Awake()
	{
		foreach (var gib in gibs)
		{
			gib.SetVelocity(gib.transform.localPosition * Random.Range(5f, 10f) + new Vector3(0, Random.Range(4f, 8f), 0),
				Random.onUnitSphere * Random.Range(5f, 15f));
		}
	}
	
	private IEnumerator Start()
	{
		float disappearTime = Time.time + 5;
		var waitFixedUpdate = new WaitForFixedUpdate();
		while (Time.time < disappearTime)
		{
			foreach (var gib in gibs)
				gib.UpdateGib();

			yield return waitFixedUpdate;
		}

		foreach (var gib in gibs)
			gib.DisablePhysics();

		float t = 0;
		Vector3 startPosition = transform.position;
		Vector3 endPosition = startPosition + DISAPPEAR_OFFSET;

		while (t < 1)
		{
			t += Time.deltaTime;
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			yield return null;
		}

		// Would be nice to somehow pool this, but it'll be fine for this test thingy
		Destroy(gameObject);
	}
}
