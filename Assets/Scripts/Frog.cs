using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour
{

	private Animator frogAnimator;

	void Awake()
	{
		frogAnimator = GetComponent<Animator>();
		frogAnimator.speed = 0f;
	}
}
