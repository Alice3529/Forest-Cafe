using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtrudeFruits : MonoBehaviour
{
    float angle = -1f;
    AnotherData data;
    Vector3 platePoint;
    float posy;
    List<float> angles = new List<float>() { 25, 70, 300, 345};
    int index = 0;
    Vector3 berPos;
    float prevAngle;
    Vector3 pos1;
    [SerializeField] float speed = 0;
    Vector3 endPos;
    bool canCreate = false;
    bool canMove = false;
    Vector3 rotation = new Vector3(0, 15, 0);
    Quaternion angleAxis=Quaternion.Euler(-40f, 10f, -10f);
    float timeCarrot=0.2f;


    private void Start()
    {
        data = FindObjectOfType<CakeConstructor>().GetData();
        platePoint = data.GetPlatePoint();
        posy = transform.position.y + data.nutHeight;
        pos1 = Quaternion.AngleAxis(0, Vector3.up) * Vector3.forward * data.squeezerRadius;
        endPos = new Vector3(pos1.x + platePoint.x, posy + data.squeezerHeight, pos1.z + platePoint.z);
        transform.position = endPos;
        SquizeerRotation();
        StartCoroutine(Wait3());
        if (data.spaces)
        {
            angle = 0;
        }
    }


    IEnumerator Wait3()
    {
        yield return new WaitForSeconds(timeCarrot);
        EnableMeshRenderer();
        canMove = true;

    }
    private void EnableMeshRenderer()
    {
        foreach (Transform child in transform)
        {
            MeshRenderer meshRenderer = child.gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer)
            {
                meshRenderer.enabled = true;
            }
        }
    }

    void Update()
    {    
        if (canMove == false) { return; }

        if (data.spaces)
        {
            CreateBerriesSpace();
        }
        else
        {
            if (canCreate == false) { CreateBerries(); }

        }

        transform.position=Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
        if (transform.position == endPos) { NextPosition(); }
        SquizeerRotation();
        if (angle > 360) { FindObjectOfType<ShelveManager>().NextButton(); Destroy(this.gameObject); }

    }


    void NextPosition()
    {
        angle += data.berryDensity;
        berPos = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward * data.berriesRadius;
        pos1 = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward * data.squeezerRadius;
        endPos = new Vector3(pos1.x + platePoint.x, posy + data.squeezerHeight, pos1.z + platePoint.z);
        canCreate = false;

    }

    private void CreateBerries()
    {
        canCreate = true;
        if (prevAngle!= angle) 
        {
            index = (index + 1) % 2;
            prevAngle = angle;
            Vector3 newPos = new Vector3(berPos.x + platePoint.x, platePoint.y + data.berryOffset, berPos.z + platePoint.z);
            GameObject newObject = Instantiate(data.berries[index].berry, newPos, data.berries[index].berry.transform.rotation);
            music.Music.Berry();

        }
    }

    private void CreateBerriesSpace()
    {
            if (angles.Contains(angle))
            {
                Vector3 newPos = new Vector3(berPos.x + platePoint.x, platePoint.y + data.berryOffset, berPos.z + platePoint.z);
                GameObject newObject = Instantiate(data.berries[index].berry, newPos, data.berries[index].berry.transform.rotation);
                music.Music.Berry();
                angles.Remove(angle);
                if (data.berryLookOnCake)
                {
                    newObject.transform.LookAt(platePoint, Vector3.up);
                    newObject.transform.Rotate(rotation);
                }
            }
    }

    private void SquizeerRotation()
    {
        Vector3 relativePos = new Vector3(platePoint.x, platePoint.y + data.squeezerHeight, platePoint.z) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation * angleAxis;
    }
}
