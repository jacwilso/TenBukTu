using UnityEngine;
using System.Collections;

public class DrawAnimatorState : StateMachineBehaviour {

	static Enums.GAME_STATE state = Enums.GAME_STATE.Move;
	static bool user_drawn = false;

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (!user_drawn)
			state = Enums.GAME_STATE.Wait;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (!user_drawn)
			state = Enums.GAME_STATE.Fail;
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	public static Enums.GAME_STATE GetDrawState () {
		return state;
	}

	public static Enums.GAME_STATE UserInput () {
		user_drawn = true;
		if (state.Equals (Enums.GAME_STATE.Wait)) {
			state = Enums.GAME_STATE.Pass;
		} else if (state.Equals (Enums.GAME_STATE.Move)) {
			state = Enums.GAME_STATE.Foul;
		}
		return state;
	}

	public static void ResetAnimator () {
		state = Enums.GAME_STATE.Move;
		user_drawn = false;
	}
}
