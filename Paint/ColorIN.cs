using UnityEngine;
using UnityEngine.EventSystems;

public class ColorIN : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] int index;
    [SerializeField] Color color;
    int amount = 0;
    internal bool check = false;
    Vector3 increaseVec= new Vector3(1.05f, 1.05f, 1.05f);
    ColorManager colManager;

    void Start()
    {
        colManager = FindObjectOfType<ColorManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (check == true) { return; }
        music.Music.Color();
        transform.localScale = increaseVec;
        colManager.ChangeSelectedColorScale();
        colManager.SetSelectedColor(this.gameObject);
        colManager.SetIndexColor(index, color);
        check = true;
  
    }

    void CheckPartHasColor(Part[] parts)
    {
        foreach (Part p in parts)
        {
            if (p.index == index && p.hasColor == false)
            {
                amount += 1;
                return;
            }
        }
    }

    public void Check()
    {
        Part[] parts = FindObjectsOfType<Part>();
        CheckPartHasColor(parts);
        if (amount == 0)
        {
            colManager.DecreaseColorAmount();
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            colManager.SetSelectedColor(null);
            this.enabled = false;
        }
        amount = 0;
    }
}

