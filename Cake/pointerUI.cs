using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointerUI : MonoBehaviour
{
    GameObject newFinger;
    internal GameObject CreateFinger(float width=73, float height=73)
    {
        GameObject finger = FindObjectOfType<CakeConstructor>().finger;
        Vector3 newPos = FindObjectOfType<Camera>().WorldToScreenPoint(transform.position);
        newFinger = Instantiate(finger);
        newFinger.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        newFinger.SetActive(true);
        newFinger.transform.SetParent(GameObject.FindGameObjectWithTag("pictureCanvas").transform);
        newFinger.transform.position = newPos;
        newFinger.transform.localScale = Vector3.one;
        return newFinger;
    }
   
    internal void DestroyFinger()
    {
        Destroy(newFinger);
    }

    internal void SetAsRound(float rad)
    {
        newFinger.GetComponentInChildren<Animator>().enabled = false;
        circlePointerMov circleP = newFinger.AddComponent<circlePointerMov>();
        circleP.radius = rad;
    }


    internal void SetAsLastSibling()
    {
        newFinger.transform.SetAsLastSibling();
    }
}
