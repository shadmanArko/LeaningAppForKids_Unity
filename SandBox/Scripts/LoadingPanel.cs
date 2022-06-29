using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoadingPanel : MonoBehaviour
{
    public Transform movingObject;
    public Transform upPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimation()
    {
        movingObject.DOMove(upPosition.position, 2).SetLoops(-1,LoopType.Yoyo);
    }

}
