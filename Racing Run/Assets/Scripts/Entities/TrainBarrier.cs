using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainBarrier : MonoBehaviour {
    [Header("Barrier Animator")]
    [Space(10)]
    public Animator barrierAnimator;
    [Header("Movement Time")]
    [Space(10)]
    public float MinMovementTime;
    public float MaxMovementTime;

    private void OnEnable()
    {
        float t = Random.Range(MinMovementTime, MaxMovementTime);
        Invoke("TriggerAnimation", t);
    }
  
    public void TriggerAnimation()
    {
        barrierAnimator.SetTrigger("Transition");
        float t = Random.Range(MinMovementTime, MaxMovementTime);
        Invoke("TriggerAnimation", t);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
