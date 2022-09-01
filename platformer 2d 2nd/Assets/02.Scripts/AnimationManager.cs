using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public bool isReady { get => animator != null; }
    public float speed
    {
        get => animator.speed;
        set => animator.speed = value;
    }
    public Animator animator { get; private set; }

    public void Play(string clipName) => animator.Play(clipName);
    public float GetAnimationTime(string clipName)
    {
        RuntimeAnimatorController rac = animator.runtimeAnimatorController;
        for (int i = 0; i < rac.animationClips.Length; i++)
        {
            if (rac.animationClips[i].name == clipName)
            {
                return rac.animationClips[i].length;
            }
        }

        Debug.LogWarning($"GetAnimationTime : {clipName} À» Ã£À» ¼ö ¾ø½À´Ï´Ù.");
        return -1.0f;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}

