using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointer : MonoBehaviour
{
    GameObject newFinger;
    Vector3 pos;
    float offect1;
    public bool needToCreateFinger=true;
    Vector3 endP1;
    CakeConstructor constructor;
    Color color1 = new Color(1, 1, 1, 0.6f);

    void Start()
    {
        constructor = FindObjectOfType<CakeConstructor>();
        offect1 = constructor.offect1;
        StartCoroutine(CreateFinger());
    }

    public IEnumerator CreateFinger()
    {
        yield return new WaitForSeconds(1f);
        GameObject mov = MainObject();
        Picture(mov);
        Finger(mov);
        moveFinger movFinger=mov.AddComponent<moveFinger>();
        movFinger.SetEndPos(pos);
        movFinger.SetParent(transform.gameObject);
        needToCreateFinger = false;
    }

    private void Finger(GameObject mov)
    {
        GameObject finger = FindObjectOfType<CakeConstructor>().finger1;
        newFinger = Instantiate(finger);
        RectTransform newFChildRect = newFinger.transform.GetChild(0).GetComponent<RectTransform>();
        newFChildRect.sizeDelta = newFChildRect.sizeDelta * offect1;
        newFinger.GetComponentInChildren<Animator>().enabled = false;
        newFinger.transform.SetParent(mov.transform);
        newFinger.GetComponent<RectTransform>().localPosition = Vector3.zero - endP1*offect1;
        newFinger.transform.localScale = Vector3.one;
    }

    private void Picture(GameObject mov) //icon silhouette
    {
        GameObject pic = Instantiate(FindObjectOfType<CakeConstructor>().pic);
        pic.transform.SetParent(mov.transform);
        RectTransform picRect = pic.GetComponent<RectTransform>();
        picRect.localPosition = Vector3.zero;
        picRect.sizeDelta = GetComponent<RectTransform>().sizeDelta*offect1;
        Image im = pic.GetComponent<Image>();
        im.sprite = GetComponent<Image>().sprite;
        im.color = color1;
        pic.transform.localScale = Vector3.one;
    }

    private GameObject MainObject()
    {
        GameObject mov = Instantiate(constructor.mov);
        mov.transform.SetParent(GameObject.FindGameObjectWithTag("pictureCanvas").transform);
        mov.transform.localPosition = transform.localPosition;
        mov.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        return mov;
    }

    public void SetEndPos(Vector3 newPos, Vector3 endP)
    {
        pos = newPos;
        endP1 = endP;
    }
}
