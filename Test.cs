using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


[CustomEditor(typeof(AnotherData))]
public class Test : Editor
{
    public AnotherData dat;
    ReorderableList list;
    SerializedProperty shelves;

    ReorderableList listCream;
    SerializedProperty creams;

    ReorderableList teaP;
    SerializedProperty teaPoints;

    ReorderableList listPaint;
    SerializedProperty paint;

    ReorderableList listCake;
    SerializedProperty cakes;

    ReorderableList listBerries;
    SerializedProperty berry;

    ReorderableList listCakeCream;
    SerializedProperty cakeCream;

    ReorderableList listSilhouettes;
    SerializedProperty silhoutte;

    ReorderableList listDecor;
    SerializedProperty decor;

    ReorderableList listSplit;
    SerializedProperty split;

    int spaceAmount = 5;
    int width = 130;
    float labelPercents=0.2f;
    float propertyPercents = 0.8f;


    public void OnEnable()
    {
        dat = (AnotherData)target;
        list = NewReordableList("objects", shelves, list, DrawListItems, DrawHeader, 1);
        listCream = NewReordableList("lineCream", creams, listCream, DrawListItemsCream, DrawHeaderCream,2);
        if (dat.choice == Choice.Cake)
        {
            listCake = NewReordableList("cakes", cakes, listCake, DrawListItemsCake, DrawHeaderCake, 6);
        }
        else
        {
            listCake = NewReordableList("cakes", cakes, listCake, DrawListItemsCake, DrawHeaderCake, 5);

        }
        listPaint =NewReordableList("colors", paint, listPaint, DrawListItemsPaint, DrawHeaderPaint,4);
        listBerries = NewReordableList("berries", berry, listBerries, DrawListItemsBerries, DrawHeaderBerries, 1);
        listCakeCream = NewReordableList("creams", cakeCream, listCakeCream, DrawListItemsCakeCream, DrawHeaderCakeCream, 6);
        listSilhouettes = NewReordableList("silhouettes", silhoutte, listSilhouettes, DrawListItemsSilhoutte, DrawHeaderSilhoutte, 1);
        listDecor = NewReordableList("decor", decor, listDecor, DrawListItemsDecor, DrawHeaderDecor, 5);
        listSplit = NewReordableList("splitDecorations", split, listSplit, DrawListItemsSplit, DrawHeaderSplit,1);
        teaP = NewReordableList("teaPoints", teaPoints, teaP, DrawListItemsTea, DrawHeaderTea, 1);

    }

    ReorderableList NewReordableList(string propName, SerializedProperty prop, ReorderableList list, ReorderableList.ElementCallbackDelegate DrawListItems, ReorderableList.HeaderCallbackDelegate DrawHeader, int fieldsAmount)
    {
        prop = serializedObject.FindProperty(propName);
        list = new ReorderableList(serializedObject, prop, true, true, true, true);
        list.elementHeight = EditorGUIUtility.singleLineHeight * fieldsAmount + 2f;
        list.drawElementCallback = DrawListItems;
        list.drawHeaderCallback = DrawHeader;
        return list;
    }
    private void DrawListItemsCream(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = listCream.serializedProperty.GetArrayElementAtIndex(index);
        CreatePropertyField(element, rect, "Sprite", "sprite", 0);
        CreatePropertyField(element, rect, "Index", "index", 1);

    }

    void DrawHeaderCream(Rect rect)
    {
        EditorGUI.LabelField(rect, "CreamLines");
    }

    private void DrawListItemsTea(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = teaP.serializedProperty.GetArrayElementAtIndex(index);
        CreatePropertyFieldWithoitLabel(element, rect, null, "point", 0);

    }

    void DrawHeaderTea(Rect rect)
    {
        EditorGUI.LabelField(rect, "TeaPoints");
    }

    private void DrawListItemsPaint(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = listPaint.serializedProperty.GetArrayElementAtIndex(index);
        CreatePropertyField(element, rect, "Sprite", "sprite", 0);
        CreatePropertyField(element, rect, "Color", "color", 1);
        CreatePropertyField(element, rect, "Radius", "radius", 2);
        CreatePropertyField(element, rect, "isRound", "isRound", 3);

    }

    void DrawHeaderPaint(Rect rect)
    {
        EditorGUI.LabelField(rect, "Paint");
    }

    private void DrawListItemsSplit(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = listSplit.serializedProperty.GetArrayElementAtIndex(index);
        CreatePropertyFieldWithoitLabel(element, rect, "Split", "amount", 0);

    }

