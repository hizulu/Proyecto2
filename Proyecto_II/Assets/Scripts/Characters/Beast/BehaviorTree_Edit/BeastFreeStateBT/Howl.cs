using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jone Sainz Egea
// 02/05/2025
public class Howl : Node, ICoroutineNode
{
    private Blackboard _blackboard;
    private Beast _beast;
    private float _duration;

    private bool _isRunning = false;
    private bool _hasFinished = false;

    public Howl(Blackboard blackboard, Beast beast)
    {
        _blackboard = blackboard;
        _beast = beast;
    }

    public override NodeState Evaluate()
    {
        if (!_isRunning)
        {
            _isRunning = true;
            _hasFinished = false;

            _beast.agent.ResetPath();

            _beast.anim.SetBool("isWalking", false);
            _beast.anim.SetTrigger("howl");
            _beast.SfxBeast.PlayRandomSFX(BeastSFXType.Howl);

            _duration = AnimationDurationDatabase.Instance.GetClipDuration("Beast_Howl");

            _beast.StartNewCoroutine(Howling(_duration), this);
        }

        if (_hasFinished)
        {
            _isRunning = false;
            state = NodeState.SUCCESS;
        }

        else
            state = NodeState.RUNNING;

        return state;
    }

    private IEnumerator Howling(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        OnCoroutineEnd();
    }

    public void OnCoroutineEnd()
    {
        if (_hasFinished) return;

        _blackboard.SetValue("isCoroutineActive", false);
        _blackboard.ClearKey("shouldHowl");

        _hasFinished = true;
    }
}
