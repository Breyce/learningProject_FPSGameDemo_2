using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvent;
    // ����ڿ��򽻻����ʱչʾ����Ϣ
    public string promptMessage;

    public virtual string OnLook()
    {
        return promptMessage;
    }

    // ������ҽ�ɫ����
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
        // ��������д
    }
}
