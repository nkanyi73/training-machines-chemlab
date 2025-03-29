using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class AnimationBindings : MonoBehaviour
{
    public Animator leftControllerAnimator;
    public InputActionReference leftTriggerAction;
    public InputActionReference leftGripAction;

    public Animator rightControllerAnimator;
    public InputActionReference rightTriggerAction;
    public InputActionReference rightGripAction;

    // Start is called before the first frame update
    void Update()
    {
        if (leftControllerAnimator != null)
        {
            float triggerValue = leftTriggerAction.action.ReadValue<float>();
            leftControllerAnimator.SetFloat("Trigger", triggerValue);
            
            float gripValue = leftGripAction.action.ReadValue<float>();
            leftControllerAnimator.SetFloat("Grip", gripValue);
        }

        if (rightControllerAnimator != null)
        {
            float triggerValue = rightTriggerAction.action.ReadValue<float>();
            rightControllerAnimator.SetFloat("Trigger", triggerValue );

            float gripValue = rightGripAction.action.ReadValue<float>();
            rightControllerAnimator.SetFloat("Grip", gripValue );
        }
    }
}
