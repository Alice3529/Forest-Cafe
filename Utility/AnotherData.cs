
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Obj")]

[System.Serializable]
public class AnotherData : ScriptableObject
{
    public GameObject piece;
    public Vector3 pieceScale;

    public Choice choice;
    public GameObject plate;
    public GameObject cloud;

    public GameObject clapperboard;
    public Vector3 clapperboardPoint;
    public GameObject pows;

    public Vector3 pointOffset;
    public List<ShelveObject> objects = new List<ShelveObject>();

    public Sprite miniPicture;
    public Sprite maxPicture;

    public GameObject fallDecorations;
    public GameObject bag;
    public Vector3 bagPoint;
    public float timeToCreateBag = 2f;

    public GameObject wateringCan;
    public Vector3 sphereScale;
    public GameObject bagWithSeeds;
    public Vector3 pointToAppear;
    public Vector3 pointToAppearW;
    public GameObject flowers;
    public GameObject seed;
    public Vector3 wateringCanSize;
    public Vector2 xWateringCanRange;

    public Vector3 bagWithSeedsSize;
    public Vector2 xBagWithSeedsRange;

    public Vector2 xSpriklingRange;

    public Vector3 secondCameraPos;
    public Vector3 secondCameraRot;


    public bool setFirstCamera;

    public bool setThirdCamera;
    public int amountToGrow;

    public GameObject nut;
    [Range(0, 3f)] public float nutRadius;
    public float nutRadiusMin = 0;
    public float nutRadiusMax=3;
    [Range(-1f, 3f)] public float nutHeight;
    public float nutHeightMin = -1;
    public float nutHeightMax = 3;
    [Range(0, 4f)] public float marshmallowOffset;
    public float marshmallowOffsetMin = 0;
    public float marshmallowOffsetMax = 4;
    public GameObject marshmallow;
    public GameObject nutBar;
    public Vector3 nutpoint;

    public Canvas canvas;
    public GameObject ballsSilhouette;
    public Canvas snowballCanvas;
    public GameObject snowballManager;
    public GameObject snowFlakes;

    public GameObject line;
    public List<LineCreamUI> lineCream = new List<LineCreamUI>();

    public List<PaintUI> colors =  new List<PaintUI>();

    public List<CakeUI> cakes;

    public Vector2 topAndbottom;
    public Vector2 rightAndLeft;
    public int maxSeedAmount = 4;


    public GameObject squeezer;
    public float squeezerRadius;
    public float squeezerRadiusMin = 0f;
    public float squeezerRadiusMax = 3f;
    public float squeezerHeight;
    public float squeezerHeightMin = 0f;
    public float squeezerHeightMax = 3f;
    public float berryOffset;
    public float berryOffsetMin = 0f;
    public float berryOffsetMax = 4f;
    public bool berryLookOnCake = false;
    public float berryDensity;
    public List<BerryObject> berries;
    public float berriesRadius;
    public Vector3 extrudepoint;
    public Vector3 berryScale;
    public Quaternion berryOrientation;
    public Vector3 capOffset;
    public bool manyCakeLayers;
    public bool spaces;
    public List<CreamUI> creams = new List<CreamUI>();

    public List<DecorUI> decor = new List<DecorUI>();
    public List<SilhouetesObject> silhouettes = new List<SilhouetesObject>();

    public List<SplitObject> splitDecorations = new List<SplitObject>();

    public GameObject juiceMaker;
    public GameObject juiceManager;

    public Vector3 roundOffset;

    public GameObject pot;
    public GameObject teaManager;
    public GameObject actionManager;
    public GameObject teaBar;

   
    public GameObject stand;

    public GameObject cookyBox;
    public GameObject puzzleCanvas;

    public GameObject threeMatchCanvas;
    public GameObject boardManager;
    public GameObject iceCream;

    public GameObject colorCanvas;
    public GameObject colorManager;
    public GameObject lollipop;
    public bool shouldRotate;
    public int colorAmount;

    public GameObject juicePlate;
    public Vector3 jPOffset;

    public GameObject strawberry;
    public GameObject spoint;

    public float waitBerry;

    public List<points> teaPoints;

    [System.Serializable]
    public struct SplitObject
    {
        public int amount;
    }


    [System.Serializable]
    public struct ShelveObject
    {
        public GameObject obj;
    }

    [System.Serializable]
    public struct BerryObject
    {
        public GameObject berry;
    }

    [System.Serializable]
    public struct SilhouetesObject
    {
        public GameObject silhouett;
    }

    [System.Serializable]
    public struct CakeUI
    {
        public Sprite sprite;
        public GameObject cake;
        public int index;
        public bool withFinger;
        public Vector3 movePos;
        public Vector3 offset;

    }

    [System.Serializable]
    public struct CreamUI
    {
        public Sprite sprite;
        public GameObject cream;
        public int index;
        public int indexAsCake;
        public bool withFinger;
        public Vector3 movePos;

    }

    [System.Serializable]
    public struct points
    {
        public Vector3 point;

    }

    [System.Serializable]
    public struct DecorUI
    {
        public Things type;
        public Sprite sprite;
        public GameObject decor;
        public int amount;
        public string parentName;
    }

    [System.Serializable]
    public struct PaintUI
    {
        public Sprite sprite;
        public Color color;
        public float radius;
        public bool isRound;
    }

    [System.Serializable]
    public struct LineCreamUI
    {
        public Sprite sprite;
        public int index;
    }

    public Vector3 GetNutPoint()
    {
        return nutpoint + GetPlatePoint();
    }

    public Vector3 GetPlatePoint()
    {
        Vector3 plateP = FindObjectOfType<CakeConstructor>().pointToCreatePlate.position;
        return pointOffset + plateP;
    }

}
