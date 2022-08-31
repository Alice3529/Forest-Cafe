using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[System.Serializable]

public class DragItem : EventTrigger, IPointerDownHandler
{
    Transform parent;
    float scaleValue = 1.3f;
    moveFinger newFinger;
    pointer newPointer;
    CanvasGroup group;
    CakeConstructor constructor;
    bool canDrag = true;

    void Start()
    {
        newPointer = FindObjectOfType<pointer>();
        group = GetComponent<CanvasGroup>();
        constructor = FindObjectOfType<CakeConstructor>();
    }

    public override void OnPointerDown(PointerEventData data)
    {
        if (constructor.itemDrag != null) { canDrag = false; }
        else {canDrag = true;}
        if (newPointer != null)
        {
            newPointer.StopAllCoroutines();
        }
        DisableFinger();

    }
    public override void OnBeginDrag(PointerEventData data)
    {
        if (canDrag == false) { return; }
        FindObjectOfType<music>().ChooseItem();
        parent = transform.parent;
        transform.SetParent(transform.root);
        transform.localScale = transform.localScale / scaleValue;
        group.blocksRaycasts = false;
        constructor.itemDrag = this.gameObject;

    }

    private void DisableFinger()
    {
        moveFinger finger=FindObjectOfType<moveFinger>();
        if (finger != null)
        {
            finger.Reset();
            newFinger = finger;
            finger.gameObject.SetActive(false);
        }
    }

    public override void OnDrag(PointerEventData data)
    {
        if (canDrag == false) { return; }
        transform.position = data.position;
    }
    public override void OnEndDrag(PointerEventData data)
    {
        if (canDrag == false) { return; }
        constructor.itemDrag = null;
        Touch touch = Input.GetTouch(0);
        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {          
            GetComponent<IMake>().Do(gameObject, newFinger);
        }
        Back();
    }

    public void Back()
    {
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        group.blocksRaycasts = true;
        ShouldEnableFinger();
        
    }

    void ShouldEnableFinger()
    {
        if (newFinger != null)
        {
            newFinger.gameObject.SetActive(true);
            newFinger.StartCoroutine(newFinger.Repeat());
        }
    }
}
