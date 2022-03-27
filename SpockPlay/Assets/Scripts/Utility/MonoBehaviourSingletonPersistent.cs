using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSingletonPersistent<T> : MonoBehaviour
	where T : Component
{
	private static T _instance;
	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject obj = new GameObject
				{
					name = typeof(T).Name
				};
				obj.AddComponent<T>();
			}
			return _instance;
		}
	}

	protected virtual void Awake()
	{
		_instance = this as T;
		DontDestroyOnLoad(gameObject);
		Init();
	}

	protected virtual void Init() { }
}

