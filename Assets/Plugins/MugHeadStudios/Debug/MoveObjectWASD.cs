using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MugHeadStudios
{
	public class MoveObjectWASD : MonoBehaviour
	{
		void Start()
		{
			
		}

		void FixedUpdate()
		{
			transform.position += new Vector3(Input.GetAxis("Horizontal") * Time.fixedDeltaTime, Input.GetAxis("Vertical") * Time.fixedDeltaTime);
		}
	}
}