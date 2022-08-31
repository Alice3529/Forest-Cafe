using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    int currentIndex;
    [SerializeField] Color currentColor;
    [SerializeField] Color greyColor;
    public bool canPaint = true;
    AnotherData data;
    int colorAmount;
    ShelveManager shelveManager;
    internal GameObject selectedColor;

    private void Start()
    {
        shelveManager = FindObjectOfType<ShelveManager>();
        data = FindObjectOfType<CakeConstructor>().GetData();
        colorAmount = data.colorAmount;
        FindObjectOfType<ShelveManager>().quit.SetActive(false);
    }

    public void DecreaseColorAmount()
    {
         colorAmount-= 1;
        if (colorAmount == 0)
        {
            FindObjectOfType<picture_scale>().StartPos();   
            StartCoroutine(End());
        }
    }
    public void SetIndexColor(int index, Color color)
    {
        Part[] parts=FindObjectsOfType<Part>();
        foreach (Part p in parts)
        {
            Image im = p.GetComponent<Image>();

            if (p.index==index && p.hasColor == false)
            {
                im.color = greyColor;
            }
            else if (p.hasColor == false)
            {
                im.color = Color.white;
            }
        }
        currentIndex = index;
        currentColor = color;
        TurnOffRaycastTarget();
    }

    public Color GetCurrentColor()
    {
        return currentColor;
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    public void SetSelectedColor(GameObject color)
    {
        selectedColor = color;
    }

    public void ChangeSelectedColorScale()
    {
        if (selectedColor == null) { return; }
        selectedColor.GetComponent<ColorIN>().check = false;
        selectedColor.transform.localScale = Vector3.one;
    }

  

    IEnumerator End()
    {
        FindObjectOfType<IncAndDicButton>().gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        shelveManager.colorCanvas.SetActive(false);
        shelveManager.lolipop.SetActive(true);
        shelveManager.ActiveEnviroment();
        GameObject.FindGameObjectWithTag("mainCanvas").GetComponent<Canvas>().enabled = true;
        StartCoroutine(shelveManager.EndActions());

    }

    public void TurnOffRaycastTarget()
    {
        Part[] parts = FindObjectsOfType<Part>();
        foreach (Part obj in parts)
        {
            if (obj.index != currentIndex)
            {
                Target(obj.transform, false);
            }
            else
            {
                Target(obj.transform, true);

            }
        }

    }

    void Target(Transform obj, bool active)
    {
        obj.GetComponent<Image>().raycastTarget = active;
        foreach (Transform child in obj.transform)
        {
            Image im = child.GetComponent<Image>();
            if (im != null)
            {
                im.raycastTarget = active;
            }
        }
    }
}
 