using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MugHeadStudios
{
	public class MugHeadMonoBehaviour : MonoBehaviour
	{
		public T UseComponent<T>(Action<T> action = null) where T : Component
		{
			var component = GetComponent<T>();
			if(component != null && action != null) action(component);
			return component;
		}

		public bool HasComponent<T>() where T : Component
		{
			return GetComponent<T>() != null;
		}
	}
}