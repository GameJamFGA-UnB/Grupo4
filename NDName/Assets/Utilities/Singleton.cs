using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	private static T instance;
	public static T Instance {

		get {

			if(instance == null) {
				try{
					instance = FindObjectOfType<T>();
					DontDestroyOnLoad(instance);
				} catch(NullReferenceException e) {
					Debug.Log(e);
					instance = null;
				}
			}
			return instance;
		}
	}
}