    void DrawHeaderSplit(Rect rect)
    {
        EditorGUI.LabelField(rect, "Split");
    }
    private void DrawListItemsDecor(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = listDecor.serializedProperty.GetArrayElementAtIndex(index);
        CreatePropertyField(element, rect, "Type", "type", 0);
        CreatePropertyField(element, rect, "Sprite", "sprite", 1);
        CreatePropertyField(element, rect, "Decor", "decor", 2);
        CreatePropertyField(element, rect, "Amount", "amount", 3);
        CreatePropertyField(element, rect, "ParentName", "parentName", 4);
    }

    void DrawHeaderDecor(Rect rect)
    {
        EditorGUI.LabelField(rect, "Decor");
    }

    private void DrawListItemsCake(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = listCake.serializedProperty.GetArrayElementAtIndex(index);
        CreatePropertyField(element, rect, "Sprite", "sprite", 0);
        CreatePropertyField(element, rect, "Cake", "cake", 1);
        CreatePropertyField(element, rect, "Finger", "withFinger", 2);
        CreatePropertyField(element, rect, "MovePos", "movePos", 3);
        CreatePropertyField(element, rect, "Offset", "offset", 4);



        if (dat.choice == Choice.Cake)
        {
            CreatePropertyField(element, rect, "Index", "index", 5);
        }
    
    }

    void DrawHeaderBerries(Rect rect)
    {
        EditorGUI.LabelField(rect, "Berries");
    }

    private void DrawListItemsBerries(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = listBerries.serializedProperty.GetArrayElementAtIndex(index);
        CreatePropertyFieldWithoitLabel(element, rect, "Berry", "berry", 0);
    }


    void DrawHeaderSilhoutte(Rect rect)
    {
        EditorGUI.LabelField(rect, "Silhoettes");
    }

    private void DrawListItemsSilhoutte(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = listSilhouettes.serializedProperty.GetArrayElementAtIndex(index);
        CreatePropertyFieldWithoitLabel(element, rect, "Silhoette", "silhouett", 0);
    }


    void DrawHeaderCake(Rect rect)
    {
        EditorGUI.LabelField(rect, "Cakes");
    }

    void DrawHeaderCakeCream(Rect rect)
    {
        EditorGUI.LabelField(rect, "Cake cream");
    }

    private void DrawListItemsCakeCream(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = listCakeCream.serializedProperty.GetArrayElementAtIndex(index);
        CreatePropertyField(element, rect, "Sprite", "sprite", 0);
        CreatePropertyField(element, rect, "Cream", "cream", 1);
        CreatePropertyField(element, rect, "Index", "index", 2);
        CreatePropertyField(element, rect, "IndexAsCake", "indexAsCake", 3);
        CreatePropertyField(element, rect, "Finger", "withFinger", 4);
        CreatePropertyField(element, rect, "MovePos", "movePos",5);


    }



