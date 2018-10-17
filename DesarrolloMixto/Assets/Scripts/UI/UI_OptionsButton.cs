using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OptionsButton : MonoBehaviour {
    public Animator animations;
    public void TriggerButton()
    {
        animations.SetTrigger("trigger");
    }
}
