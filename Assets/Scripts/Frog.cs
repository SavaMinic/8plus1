using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour
{
	public bool GoingRight { get; private set; }
	public int CurrentIndex { get; private set; }

	private Animator frogAnimator;
	private LillyPads pads;
	private FrogsManager manager;
	private GoTween jumpTween;

	private static bool canClick = true;

	void Awake()
	{
		frogAnimator = GetComponent<Animator>();
		frogAnimator.speed = 0f;

		pads = FindObjectOfType<LillyPads>();
		manager = FindObjectOfType<FrogsManager>();
	}

	public void Initialize(int index)
	{
		GoingRight = index < 4;
		CurrentIndex = -1;
		SetIndex(index);

		// don't need to change z
		transform.position = (Vector2)pads.Pads[index].transform.position;
		transform.rotation = Quaternion.Euler(0f, 0f, GoingRight ? 270f : 90f);

		// stop tween if it's running
		if (jumpTween != null && jumpTween.state == GoTweenState.Running)
		{
			jumpTween.complete();
		}
	}

	public void SetIndex(int index)
	{
		if (CurrentIndex != -1) pads.Pads[CurrentIndex].frog = null;
		CurrentIndex = index;
		pads.Pads[CurrentIndex].frog = this;
	}

	void OnMouseUpAsButton()
	{
		if (!canClick) return;
		// try just one ahead
		// if it can't, try to jump over him
		if (TryToMove() || TryToMove(2))
		{
			manager.CheckEnd();
		}
	}

	private bool TryToMove(int howMuch = 1)
	{
		int nextIndex = CurrentIndex + (GoingRight ? 1 : -1) * howMuch;
		// check bounds
		if (nextIndex >= 0 && nextIndex <=8 && !pads.Pads[nextIndex].IsOccupied)
		{
			Move(nextIndex);
			return true;
		}
		return false;
	}

	private void Move(int index)
	{
		SetIndex(index);
		var target = (Vector2) pads.Pads[index].transform.position;

		jumpTween = Go.to(transform, manager.jumpDuration, new GoTweenConfig().position(target).setEaseType(manager.jumpEaseType));
		jumpTween.setOnBeginHandler(tween =>
		{
			frogAnimator.speed = 1f;
			canClick = false;
		});
		jumpTween.setOnCompleteHandler(tween =>
		{
			frogAnimator.speed = 0f;
			canClick = true;
		});
	}
}
