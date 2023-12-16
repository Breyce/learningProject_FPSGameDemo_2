using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvent;
    // 玩家在看向交互组件时展示的信息
    public string promptMessage;

    public virtual string OnLook()
    {
        return promptMessage;
    }

    // 将由玩家角色调用
    public void BaseInteract()
    {
        if (useEvent)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }
        Interact();
    }
    protected virtual void Interact()
    {
        // 被子类重写
    }
}
