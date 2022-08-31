using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCake : MonoBehaviour, IMake
{
    AnotherData.CakeUI prop;
    GameObject cake;
    Transform bigCake;
    AnotherData data;

    void Start()
    {
        data = FindObjectOfType<CakeConstructor>().GetData();
        bigCake = GameObject.FindGameObjectWithTag("BigCake").transform;
    }
  

    public void SetProp(AnotherData.CakeUI newProp)
    {
        prop = newProp;
    }

    public void Do(GameObject obj, moveFinger newFinger)
    {
        var cakesInScene = FindObjectsOfType<Cake>();
        int length = cakesInScene.Length;
        if (length == prop.index)
        {
            music.Music.DropItem();
            NewCake(obj);
            CheckIsLastCake();
            if (newFinger != null)
            {
                Destroy(newFinger.gameObject);
            }
        }
        else
        {
            GetComponent<DragItem>().Back();
        }
        
    }

    private void NewCake(GameObject obj)
    {
        Vector3 point = data.GetPlatePoint();
        cake = GameObject.Instantiate(prop.cake, point, Quaternion.identity);
        cake.transform.SetParent(bigCake);
        Destroy(obj);
        if (cake != null && data.choice == Choice.Cake)
        {
            cake.GetComponentInChildren<Cake>().SetIndex(prop.index);
        }
    }

    private void CheckIsLastCake()
    {
        if ((data.cakes.Count-1)==prop.index)
        {
            FindObjectOfType<ShelveManager>().NextButton();

        }
    }
}
