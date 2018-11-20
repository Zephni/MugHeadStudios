using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MugHeadStudios
{
	[RequireComponent(typeof(CharacterController2D))]
	public class CharacterInput2D : MugHeadMonoBehaviour
	{
		float HorizontalMove;
		bool Jump = false;

		void FixedUpdate()
		{
			UseComponent<CharacterController2D>().Move(HorizontalMove, false, Jump);

			if (Jump)
				Jump = false;
		}

		void Update()
		{
			if (Input.GetButtonDown("Fire1"))
			Jump = true;

			HorizontalMove = Input.GetAxis("Horizontal");
		}
	}
}