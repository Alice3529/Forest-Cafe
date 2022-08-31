using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Part : MonoBehaviour, IPointerClickHandler
{
    public int index;
    public bool hasColor = false;
    ColorManager colorManager;


    void Start()
    {
        colorManager = FindObjectOfType<ColorManager>();
    }
 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (colorManager.GetCurrentIndex() == index && hasColor==false)
        {
            music.Music.Paint();
            Color color = colorManager.GetCurrentColor();
            GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);
            if (transform.childCount > 0) {
                Destroy(transform.GetChild(0).gameObject);
                Destroy(transform.GetChild(1).gameObject);

            }
            hasColor = true;
            colorManager.selectedColor.GetComponent<ColorIN>().Check();

        }
    }

}
