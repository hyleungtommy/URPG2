using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	Animator animator;
	// Use this for initialization
	void Start ()
	{
		animator = (Animator)GetComponent ("Animator");
		//animator.Play ("Basic Attack");
	}

	// Update is called once per frame
	void Update ()
	{

		AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
		if (asi.normalizedTime >= 1)
		{
			Destroy(gameObject);
		}
	}

	void AnimationEnd() 
	{

		//Destroy (gameObject); //消滅物件
	}
}
