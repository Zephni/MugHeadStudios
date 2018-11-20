using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MugHeadStudios_Example
{
	public class RotateCube : MonoBehaviour
	{
		void Update()
		{
			transform.Rotate(new Vector3(1, 1, 0), Time.deltaTime * 200);
		}
	}
}