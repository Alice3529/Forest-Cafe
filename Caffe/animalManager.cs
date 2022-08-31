using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class animalManager : MonoBehaviour
{
    public Transform animal;
    public List<GameObject> animals;
    int index;
    int prevIndex = -100;
    internal bool wasActive = false;
    [SerializeField] Transform foodContainer;
    [SerializeField] GameObject plate;
    GameObject food1;
    [SerializeField] questionIcon question;
    internal GameObject currentClothes;
    int prevInd = -1;
    GameObject currentAnimal;
    GameObject canvas;
    public Image quit;


    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("mainCanvas");
        food1 newfood = FindObjectOfType<food1>();
        if (newfood != null)
        {
            wasActive = true;
            if (newfood.shouldExist == false)
            {
                Destroy(newfood.gameObject);
            }
        }
     
    }

    IEnumerator NextAnimal(float time)
    {
        yield return new WaitForSeconds(time);
        index = UnityEngine.Random.Range(0, 3);
        prevInd = PlayerPrefs.GetInt("prevIndex", -1);
        StopAnimalRepetition();
        PlayerPrefs.SetInt("prevIndex", index);
        currentAnimal=Instantiate(animals[index], animal);
        SaveData();

    }

    private void StopAnimalRepetition()
    {
        if (prevInd == index)
        {
            index++;
            if (index > 2) {index = 0;}
        }
    }

    void Start()
    {
        music.Music.CaffeMusic();
        RecoverData();
        food1 newfood = FindObjectOfType<food1>();
        if (newfood != null)
        {
            if (newfood.shouldExist == false) { return; }
            CreateFood(newfood);
            SetAnimalAnimation(newfood);
            Destroy(newfood.gameObject);
            StartCoroutine(Wait());
        }
    }

    private void CreateFood(food1 newfood)
    {
        food1 = Instantiate(newfood.piece, foodContainer.transform);
        food1.transform.localPosition = Vector3.zero;
        food1.transform.localScale = newfood.scale;
    }

    private void SetAnimalAnimation(food1 newfood)
    {
        Animator animalAnim = currentAnimal.GetComponent<Animator>();
        if (newfood.choice == Choice.Lollipop || newfood.choice == Choice.IceCream || newfood.choice == Choice.Juice || newfood.choice == Choice.Tea)
        {
            plate.SetActive(false);
        }
        else if (newfood.choice == Choice.Cake || newfood.choice == Choice.Cookie || newfood.choice == Choice.Cupcake)
        {
            plate.SetActive(true);
        }
        animalAnim.SetLayerWeight(1, 0);
        animalAnim.SetLayerWeight(2, 1);
    }

    public void GetParams(int index1) 
    {
        index = index1;
        if (prevIndex != index)
        {
            prevIndex = index;
            SetParams();
        }
    }

    public void SetParams()
    {
        currentAnimal=Instantiate(animals[index], animal);
     
    }

    public IEnumerator Wait()
    {
        animationPropertis properties = FindObjectOfType<animationPropertis>();
        yield return new WaitForSeconds(7f);
        properties.StopClapping();
        yield return new WaitForSeconds(0.5f);
        properties.FoodDisappear();
        yield return new WaitForSeconds(0.5f);
        plate.SetActive(false);
        Destroy(currentAnimal.gameObject);
        StartCoroutine(NextAnimal(0.8f));
    }
    private void Reset1()
    {
        if (food1 != null)
        {
            Destroy(food1);
        }
        StopAllCoroutines();
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("index", index);
    }

    private void RecoverData()
    {
        if (PlayerPrefs.HasKey("index"))
        {
            index = PlayerPrefs.GetInt("index");
            SetParams();
        }
        else
        {
            StartCoroutine(NextAnimal(0f));
        }
    }

    public void WaitWhileEat()
    {
        if (food1 != null)
        {
            Destroy(food1);
        }
    }
    public void CreateBookGroup(GameObject obj, GameObject que)
    {
        Instantiate(obj, canvas.transform);
        transform.position = Vector3.zero;
        que.GetComponent<IncAndDic3>().wasClicked = false;
        que.transform.parent.gameObject.SetActive(false);

    }

    public void ActiveQuestion()
    {
        question.gameObject.SetActive(true);
        question.canIncrease = true;
    }

    public void ActiveQuestion1()
    {
        question.gameObject.SetActive(true);
        question.canIncrease = false;
    }

    public void LoadFirstScene()
    {
        music.Music.StopSound();
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }


}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
