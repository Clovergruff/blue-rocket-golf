using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonFeedback : MonoBehaviour
	, IPointerDownHandler
	, IPointerUpHandler
	, IPointerClickHandler
	, IPointerExitHandler
{
	[SerializeField] private float clickScale = 0.8f;
	[SerializeField] private float clickRotation = 0;
	private Coroutine _pressCoroutine;
	private float _origRot, _rot;

	private void Start()
	{
		_origRot = transform.localEulerAngles.z;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		// GameUI.ButtonClick();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		// GameUI.ButtonDown();
		PressDown();
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		Release();
	}
	public void OnPointerExit(PointerEventData eventData) => Release();

	private void PressDown()
	{
		StopToggleCoroutine();
		_pressCoroutine = StartCoroutine(PressSequence());
		IEnumerator PressSequence()
		{
			float t = 0;
			Vector3 startScale = transform.localScale;
			Vector3 clickScaleVector = new Vector3(clickScale, clickScale, clickScale);
			Vector3 rot = transform.localEulerAngles;
			float startRot = _rot;
			float goalRot = _origRot + clickRotation;

			while (t < 1)
			{
				t += Time.unscaledDeltaTime * 4;
				float evalT = UIConfig.Instance.buttonPressScaleCurve.Evaluate(t);
				_rot = Mathf.LerpUnclamped(startRot, goalRot, evalT);
				rot.z = _rot;
				transform.localScale = Vector3.LerpUnclamped(startScale, clickScaleVector, evalT);
				transform.localEulerAngles = rot;
				yield return null;
			}
		}
	}

	private void Release()
	{
		StopToggleCoroutine();
		_pressCoroutine = StartCoroutine(ReleaseSequence());
		IEnumerator ReleaseSequence()
		{
			float t = 0;
			Vector3 startScale = transform.localScale;
			Vector3 rot = transform.localEulerAngles;
			float startRot = _rot;
			float goalRot = _origRot;

			while (t < 1)
			{
				t += Time.unscaledDeltaTime * 4;
				float evalT = UIConfig.Instance.buttonReleaseScaleCurve.Evaluate(t);
				_rot = Mathf.LerpUnclamped(startRot, goalRot, evalT);
				rot.z = _rot;
				transform.localScale = Vector3.LerpUnclamped(startScale, Vector3.one, evalT);
				transform.localEulerAngles = rot;
				yield return null;
			}
		}
	}

	private void StopToggleCoroutine()
	{
		if (_pressCoroutine != null)
		{
			StopCoroutine(_pressCoroutine);
			_pressCoroutine = null;
		}
	}
}