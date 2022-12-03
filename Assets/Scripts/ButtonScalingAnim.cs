/*
This script is animate the button, here it is scaling
*/

using UnityEngine;
using DG.Tweening;

public class ButtonScalingAnim : MonoBehaviour
{
    private float mScaleTo=0.7f;
    private float mScaleAnimationDuration=1f;
    private int mInfiniteLooping=-1;
    private LoopType mTypeOfLoop=LoopType.Yoyo;
    private void Start(){
        CarryOutScalingAnimation();
    }

    private void CarryOutScalingAnimation(){
        gameObject.transform.DOScale(mScaleTo,mScaleAnimationDuration).SetLoops(mInfiniteLooping,mTypeOfLoop);
    }
}
