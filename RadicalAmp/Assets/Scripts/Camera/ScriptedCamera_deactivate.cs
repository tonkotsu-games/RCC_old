using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedCamera_deactivate : MonoBehaviour
{
		float timeLeft = 15.0f;

		void Update()
		{
			timeLeft -= Time.deltaTime;
				if(timeLeft < 0)
				{
				this.gameObject.SetActive (false);
				}
		}
	}