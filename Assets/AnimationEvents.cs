using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Action<string> onAnimationEvent;

    public void OnAnimationEvent(string eventName) {
        onAnimationEvent?.Invoke(eventName);
    }
}
