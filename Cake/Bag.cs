using System.Collections;
using UnityEngine;

public class Bag : MonoBehaviour
{
    AnotherData data;
    bool onesWater;
    [SerializeField] int maxSeedsAmount;
    [SerializeField] GameObject seedCreator;
    internal int amount = 0;
    Quaternion startRot;
    Quaternion rot;
    GameObject newFinger;
    float timeToCreateFinger = 2f;
    int canMove=0;
    float timeWateringCan = 2f;
    float angle = 0;
    bool once = false;
    float speed = 100;

    void Start()
    {
        data = FindObjectOfType<CakeConstructor>().GetData();
        startRot = transform.rotation;
        CreateFinger();

    }

    private void OnMouseDown()
    {
        canMove = 1;
        StopAllCoroutines();
        if (newFinger != null)
        {
            newFinger.SetActive(false);
        }
    }

    private void OnMouseUp()
    {
        canMove = 2;
    }


    void Update()
    {
        CreateSeed();

        if (canMove==1)
        {
            RotateToXBag(-0.6f);
            once = false;
        }
        else if (canMove==2)
        {
            Reset1();
            RotateBackXBag(rot, startRot);

        }
    }

    private void Reset1()
    {
        if (once == false)
        {
            angle = 0;
            rot = transform.rotation;
            once = true;
        }
    }

    private void CreateSeed()
    {
        if (seedCreator != null)
        {
            if (seedCreator.transform.childCount < maxSeedsAmount)
            {
                seedCreator.GetComponent<seedCreator>().CreateSeed();
            }
        }
    }

    IEnumerator CreateWateringCan()
    {
        yield return new WaitForSeconds(timeWateringCan);
        GameObject can = Instantiate(data.wateringCan, data.GetPlatePoint()+data.pointToAppearW, data.wateringCan.transform.rotation);
        can.transform.localScale = data.wateringCanSize;
        Destroy(this.gameObject);
    }
    public void SetSeedAmount()
    {
        FlowerCollider[] seeds = FindObjectsOfType<FlowerCollider>();
        foreach (FlowerCollider seed in seeds)
        {
            if (seed.hasSeed == false) { return; }
        }
        DisableBag();
    }

    private void DisableBag()
    {
        if (onesWater == false)
        {
            foreach (Transform child in transform)
            {
                seedCreator seedCreator = child.GetComponent<seedCreator>();
                if (seedCreator != null)
                {
                    Destroy(seedCreator.gameObject);
                }
            }
            GetComponent<MeshCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(CreateWateringCan());
            onesWater = true;
        }
    }

    private void EnableFinger()
    {
        StartCoroutine(EnableNewFinger());

    }

    IEnumerator EnableNewFinger()
    {
        yield return new WaitForSeconds(timeToCreateFinger);
        if (newFinger != null)
        {
            newFinger.SetActive(true);
        }
    }


    void CreateFinger()
    {
        newFinger=transform.GetComponentInChildren<pointerUI>().CreateFinger();  
    }

    private void OnDestroy()
    {
        if (newFinger != null)
        {
            Destroy(newFinger);
        }
    }

    public void RotateToXBag(float value)
    {
        if (transform.rotation.x > value)
        {
            transform.Rotate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }

    }
    public void RotateBackXBag(Quaternion startRot, Quaternion endRot)
    {
        if (transform.rotation.x < 0)
        {
            angle += Time.deltaTime;
            Quaternion newRot = Quaternion.Lerp(startRot, endRot, angle);
            transform.rotation = newRot;
            return;
        }
        canMove = 0;
        EnableFinger();

    }


}
