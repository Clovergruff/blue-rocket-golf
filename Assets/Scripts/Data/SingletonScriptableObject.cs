using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
	private static bool _instanceFound;
	private static T _instance;

	public static bool Exists
	{
		get
		{
			FindInstance();
			return _instanceFound;
		}
	}

	public static T Instance
	{
		get
		{
			FindInstance();
			return _instance;
		}
	}

	private static void FindInstance()
	{
		if (_instanceFound)
		{
			return;
		}

		var assets = Resources.LoadAll<T>("");
		if (assets == null || assets.Length < 1)
		{
			throw new System.Exception($"No {typeof(T)} instances exist!");
		}

		if (assets.Length > 1)
		{
			throw new System.Exception($"There are multiple {typeof(T)} instances!");
		}

		_instanceFound = true;
		_instance = assets[0];
	}
    }