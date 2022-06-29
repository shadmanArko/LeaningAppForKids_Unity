using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationText : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("AnimationLoading", 0f,2f);
    }

    async void AnimationLoading()
    {
        
    }
}
