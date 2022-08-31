using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFinger : MonoBehaviour
{
    Camera camera1;
    bool move = false;
    bool fading = false;
    float val;
    float fadeSpeed = 1.4f;
    float value = 0;
    Vector3 startPos;
    Vector3 endP;
    GameObject parent;
    CanvasGroup canvasGroup;
    void Start()
    {
        camera1 = FindObjectOfType<Camera>();
        startPos = GetComponent<RectTransform>().localPosition;
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(Wait());
    }


    void Update()
    {
        if (move == true)
        {
            Vector3 endPos = camera1.WorldToScreenPoint(FindObjectOfType<CakeConstructor>().GetData().GetPlatePoint() + endP);
            Vector3 pos = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * 500);
            if (Vector3.Distance(transform.position, pos) < 0.5f)
            {
                move = false;
                fading = true;
            }
            transform.position = pos;
        }

        if (fading == true)
        {
            value += Time.deltaTime * fadeSpeed;
            val = Mathf.Lerp(0f,1f, value);
            canvasGroup.alpha = 1-val;
            if (1-val == 0)
            {
                fading = false;
                value = 0;
                StartCoroutine(Repeat());
            }
        }
    }

    public IEnumerator Repeat()
    {
        yield return new WaitForSeconds(1f);
        canvasGroup.alpha = 1;
        GetComponent<RectTransform>().position = parent.GetComponent<RectTransform>().position;
        yield return new WaitForSeconds(1f);
        move = true;
    }

    public void Reset()
    {
        value = 0;
        move = false;
        fading = false;
        canvasGroup.alpha = 0;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        move = true;
    }

    public void SetEndPos(Vector3 pos)
    {
        endP = pos;
    }

    public void SetParent(GameObject par)
    {
        parent = par;
    }
}
