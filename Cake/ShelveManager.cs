using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class ShelveManager : MonoBehaviour
{
    AnotherData data;
    List<GameObject> shelves=new List<GameObject>();
    public GameObject camera3;
    public GameObject camera2;
    public GameObject camera1;
    [SerializeField] ParticleSystem endParticles;
    internal GameObject nutBar;
    List<GameObject> sweets = new List<GameObject>();
    int index = 0;
    Transform bigCake;
    int powsAmount = 0;
    public GameObject enviroment;
    string shelveName;
    int sil = 0;
    int silInScene = 0;
    float timeToCreate = 2f; 
    int amount;
    public bool canCreateSquizeer;
    float time = 1f;
    GameObject snowballManager;
    Canvas pictureCanvas;
    float cloudOffset = 1.8f;
    RotateCake rotateCake;
    internal GameObject colorCanvas;
    internal bool canChangeScene = false;
    public GameObject quit;
    public GameObject shelveObject;
    public GameObject cakeExample;
    internal GameObject lolipop;
    internal GameObject cooky;
    internal GameObject icecream;



    void New()
    {
        camera2.SetActive(false);
        if (data.choice == Choice.Juice)
        {
            FindObjectOfType<cakePicture1>().gameObject.SetActive(false);
            Instantiate(data.juiceMaker, data.GetPlatePoint(), Quaternion.Euler(0, 270, 0));
            Instantiate(data.juiceManager);
            Instantiate(data.juicePlate, data.GetPlatePoint() + data.jPOffset, Quaternion.identity);
            return;
        }

        if (data.choice == Choice.Tea)
        {
            Instantiate(data.teaManager);
            FindObjectOfType<teaData>().CreateCanvas();
            return;
        }

        if (data.choice == Choice.Cookie)
        {
           pictureCanvas.enabled = false;
           cooky = Instantiate(data.cookyBox, data.GetPlatePoint(), Quaternion.identity);
           cooky.SetActive(false);
           Instantiate(data.puzzleCanvas);
            return;
        }

        if (data.choice == Choice.IceCream)
        {
            pictureCanvas.enabled = false;
            icecream = Instantiate(data.iceCream, data.GetPlatePoint(), Quaternion.identity);
            icecream.SetActive(false);
            Instantiate(data.threeMatchCanvas);
            GameObject boardManager = Instantiate(data.boardManager);
            boardManager.SetActive(true);
            return;
        }

        if (data.choice == Choice.Lollipop)
        {
            pictureCanvas.enabled = false;
            GameObject.FindGameObjectWithTag("mainCanvas").GetComponent<Canvas>().enabled = false;
            lolipop=Instantiate(data.lollipop, data.GetPlatePoint(), Quaternion.identity);
            lolipop.SetActive(false);
            colorCanvas = Instantiate(data.colorCanvas);
            GameObject boardManager = Instantiate(data.colorManager);
            return;

        }

        CreateShelves();
    }

    private void CreateShelves()
    {
        foreach (AnotherData.ShelveObject shelve in data.objects)
        {
            GameObject newSh = Instantiate(shelve.obj, shelveObject.transform);
            RectTransform newShRect = newSh.GetComponent<RectTransform>();
            RectTransform shelveObjRect = shelve.obj.GetComponent<RectTransform>();
            newShRect.anchoredPosition = shelveObjRect.anchoredPosition;
            newShRect.offsetMin = shelveObjRect.offsetMin;
            newShRect.offsetMax = shelveObjRect.offsetMax;
            newShRect.localScale = Vector3.one;
            shelves.Add(newSh);
        }
    }

    private void Start()
    {
        pictureCanvas = GameObject.FindGameObjectWithTag("pictureCanvas").GetComponent<Canvas>();
        data = FindObjectOfType<CakeConstructor>().GetData();
        Cameras();
        New();
        enviroment = GameObject.Find("Enviroment");
        StartCoroutine(LoadCaffeScene());
        if (DoNotCreateShelves()) { return; }
        bigCake = GameObject.FindGameObjectWithTag("BigCake").transform;
        foreach (GameObject lShelve in shelves)
        {
            lShelve.SetActive(false);
        }
        shelves[index].SetActive(true);

    }

    private bool DoNotCreateShelves()
    {
        if (data.choice == Choice.Juice ) { return true; }
        if (data.choice == Choice.Tea) { return true; }
        if (data.choice == Choice.Cookie) { return true; }
        if (data.choice == Choice.IceCream) { enviroment.SetActive(false); return true; }
        if (data.choice == Choice.Lollipop) { enviroment.SetActive(false); return true; }
        return false;
    }

    private void Cameras()
    {
        camera2.transform.position = data.secondCameraPos;
        camera2.transform.rotation = Quaternion.Euler(data.secondCameraRot.x, data.secondCameraRot.y, data.secondCameraRot.z);
        camera2.SetActive(false);
        camera3.SetActive(false);
    }

    public void NextButton()
    {
        if (index < shelves.Count - 1)
        {
            index += 1;
            shelveName = shelves[index].gameObject.name;
            Sprinkling();
            IsCreamShelve();
            IsFruitsShelve();
            IsGardenShelve();
            IsMarshmallowShelve();
            IsFlameShelve();
            IsEnd();
            IsCloudShelve();
            IsSnowballShelve();
            IsClapperboardShelve();
            IsLightsShalve();
            IsDecorationShelve();
            shelves[index - 1].SetActive(false);
            shelves[index].SetActive(true);

            if (data.setFirstCamera == true && camera1.activeInHierarchy==false)
            {
                camera1.SetActive(true);
                camera2.SetActive(false);
                if (canCreateSquizeer)
                {
                    StartCoroutine(CreateSquizeer());
                    canCreateSquizeer = false;
                }
            }
        }
    }
    public IEnumerator Again()
    {
        yield return new WaitForSeconds(time);
        Transform pows = GameObject.FindGameObjectWithTag("pows").transform;
        foreach (Transform pow in pows)
        {
            MeshRenderer meshRenderer = pow.GetComponent<MeshRenderer>();
            if (meshRenderer.enabled == false)
            {
                IncreasePowsAmount();
            }
            meshRenderer.enabled = true;
        }
    }

    IEnumerator CreateSquizeer()
    {
        yield return new WaitForSeconds(2.5f);
        Instantiate(data.squeezer, data.extrudepoint + data.GetPlatePoint(), Quaternion.identity);
    }

    private void IsDecorationShelve()
    {
        GameObject bigCake = GameObject.FindGameObjectWithTag("BigCake");
        if (shelveName.Contains("Decoration") == true)
        {
            if (data.shouldRotate == true && rotateCake == null)
            {
                 rotateCake = bigCake.AddComponent<RotateCake>();
            }
        }
       // else if (rotateCake != null) { rotateCake.enabled = false; }
    }

    private void IsLightsShalve()
    {
        if (shelveName.Contains("lights"))
        {
            FindObjectOfType<greentree>().enabled = true;
        }
    }

    private void IsFlameShelve()
    {
        if (shelveName.Contains("flame") == true)
        {
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
    }

    public void IsCloudShelve()
    {
        if (shelveName.Contains("cloud") || shelveName.Contains("Cloud"))
        {
            Vector3 point = data.GetPlatePoint();
            Vector3 pos = new Vector3(point.x, point.y + cloudOffset, point.z);
            Instantiate(data.cloud, pos, data.cloud.transform.rotation);
            GameObject snowFlakes=Instantiate(data.snowFlakes, point , Quaternion.identity);
            snowFlakes.transform.SetParent(bigCake);

        }
    }

    public void IsEnd()
    {
        if (shelveName.Contains("End") == true)
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
            StartCoroutine(EndActions());

        }
    }

    public void ActiveEnviroment()
    {
        endParticles.Play();
        enviroment.SetActive(true);
    }

    public void EnableCanvas()
    {
        StartCoroutine(Canvas());
        StartCoroutine(EndActions());

    }

    IEnumerator Canvas()
    {
        yield return new WaitForSeconds(0.5f);
        pictureCanvas.enabled = true;

    }

    public IEnumerator EndActions()
    {
        quit.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        music.Music.Win();
        if (endParticles.isPlaying == false)
        {
            endParticles.Play();
        }
        FindObjectOfType<food1>().SetPiece(data.piece, data.pieceScale, data.choice);
        enviroment.SetActive(true);
        yield return new WaitForSeconds(6f);
        music.Music.StopSound();
        canChangeScene = true;
    }

    public void ChangeScene()
    {
        FindObjectOfType<food1>().shouldExist = false;
        FindObjectOfType<DecreaseBButton>().state = 0;

    }
    public void SetLoadScreen()
    {
        StopAllCoroutines();
        music.Music.StopSound();
        FindObjectOfType<SceneIndex>().index = 1;
        SceneManager.LoadScene(3);
       
    }

    public void CheckCanClick(IncAndDicButton quit)
    {
        if (FindObjectOfType<CakeConstructor>().itemDrag != null) { return; }
        quit.SetFirstState1();
    }

    IEnumerator LoadCaffeScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                if (canChangeScene == true)
                {
                    asyncLoad.allowSceneActivation = true;

                }
            }
            yield return null;
        }
    }

    public void IsFruitsShelve()
    {
        if (shelveName.Contains("Fruits") == true)
        {
             canCreateSquizeer = true;
        }
    }

    public void IsSnowballShelve()
    {
        if (shelveName.Contains("Snowball") == true)
        {
            GameObject snowballGroup=Instantiate(data.ballsSilhouette, data.GetPlatePoint(), Quaternion.identity) as GameObject;
            snowballGroup.transform.SetParent(bigCake.transform);
            snowballManager=Instantiate(data.snowballManager);
            GameObject snowballCanvas=Instantiate(data.snowballCanvas.gameObject);
            RotateCake rotateCake=bigCake.gameObject.AddComponent<RotateCake>();
            rotateCake.enabled = true;
        }
        else
        {
            if (snowballManager != null)
            {
                Destroy(snowballManager);
            }
        }
       
    }
    public void IsClapperboardShelve()
    {
        if (shelveName.Contains("ClapperboardShelve")==true)
        {
            Vector3 point = data.GetPlatePoint() + data.clapperboardPoint;
            StartCoroutine(CreateObject(data.clapperboard, point, Vector3.zero, data.clapperboard.transform.localScale, timeToCreate));
            Instantiate(data.pows, data.GetPlatePoint(), Quaternion.identity);
            amount = FindObjectsOfType<powsCollision>().Length;

        }
    }


    public void IsMarshmallowShelve()
    {
        if (shelveName.Contains("MarshmallowShelve")==true)
        {
            StartCoroutine(CreateNut());
        }
    }

    IEnumerator CreateNut()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(data.nut, data.GetNutPoint(), Quaternion.identity);
        DragCord cord = FindObjectOfType<DragCord>();
        cord.StartCoroutine(cord.CreateLineAndRound(0f));
        nutBar=Instantiate(data.nutBar, pictureCanvas.transform);
    }
    public void IsCreamShelve()
    {
        if (shelveName.Contains("LineCream") == true)
        {
            Instantiate(data.line, data.GetPlatePoint(), Quaternion.identity);
        }
        
    }

    public void Sprinkling()
    {
        if (shelveName.Contains("Sprikling") == true)
        {
            StartCoroutine(CreateObject(data.bag, data.GetPlatePoint() + data.bagPoint, Vector3.zero, data.bag.transform.localScale, 2.3f));
            GameObject decor=Instantiate(data.fallDecorations, data.GetPlatePoint(), Quaternion.identity);
            decor.transform.SetParent(GameObject.FindGameObjectWithTag("BigCake").transform);
        }
        else
        {
            DestroyBagAndSweets();
        }
    }

    private void DestroyBagAndSweets()
    {
        Sprikling obj = FindObjectOfType<Sprikling>();
        if (obj != null)
        {
            Destroy(obj.gameObject);
        }
        if (sweets.Count != 0)
        {
            foreach (GameObject sweet in sweets)
            {
                Destroy(sweet);
            }
        }
    }

    public void IsGardenShelve()
    {
        if (shelveName.Contains("Garden") == true)
        {
            if (data.setThirdCamera == true)
            {
                camera3.SetActive(true);
            }
            Vector3 pos = data.GetPlatePoint() + data.pointToAppear;
            StartCoroutine(CreateObject(data.bagWithSeeds, pos, new Vector3(0,-90,0), data.bagWithSeedsSize, data.timeToCreateBag));
            GameObject flowerCircle = Instantiate(data.flowers, data.GetPlatePoint(), Quaternion.identity);
        }
    }

    IEnumerator CreateObject(GameObject obj, Vector3 point, Vector3 axis, Vector3 scale, float time)
    {
        yield return new WaitForSeconds(time);
        GameObject newObject=Instantiate(obj, point, Quaternion.Euler(axis.x, axis.y, axis.z));
        newObject.transform.localScale = scale;
        Sprikling sprikling = FindObjectOfType<Sprikling>();

        if (sprikling != null)
        {
            sprikling.SetSweetsAmount(data.fallDecorations.transform.childCount);
        }

    }

    public void IncreasePowsAmount()
    {
        powsAmount += 1;
        if (powsAmount == amount)
        {
            Destroy(GameObject.FindGameObjectWithTag("Clapperboard"));
            NextButton();
        }
    }

    public void IncreaseSilAmount()
    {
        sil += 1;
      
        if (silInScene == sil)
        {
            silInScene = 0;
            sil = 0;
            if (shelves[index + 1].gameObject.name.Contains("Decoration")) { NextButton(); return; }
            if (data.spaces)
            {
                StartCoroutine(Wait(2f));
            }
            else
            {
                NextButton();
            }
            
        }
        
    }

    public void IncreaseSilInScene(int am)
    {
        silInScene += am;

    }

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        NextButton();

    }

    public void DestroyFoodPiece()
    {
        Destroy(FindObjectOfType<food1>().gameObject);
    }

}