    void CreatePropertyField(SerializedProperty element, Rect rect, string fieldName, string propertyName, int number)
    {
        EditorGUI.LabelField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight*number, (rect.width * labelPercents), EditorGUIUtility.singleLineHeight), fieldName);

        EditorGUI.PropertyField(
           new Rect(rect.x + (rect.width * labelPercents), rect.y + EditorGUIUtility.singleLineHeight * number, rect.width * propertyPercents, EditorGUIUtility.singleLineHeight),
           element.FindPropertyRelative(propertyName),
           GUIContent.none
       );
    }
    void CreatePropertyFieldWithoitLabel(SerializedProperty element, Rect rect, string fieldName, string propertyName, int number)
    {
        EditorGUI.PropertyField(
           new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * number, rect.width, EditorGUIUtility.singleLineHeight),
           element.FindPropertyRelative(propertyName),
           GUIContent.none
       );
    }


    public override void OnInspectorGUI()
    {
        #region choice
        GUILayout.Space(spaceAmount);
        EditorGUI.BeginChangeCheck();
        Choice newChoice = (Choice)EditorGUILayout.EnumPopup("Choice", dat.choice);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change plate object");
            dat.choice = newChoice;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region piece
        EditorGUI.BeginChangeCheck();
        GameObject piece = (GameObject)EditorGUILayout.ObjectField("Piece", dat.piece, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Piece");
            dat.piece = piece;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region pieceScale
        EditorGUI.BeginChangeCheck();
        Vector3 pieceScale = EditorGUILayout.Vector3Field("pieceScale", dat.pieceScale);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "pieceScale");
            dat.pieceScale = pieceScale;
            EditorUtility.SetDirty(target);
        }

        #endregion
        if (dat.choice == Choice.Juice)
        {
            Juice();
            return;
        }
        if (dat.choice == Choice.Tea)
        {
            Tea();
            return;
        }
        if (dat.choice == Choice.Lollipop)
        {
            Lollipop1();
            return;
        }
        if (dat.choice == Choice.Cookie)
        {
            Cooky();
            return;
        }
        if (dat.choice == Choice.IceCream)
        {
            IceCream();
            return;
        }

        if (dat.choice == Choice.Cupcake)
        {
            EditorGUI.BeginChangeCheck();
            bool shouldRotate = EditorGUILayout.Toggle("shouldRotate", dat.shouldRotate);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "shouldRotate");
                dat.shouldRotate = shouldRotate;
                EditorUtility.SetDirty(target);
            }
        }
        CreatePictureFields();
        CreateCameraFields();
        if (dat.choice == Choice.Cake)
        {
            GUILayout.Space(spaceAmount);
            GUILayout.Label("Plate", EditorStyles.boldLabel);
            #region plate
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Plate", GUILayout.Width(130));
            EditorGUI.BeginChangeCheck();
            GameObject plateObject = (GameObject)EditorGUILayout.ObjectField(dat.plate, typeof(GameObject), false);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Change plate object");
                dat.plate = plateObject;
                EditorUtility.SetDirty(target);
            }
            EditorGUILayout.EndHorizontal();
            #endregion
            #region point
            Point();
            #endregion

        }
        else
        {
            GUILayout.Space(spaceAmount);
            GUILayout.Label("Mold", EditorStyles.boldLabel);
            #region point
            Point();
            #endregion

        }
        CreateList(list, "Shelves");
        CreateShelves();

    }

    void CreateCameraFields()
    {
        #region secondCameraPos
        EditorGUI.BeginChangeCheck();
        Vector3 secondCameraPos = EditorGUILayout.Vector3Field("secondCameraPos", dat.secondCameraPos);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "secondCameraPos");
            dat.secondCameraPos = secondCameraPos;
            EditorUtility.SetDirty(target);
        }

        #endregion
        #region secondCameraRot
        EditorGUI.BeginChangeCheck();
        Vector3 secondCameraRot = EditorGUILayout.Vector3Field("secondCameraRot", dat.secondCameraRot);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "secondCameraRot");
            dat.secondCameraRot = secondCameraRot;
            EditorUtility.SetDirty(target);
        }

        #endregion
    }

    private void Lollipop1()
    {
       
        Point();
        CreatePictureFields();
        GUILayout.Space(spaceAmount);
        #region lollipop
        EditorGUI.BeginChangeCheck();
        GameObject lollipop = (GameObject)EditorGUILayout.ObjectField("Lollipop", dat.lollipop, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Lollipop");
            dat.lollipop = lollipop;
            EditorUtility.SetDirty(target);
        }
        #endregion

        #region colorManager
        EditorGUI.BeginChangeCheck();
        GameObject colorManager = (GameObject)EditorGUILayout.ObjectField("ColorManager", dat.colorManager, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "ColorManager");
            dat.colorManager = colorManager;
            EditorUtility.SetDirty(target);
        }
        #endregion

        #region colorAmount
        EditorGUI.BeginChangeCheck();
        int colorAmount = EditorGUILayout.IntField("colorAmount", dat.colorAmount);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "colorAmount");
            dat.colorAmount = colorAmount;
            EditorUtility.SetDirty(target);
        }
        #endregion


        #region colorCanvas
        EditorGUI.BeginChangeCheck();
        GameObject colorCanvas = (GameObject)EditorGUILayout.ObjectField("ColorCanvas", dat.colorCanvas, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "ColorCanvas");
            dat.colorCanvas = colorCanvas;
            EditorUtility.SetDirty(target);
        }
        #endregion
    }

    private void IceCream()
    {
        Point();
        CreatePictureFields();
        GUILayout.Space(spaceAmount);
        #region iceCream
        EditorGUI.BeginChangeCheck();
        GameObject iceCream = (GameObject)EditorGUILayout.ObjectField("IceCream", dat.iceCream, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "IceCream");
            dat.iceCream = iceCream;
            EditorUtility.SetDirty(target);
        }
        #endregion

        #region boardManager
        EditorGUI.BeginChangeCheck();
        GameObject boardManager = (GameObject)EditorGUILayout.ObjectField("BoardManager", dat.boardManager, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "BoardManager");
            dat.boardManager = boardManager;
            EditorUtility.SetDirty(target);
        }
        #endregion

        #region threeMatchCanvas
        EditorGUI.BeginChangeCheck();
        GameObject threeMatchCanvas = (GameObject)EditorGUILayout.ObjectField("ThreeMatchCanvas", dat.threeMatchCanvas, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "ThreeMatchCanvas");
            dat.threeMatchCanvas = threeMatchCanvas;
            EditorUtility.SetDirty(target);
        }
        #endregion
    }

    private void CreateShelves()
    {
        int amount = 0;
        foreach (AnotherData.ShelveObject shelve in dat.objects)
        {
            if (shelve.obj == null) { return; }
            if (shelve.obj.name == "CakeShelve")
            {
                Cake();
            }
            if (shelve.obj.name == "LineCreamShelve")
            {
                Cream();
            }
            if (shelve.obj.name == "ClapperboardShelve")
            {
                Clapperboard();
            }
            if (shelve.obj.name == "SnowballShelve")
            {
                Snowball();
            }
            if (shelve.obj.name == "GardenShelve")
            {
                Garden();
            }
            if (shelve.obj.name == "MarshmallowShelve")
            {
                Marshmallow();
            }
            if (shelve.obj.name == "CreamShelve")
            {
                CreamBig();
            }
            if (shelve.obj.name == "SpriklingShelve")
            {
                Sprikling();
            }
            if (shelve.obj.name == "FruitsShelve")
            {
                Fruits();
            }
            if (shelve.obj.name == "PaintShelve")
            {
                Paint();
            }
            if (shelve.obj.name == "CloudShelve")
            {
                Cloud();
            }
            if (shelve.obj.name == "DecorationShelve")
            {
                EditorGUI.BeginChangeCheck();
                bool setFirstCamera = EditorGUILayout.Toggle("SetFirstCamera",dat.setFirstCamera);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "SetFirstCamera");
                    dat.setFirstCamera = setFirstCamera;
                    EditorUtility.SetDirty(target);
                }

                amount += 1;
                if (amount < 2)
                {
                    Decorations();
                }
            }


        }

    }

    void Cooky()
    {
        Point();
        CreatePictureFields();
        GUILayout.Space(spaceAmount);
        #region box
        EditorGUI.BeginChangeCheck();
        GameObject box = (GameObject)EditorGUILayout.ObjectField("Box", dat.cookyBox, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Box");
            dat.cookyBox = box;
            EditorUtility.SetDirty(target);
        }
        #endregion

        #region puzzles
        EditorGUI.BeginChangeCheck();
        GameObject puzzle = (GameObject)EditorGUILayout.ObjectField("Puzzles", dat.puzzleCanvas, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Puzzles");
            dat.puzzleCanvas = puzzle;
            EditorUtility.SetDirty(target);
        }
        #endregion

    }

    private void Lollipop()
    {
        Point();
        CreatePictureFields();
        CreateList(list, "Shelves");
        CreateShelves();
        GUILayout.Space(spaceAmount);
        #region stand
        EditorGUI.BeginChangeCheck();
        GameObject stand = (GameObject)EditorGUILayout.ObjectField("Stand", dat.stand, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Stand");
            dat.stand = stand;
            EditorUtility.SetDirty(target);
        }
        #endregion

    }

    private void Tea()
    {
        Point();
        CreateList(teaP, "teaP");
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Tea", EditorStyles.boldLabel);
        #region bag
        EditorGUI.BeginChangeCheck();
        GameObject pot = (GameObject)EditorGUILayout.ObjectField("Pot", dat.pot, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Pot");
            dat.pot = pot;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region teaManager

        EditorGUI.BeginChangeCheck();
        GameObject teaManager = (GameObject)EditorGUILayout.ObjectField("TeaManager", dat.teaManager, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "TeaManager");
            dat.teaManager = teaManager;
            EditorUtility.SetDirty(target);
        }
        #endregion
        

        EditorGUI.BeginChangeCheck();
        GameObject teaBar = (GameObject)EditorGUILayout.ObjectField("teaBar", dat.teaBar, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "teaBar");
            dat.teaBar = teaBar;
            EditorUtility.SetDirty(target);
        }
        RoundOffset();
    }

    private void CreatePictureFields()
    {
        #region pictures
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Pictures", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();
        Sprite minPicture = (Sprite)EditorGUILayout.ObjectField("Min picture", dat.miniPicture, typeof(Sprite), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Min picture");
            dat.miniPicture = minPicture;
            EditorUtility.SetDirty(target);
        }
        EditorGUI.BeginChangeCheck();
        Sprite maxPicture = (Sprite)EditorGUILayout.ObjectField("Max picture", dat.maxPicture, typeof(Sprite), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Min picture");
            dat.maxPicture = maxPicture;
            EditorUtility.SetDirty(target);
        }
        #endregion
    }

    private void Juice()
    {
        Point();
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Juice", EditorStyles.boldLabel);
        #region maker

        EditorGUI.BeginChangeCheck();
        GameObject maker = (GameObject)EditorGUILayout.ObjectField("Maker", dat.juiceMaker, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Maker");
            dat.juiceMaker = maker;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region bag
        EditorGUI.BeginChangeCheck();
        GameObject manager = (GameObject)EditorGUILayout.ObjectField("Manager", dat.juiceManager, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Manager");
            dat.juiceManager = manager;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region jPOffset
        EditorGUI.BeginChangeCheck();
        Vector3 jPOffset = EditorGUILayout.Vector3Field("PlateOffset", dat.jPOffset);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "jPOffset");
            dat.jPOffset = jPOffset;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region strawberry
        EditorGUI.BeginChangeCheck();
        GameObject strawberry = (GameObject)EditorGUILayout.ObjectField("strawberry", dat.strawberry, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "strawberry");
            dat.strawberry = strawberry;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region strawberryP
        EditorGUI.BeginChangeCheck();
        GameObject strawberryP = (GameObject)EditorGUILayout.ObjectField("strawberryPoint", dat.spoint, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "strawberryPoint");
            dat.spoint = strawberryP;
            EditorUtility.SetDirty(target);
        }
        #endregion
    }

    void Cake()
    {
          CreateList(listCake, "Cakes");
    }

    void Cream()
    {
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Line", EditorStyles.boldLabel);
        GameObject newC=CreateField("Line", dat.line);
        if (newC != null)
        {
            Undo.RecordObject(target, "Line");
            dat.line = newC;
            EditorUtility.SetDirty(target);
        }
        CreateList(listCream, "Line");
    }

    GameObject CreateField(string label, GameObject obj)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(label, GUILayout.Width(width));
        EditorGUI.BeginChangeCheck();
        GameObject newObj = (GameObject)EditorGUILayout.ObjectField(obj, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            return newObj;
        }
        EditorGUILayout.EndHorizontal();
        return null;
    }

    private void CreateList(ReorderableList listM, string listName)
    {
        GUILayout.Space(spaceAmount);
        GUILayout.Label(listName, EditorStyles.boldLabel);
        serializedObject.Update();
        listM.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    void Clapperboard()
    {
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Clapperboard shelve", EditorStyles.boldLabel);
        #region clapperboard
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Clapperboard", GUILayout.Width(width));
        EditorGUI.BeginChangeCheck();
        GameObject clapperboard = (GameObject)EditorGUILayout.ObjectField(dat.clapperboard, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Clapperboard");
            dat.clapperboard = clapperboard;
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        #region clapperboardPoint
        EditorGUI.BeginChangeCheck();
        Vector3 clapperboardPoint = EditorGUILayout.Vector3Field("ClapperboardPoint", dat.clapperboardPoint);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "ClapperboardPoint");
            dat.clapperboardPoint = clapperboardPoint;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region pows
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Pows", GUILayout.Width(width));
        EditorGUI.BeginChangeCheck();
        GameObject pows = (GameObject)EditorGUILayout.ObjectField(dat.pows, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Pows");
            dat.pows = pows;
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
    }

    void Snowball()
    {
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Snowball shelve", EditorStyles.boldLabel);
        #region canvas
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Canvas", GUILayout.Width(130));
        EditorGUI.BeginChangeCheck();
        Canvas canvas = (Canvas)EditorGUILayout.ObjectField(dat.canvas, typeof(Canvas), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Canvas");
            dat.canvas = canvas;
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        #region ballsSilhouette
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 80;
        GUILayout.Label("BallsSilhouette", GUILayout.Width(130));
        EditorGUI.BeginChangeCheck();
        GameObject ballSilhouette = (GameObject)EditorGUILayout.ObjectField(dat.ballsSilhouette, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "BallsSilhouette");
            dat.ballsSilhouette = ballSilhouette;
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        #region snowballCanvas
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("SnowballCanvas", GUILayout.Width(130));
        EditorGUI.BeginChangeCheck();
        Canvas snowballCanvas = (Canvas)EditorGUILayout.ObjectField(dat.snowballCanvas, typeof(Canvas), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "SnowballCanvas");
            dat.snowballCanvas = snowballCanvas;
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        #region snowballManager
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 80;
        GUILayout.Label("SnowballManager", GUILayout.Width(130));
        EditorGUI.BeginChangeCheck();
        GameObject snowballManager = (GameObject)EditorGUILayout.ObjectField(dat.snowballManager, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "SnowballManager");
            dat.snowballManager = snowballManager;
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
        #region snowflakes
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 80;
        GUILayout.Label("Snowflakes", GUILayout.Width(130));
        EditorGUI.BeginChangeCheck();
        GameObject snowflakes = (GameObject)EditorGUILayout.ObjectField(dat.snowFlakes, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Snowflakes");
            dat.snowFlakes = snowflakes;
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
    }

    void Garden()
    {
        EditorGUI.BeginChangeCheck();
        bool setThirdCamera = EditorGUILayout.Toggle("setThirdCamera", dat.setThirdCamera);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "setThirdCamera");
            dat.setThirdCamera = setThirdCamera;
            EditorUtility.SetDirty(target);
        }
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Watering can shelve", EditorStyles.boldLabel);
        #region watering can
  
        EditorGUI.BeginChangeCheck();
        GameObject fallDecoration = (GameObject)EditorGUILayout.ObjectField("Watering can", dat.wateringCan, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Watering can");
            dat.wateringCan = fallDecoration;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region sphereScale
        EditorGUI.BeginChangeCheck();
        Vector3 sphereScale = EditorGUILayout.Vector3Field("sphereScale", dat.sphereScale);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "sphereScale");
            dat.sphereScale = sphereScale;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region bag
        EditorGUI.BeginChangeCheck();
        GameObject bagWithSeeds = (GameObject)EditorGUILayout.ObjectField("Bag with seeds", dat.bagWithSeeds, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Bag");
            dat.bagWithSeeds = bagWithSeeds;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region pointToAppear
        EditorGUI.BeginChangeCheck();
        Vector3 pointToAppear = EditorGUILayout.Vector3Field("PointToAppear", dat.pointToAppear);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "pointToAppear");
            dat.pointToAppear = pointToAppear;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region pointToAppearW
        EditorGUI.BeginChangeCheck();
        Vector3 pointToAppearW = EditorGUILayout.Vector3Field("pointToAppearW", dat.pointToAppearW);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "pointToAppearW");
            dat.pointToAppearW = pointToAppearW;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region flowers
        EditorGUI.BeginChangeCheck();
        GameObject flowers = (GameObject)EditorGUILayout.ObjectField("Flowers", dat.flowers, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Flowers");
            dat.flowers = flowers;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region seed
        EditorGUI.BeginChangeCheck();
        GameObject seed = (GameObject)EditorGUILayout.ObjectField("Seed", dat.seed, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Seed");
            dat.seed = seed;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region seedAmount
        EditorGUI.BeginChangeCheck();
        int seedAmount = EditorGUILayout.IntField("seedAmount", dat.maxSeedAmount);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "seedAmount");
            dat.maxSeedAmount = seedAmount;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region wateringCanAmount
        EditorGUI.BeginChangeCheck();
        int amountToGrow = EditorGUILayout.IntField("wateringCanAmount", dat.amountToGrow);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "wateringCanAmount");
            dat.amountToGrow = amountToGrow;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region wateringCanSize
        EditorGUI.BeginChangeCheck();
        Vector3 wateringCanSize = EditorGUILayout.Vector3Field("wateringCanSize", dat.wateringCanSize);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "wateringCanSize");
            dat.wateringCanSize = wateringCanSize;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region bagWithSeedsSize
        EditorGUI.BeginChangeCheck();
        Vector3 bagWithSeedsSize = EditorGUILayout.Vector3Field("bagWithSeedsSize", dat.bagWithSeedsSize);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "bagWithSeedsSize");
            dat.bagWithSeedsSize = bagWithSeedsSize;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region timeToCreateBag
        EditorGUI.BeginChangeCheck();
        float timeToCreateBag = EditorGUILayout.FloatField("timeToCreateBag", dat.timeToCreateBag);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "timeToCreateBag");
            dat.timeToCreateBag = timeToCreateBag;
            EditorUtility.SetDirty(target);
        }
        #endregion


    }

    void Marshmallow()
    {
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Marshmallow shelve", EditorStyles.boldLabel);

        #region nut
        EditorGUI.BeginChangeCheck();
        GameObject nut = (GameObject)EditorGUILayout.ObjectField("Marshmallow shelve", dat.nut, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Nut");
            dat.nut = nut;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region nutRadius
        float radius = EditorGUILayout.Slider("Nut Radius", dat.nutRadius, dat.nutRadiusMin, dat.nutRadiusMax); 
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Nut Radius");
            dat.nutRadius = radius;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region nutHeight
        EditorGUI.BeginChangeCheck();
        float height = EditorGUILayout.Slider("Nut height", dat.nutHeight, dat.nutHeightMin, dat.nutHeightMax);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Nut height");
            dat.nutHeight = height;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region marshmallowOffset
        EditorGUI.BeginChangeCheck();
        float offset = EditorGUILayout.Slider("Marshmallow Offset", dat.marshmallowOffset, dat.marshmallowOffsetMin, dat.marshmallowOffsetMax);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Marshmallow Offset");
            dat.marshmallowOffset = offset;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region marshmallow
        EditorGUI.BeginChangeCheck();
        GameObject marshmallow = (GameObject)EditorGUILayout.ObjectField("Marshmallow", dat.marshmallow, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Marshmallow");
            dat.marshmallow = marshmallow;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region nutBar
        EditorGUI.BeginChangeCheck();
        GameObject nutBar = (GameObject)EditorGUILayout.ObjectField("Nutbar", dat.nutBar, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Nutbar");
            dat.nutBar = nutBar;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region nutpoint
        EditorGUI.BeginChangeCheck();
        Vector3 nutPoint = EditorGUILayout.Vector3Field("NutPoint", dat.nutpoint);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "NutPoint");
            dat.nutpoint = nutPoint;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region roundOffset
        RoundOffset();
        #endregion
    }

    void RoundOffset()
    {
        EditorGUI.BeginChangeCheck();
        Vector3 roundOffset = EditorGUILayout.Vector3Field("roundOffset", dat.roundOffset);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "roundOffset");
            dat.roundOffset = roundOffset;
            EditorUtility.SetDirty(target);
        }
    }

    void CreamBig()
    {
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Cream", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Many cream layers", GUILayout.Width(130));
        EditorGUI.BeginChangeCheck();
        bool big = EditorGUILayout.Toggle(dat.manyCakeLayers);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "CreamBig");
            dat.manyCakeLayers = big;
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();
        CreateList(listCakeCream, "Cake cream");

    }

    void Sprikling()
    {
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Sprikling shelve", EditorStyles.boldLabel);
        #region fallDecoration
        EditorGUI.BeginChangeCheck();
        GameObject fallDecoration = (GameObject)EditorGUILayout.ObjectField("FallDecoration", dat.fallDecorations, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "FallDecoration");
            dat.fallDecorations = fallDecoration;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region bag
        EditorGUI.BeginChangeCheck();
        GameObject bag = (GameObject)EditorGUILayout.ObjectField("Bag", dat.bag, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Bag");
            dat.bag = bag;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region bagPoint
    
        EditorGUI.BeginChangeCheck();
        Vector3 bagPoint = EditorGUILayout.Vector3Field("BagPoint", dat.bagPoint);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "BagPoint");
            dat.bagPoint = bagPoint;
            EditorUtility.SetDirty(target);
        }
        #endregion
       
    }

    void Decorations()
    {
        CreateList(listDecor, "Decor");
        CreateList(listSilhouettes, "Silhouttes");
        CreateList(listSplit, "Split");
      


    }
    void Fruits()
    {
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Fruits shelve", EditorStyles.boldLabel);
        #region squizeer
        EditorGUI.BeginChangeCheck();
        GameObject squizeer = (GameObject)EditorGUILayout.ObjectField("Squeezer", dat.squeezer, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "squeezer");
            dat.squeezer = squizeer;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region squizeerRadius      
        EditorGUI.BeginChangeCheck();
        float radius = EditorGUILayout.Slider("Squeezer Radius", dat.squeezerRadius, dat.squeezerRadiusMin, dat.squeezerRadiusMax);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Squeezer Radius");
            dat.squeezerRadius = radius;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region berriesRadius      
        EditorGUI.BeginChangeCheck();
        float berradius = EditorGUILayout.Slider("Berries Radius", dat.berriesRadius, 0, 10);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Berries Radius");
            dat.berriesRadius = berradius;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region squizeerHeight
        EditorGUI.BeginChangeCheck();
        float height = EditorGUILayout.Slider("Squizeer height", dat.squeezerHeight, dat.squeezerHeightMin, dat.squeezerHeightMax);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Squizeer height");
            dat.squeezerHeight = height;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region berryOffset
        EditorGUI.BeginChangeCheck();
        float offset = EditorGUILayout.Slider("Squizeer Offset", dat.berryOffset, dat.berryOffsetMin, dat.berryOffsetMax);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Berry Offset");
            dat.berryOffset = offset;
            EditorUtility.SetDirty(target);
        }
        #endregion
        CreateList(listBerries, "Berries");
        #region extrudepoint
        EditorGUI.BeginChangeCheck();
        Vector3 extrudepoint = EditorGUILayout.Vector3Field("ExtrudePoint", dat.extrudepoint);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "ExtrudePoint");
            dat.extrudepoint = extrudepoint;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region waitBerry
        EditorGUI.BeginChangeCheck();
        float waitBerry = EditorGUILayout.FloatField("waitBerry", dat.waitBerry);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "waitBerry");
            dat.waitBerry = waitBerry;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region berryScale
        EditorGUI.BeginChangeCheck();
        Vector3 scale = EditorGUILayout.Vector3Field("Berry Scale", dat.berryScale);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Berry Scale");
            dat.berryScale = scale;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region berryOrientation
        EditorGUI.BeginChangeCheck();
        Vector3 vec = new Vector3(dat.berryOrientation.x, dat.berryOrientation.y, dat.berryOrientation.z);
        EditorGUILayout.Vector3Field("BerryOrientation", vec);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "BerryOrientation");
            dat.berryOrientation = Quaternion.Euler(vec);
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region berrylookOnCake
        EditorGUI.BeginChangeCheck();
        bool look = EditorGUILayout.Toggle("Look on cake", dat.berryLookOnCake);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Look on cake");
            dat.berryLookOnCake = look;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region spaces
        EditorGUI.BeginChangeCheck();
        bool space = EditorGUILayout.Toggle("Space", dat.spaces);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Space");
            dat.spaces = space;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region berryDensity
        EditorGUI.BeginChangeCheck();
        float density = EditorGUILayout.FloatField("BerryDensity", dat.berryDensity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "BerryDensity");
            dat.berryDensity = density;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region cap
        EditorGUI.BeginChangeCheck();
        Vector3 capOffset = EditorGUILayout.Vector3Field("capOffset", dat.capOffset);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "capOffset");
            dat.capOffset = capOffset;
            EditorUtility.SetDirty(target);
        }
        #endregion
    }

    private void Borders()
    {
        #region carrotBorderRightAndLeft
        EditorGUI.BeginChangeCheck();
        Vector2 rightAndLeft = EditorGUILayout.Vector2Field("rightAndLeft", new Vector2(dat.rightAndLeft.x, dat.rightAndLeft.y));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "rightAndLeft");
            dat.rightAndLeft = rightAndLeft;
            EditorUtility.SetDirty(target);
        }
        #endregion
        #region carrotBorderTopAndBottom
        EditorGUI.BeginChangeCheck();
        Vector2 topAndBottom = EditorGUILayout.Vector2Field("topAndbottom", new Vector2(dat.topAndbottom.x, dat.topAndbottom.y));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "topAndbottom");
            dat.topAndbottom = topAndBottom;
            EditorUtility.SetDirty(target);
        }
        #endregion
    }

    void Paint()
    {
        CreateList(listPaint, "Paint");
    }

    void Cloud()
    {
        #region cloud
        GUILayout.Space(spaceAmount);
        GUILayout.Label("Cloud", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Cloud", GUILayout.Width(width));
        EditorGUI.BeginChangeCheck();
        GameObject cloudObject = (GameObject)EditorGUILayout.ObjectField(dat.cloud, typeof(GameObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Cloud");
            dat.cloud = cloudObject;
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();
        #endregion
    }

    void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("obj"),
            GUIContent.none);
    }
    void DrawHeader(Rect rect)
    {
        EditorGUI.LabelField(rect, "Shelves");
    }
    private void Point()
    {
        EditorGUI.BeginChangeCheck();
        Vector3 point = EditorGUILayout.Vector3Field("Point", dat.pointOffset);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change plate object");
            dat.pointOffset = point;
            EditorUtility.SetDirty(target);
        }
    }
}

