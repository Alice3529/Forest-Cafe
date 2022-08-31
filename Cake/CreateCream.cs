using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCream : MonoBehaviour, IMake
{
    int layerMask = 1 << 7;
    GameObject cream;
    int index = 0;
    [SerializeField] int indexAsCake;
    int creamAmount;
    DragItem drag;
    ShelveManager shelveManager;

    void Start()
    {
        creamAmount = FindObjectsOfType<CreateCream>().Length;
        drag = GetComponent<DragItem>();
        shelveManager = FindObjectOfType<ShelveManager>();
    }

    public void Do(GameObject obj, moveFinger finger)
    {
        AnotherData data = FindObjectOfType<CakeConstructor>().GetData();
        RaycastHit hit = FindObjectOfType<CameraRaycater>().Hit(layerMask);
        if (hit.collider == null) {
            drag.Back();
            return; 
        }
        Cake cake = hit.collider.GetComponent<Cake>();

        if (data.manyCakeLayers == false && cake.GetCream() == false && cake.GetIndex() == indexAsCake)
        {
            OneCakeLayer(obj, hit, cake);

        }
        else if (data.manyCakeLayers==true)
        {
            ManyCakeLayers(obj, hit, cake);
        }
        else
        {
            drag.Back();
        }
    }

    private void ManyCakeLayers(GameObject obj, RaycastHit hit, Cake cake)
    {
        if (index == cake.GetCreamIndex())
        {
            music.Music.DropItem();
            GameObject newCream = Instantiate(cream, hit.collider.gameObject.transform.position, Quaternion.identity);
            newCream.transform.SetParent(cake.transform);
            cake.SetCreamIndex();
            Destroy(obj);
            if (index == creamAmount - 1)
            {
                shelveManager.NextButton();
            }

        }
        else
        {
            drag.Back();
        }
    }

    private void OneCakeLayer(GameObject obj, RaycastHit hit, Cake cake)
    {
        music.Music.DropItem();
        GameObject newCream = Instantiate(cream, hit.collider.gameObject.transform.position, Quaternion.identity);
        newCream.transform.SetParent(cake.transform);
        cake.SetCream();
        Destroy(obj);
        CheckNext();
    }

    private void CheckNext()
    {
        int creamAmount = GameObject.FindGameObjectsWithTag("Cream").Length;
        int cakeAmount = GameObject.FindGameObjectsWithTag("Cake").Length;
        if (cakeAmount == creamAmount){ shelveManager.NextButton();}
    }

    public void SetCream(GameObject newCream)
    {
        cream = newCream;
    }

    public void SetIndex(int newIndex)
    {
        index = newIndex;
    }

    public void SetIndexAsCake(int newIndex)
    {
        indexAsCake = newIndex;
    }
}
