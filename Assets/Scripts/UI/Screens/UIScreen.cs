using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIScreen : MonoBehaviour
{
	private readonly Vector3 OPEN_CONTENT_SCALE = new Vector3(0.4f, 0.4f, 0.4f);

	[SerializeField] protected CanvasGroup canvasGroup;
	[SerializeField] protected Transform frontContents;
	public bool isModal = false;

	public abstract void Init();

	public bool IsOpen {get; private set;}

	private Coroutine _toggleCoroutine;

	public static event Action<UIScreen> OnScreenOpened;
	public static event Action<UIScreen> OnScreenClosed;

	public bool Close()
	{
		bool wasOpen = IsOpen;
		IsOpen = false;
		
		if (wasOpen)
		{
			StopToggleCoroutine();
			_toggleCoroutine = UIScreenManager.Instance.StartCoroutine(CloseSequence());
			OnScreenClosed?.Invoke(this);
			OnClosed();
			return true;
		}
	
		return false;

		IEnumerator CloseSequence()
		{
			canvasGroup.blocksRaycasts = false;

			float t = 0;
			float startAlpha = canvasGroup.alpha;
			Vector3 startScale = frontContents ? frontContents.localScale : transform.localScale;

			while (t < 1)
			{
				canvasGroup.alpha = Mathf.LerpUnclamped(startAlpha, 0, UIScreenManager.Instance.curves.fadeOut.Evaluate(t));
				if (frontContents)
					frontContents.localScale = Vector3.LerpUnclamped(startScale, OPEN_CONTENT_SCALE, UIScreenManager.Instance.curves.scaleOut.Evaluate(t));
				t += Time.unscaledDeltaTime * 4;
				yield return null;
			}

			gameObject.SetActive(false);
			canvasGroup.alpha = 0;
			if (frontContents)
				frontContents.localScale = OPEN_CONTENT_SCALE;
		}
	}

	public bool Open()
	{
		bool wasOpen = IsOpen;
		IsOpen = true;
		
		if (!wasOpen)
		{
			StopToggleCoroutine();
			_toggleCoroutine = UIScreenManager.Instance.StartCoroutine(StartSequence());
			OnScreenOpened?.Invoke(this);
			OnOpened();
			return true;
		}
	
		return false;

		IEnumerator StartSequence()
		{
			if (!gameObject.activeSelf)
			{
				canvasGroup.alpha = 0;
				if (frontContents)
					frontContents.localScale = OPEN_CONTENT_SCALE;
				gameObject.SetActive(true);
			}
			
			float t = 0;
			float startAlpha = canvasGroup.alpha;
			Vector3 startScale = frontContents ? frontContents.localScale : transform.localScale;

			while (t < 1)
			{
				canvasGroup.alpha = Mathf.LerpUnclamped(startAlpha, 1, UIScreenManager.Instance.curves.fadeIn.Evaluate(t));
				if (frontContents)
					frontContents.localScale = Vector3.LerpUnclamped(startScale, Vector3.one, UIScreenManager.Instance.curves.scaleIn.Evaluate(t));
				t += Time.unscaledDeltaTime * 4;
				yield return null;
			}

			canvasGroup.blocksRaycasts = true;	
			canvasGroup.alpha = 1;
			if (frontContents)
				frontContents.localScale = Vector3.one;
		}
	}

	protected virtual void OnOpened() { }
	protected virtual void OnClosed() { }

	public virtual void OnCloseButton()
	{
		Close();
	}

	private void StopToggleCoroutine()
	{
		if (_toggleCoroutine != null)
		{
			UIScreenManager.Instance.StopCoroutine(_toggleCoroutine);
			_toggleCoroutine = null;
		}
	}

	private void Reset()
	{
		if (!TryGetComponent<CanvasGroup>(out canvasGroup))
			canvasGroup = gameObject.AddComponent<CanvasGroup>();
	}
}