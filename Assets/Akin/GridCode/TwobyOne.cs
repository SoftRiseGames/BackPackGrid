using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.IO;
public class TwoByOne : MonoBehaviour, IInventoryObject, IRotatable, IPowerItem
{
    public bool OnDownMiddle { get; private set; }
    public bool OnUpMiddle { get; private set; }
    public bool onLeftMiddle { get; private set; }
    public bool onRightMiddle { get; private set; }

    public bool OnUpRight { get; private set; }

    public bool OnDownRight { get; private set; }

    public bool OnUpLeft { get; private set; }

    public bool OnDownLeft { get; private set; }



    public bool OnDownNext { get; private set; }
    public bool OnUpNext { get; private set; }
    public bool onLeftNext { get; private set; }
    public bool onRightNext { get; private set; }


    public bool OnDownObjectDedect { get; private set; }
    public bool OnUpObjectDedect { get; private set; }
    public bool onLeftObjectDedect { get; private set; }
    public bool onRightObjectDedect { get; private set; }
    public bool gridEnter { get; set; }
    public BaseItem BaseItemObj { get; set; }
    
    public List<GameObject> AddedMaterialsChecker { get;} = new List<GameObject>();

    [SerializeField] BaseItem BaseItem;
        
    IInventoryObject inventoryObject;
    Vector2 objectPosition;
    Vector2 cellCenterPosition;
    public int SidePosXValue;
    public int SidePosYValue;
    public Grid gridBasement;
    public GridRaycast gridInput;
    public GameObject handledObject;
    public GameObject grid;
    bool isDragging;
    public float collisionRadius = 0.25f;
    public Vector2 UpandBottomboxSize;
    public Vector2 SideBoxSize;
    public List<Vector2> VectorsMiddle;
    public List<Vector2> VectorsOutside;
    public List<Vector2> VectorsRight;
    public List<Vector2> VectorsLeft;
    public LayerMask Layer;
    public LayerMask LayerNextCheck;
    public LayerMask LayerObjectDedect;
    float pivotOffsetX = 0;
    float pivotOffsetY = 0;
    public Vector2 StartPosition;
    bool isHandle;
    public bool CanEnterPosition { get; set; }
    int successfulMerges = 0;

    float dragThreshold = 0.11f;

    float lastlocationX = 0;
    float lastlocationY = 0;

    Vector3 mouseDelta = new Vector3();
    public bool isAdded { get; set; }
    

    public HandledCards CardHandleDataList;
    [SerializeField] List<BoxCollider2D> CollideDedectors;
    private HashSet<BaseItem> objectsThatAdded = new HashSet<BaseItem>();
    private void Start()
    {
        GridIntegration();
        ScaleObject();
        StartPosition = transform.position;
        isDragging = false;
        CardHandleDataList = GameObject.Find("HandledCardManager").GetComponent<HandledCards>();
        BaseItemObj = BaseItem;
        CanEnterPosition = true;
        
    }

    private void Update()
    {
        MouseDragControl();
        OutOfGrid();
        GridEnterBoolCheck();
        Ray();
        AddList();
        mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        if (isHandle)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;

                gridBasement.GetComponent<GridSystem>().Inv = null;
                gameObject.layer = LayerMask.NameToLayer("HandleObjectLocked");

                if (gridEnter)
                {
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                }


                if (!gridEnter)
                {
                    transform.position = StartPosition;
                    CanEnterPosition = true;
                }

                isHandle = false;
            }


            if ((onRightNext || onLeftNext) && lastlocationX == 0)
            {
                lastlocationX = gameObject.transform.position.x;
            }
            if ((OnUpNext || OnDownNext) && lastlocationY == 0)
            {
                lastlocationY = gameObject.transform.position.y;
            }

        }
    }
  
    public void ButtonSkipEvent()
    {
        AddList();
        CombineObject();
    }
    public void Consume()
    {
        throw new System.NotImplementedException();
    }

    public void GridIntegration()
    {
        gridBasement = GameObject.Find("Grid").GetComponent<Grid>();
        gridInput = GameObject.Find("Grid").GetComponent<GridRaycast>();
        handledObject = gameObject;
    }

    void ScaleObjectRechange()
    {
        float offsetDataCollector = pivotOffsetX;
        pivotOffsetX = pivotOffsetY;
        pivotOffsetY = offsetDataCollector;
    }


    void MouseDragControl()
    {
        if (isHandle)
        {

            if (mouseDelta.magnitude > dragThreshold && !gridEnter)
            {
                isDragging = true;
            }

            if (mouseDelta.magnitude > dragThreshold && gridEnter)
            {
                CanEnterPosition = false;
            }

            if (isDragging)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;
                transform.position = mousePosition;
            }
        }

    }
    void ScaleObject()
    {

        if (handledObject.transform.localScale.x / 2 == 1)
        {
            pivotOffsetX = .5f * handledObject.GetComponent<TwoByOne>().SidePosXValue;
        }
        if (handledObject.transform.localScale.y / 2 == 1)
        {
            pivotOffsetY = .5f * handledObject.GetComponent<TwoByOne>().SidePosYValue;
        }
    }

    public void RegisterYourself()
    {
        isDragging = false;

        // Grid'den seçilen pozisyonu al
        Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = gridBasement.WorldToCell(selectedPosition);

        if (!gridEnter || handledObject == null)
            return;

        inventoryObject = handledObject.GetComponent<IInventoryObject>();
        cellCenterPosition = gridBasement.GetCellCenterWorld(cellPosition);

        bool snapped = false;

        if (CanEnterPosition)
        {
            // Direkt olarak grid hücresine snaple (pivot offset ile)
            Vector3 newPosition = cellCenterPosition;
            newPosition.x -= pivotOffsetX;
            newPosition.y -= pivotOffsetY;

            handledObject.transform.position = newPosition;
            CanEnterPosition = false;
            snapped = true;
        }
        else
        {
            Vector3 currentPosition = handledObject.transform.position;
            Vector3 newPosition = currentPosition;

            // Snap X
            if ((inventoryObject.onRightNext && !onRightObjectDedect && cellPosition.x >= currentPosition.x) ||
                (inventoryObject.onLeftNext && !onLeftObjectDedect && cellPosition.x < currentPosition.x))
            {
                newPosition.x = cellCenterPosition.x - pivotOffsetX;
            }

            // Snap Y
            if ((inventoryObject.OnUpNext && !OnUpObjectDedect && cellPosition.y >= currentPosition.y) ||
                (inventoryObject.OnDownNext && !OnDownObjectDedect && cellPosition.y < currentPosition.y))
            {
                newPosition.y = cellCenterPosition.y - pivotOffsetY;
            }

            if (newPosition != currentPosition)
            {
                handledObject.transform.position = newPosition;
                snapped = true;
            }
        }

        if (!snapped)
        {
            // Snap yapılamadıysa istenirse burada log/logic eklenebilir
            Debug.Log("Snap yapılamadı. Engel veya geçersiz pozisyon.");
        }
    }

    void AddList()
    {
        if (gridEnter)
        {
            foreach (GameObject i in CardHandleDataList.HandledObjects)
            {
                if (i == gameObject.GetComponent<IInventoryObject>().BaseItemObj)// sabah burayı kontrol et
                {
                    isAdded = true;
                }
            }

            if (isAdded)
            {
                return;
            }
            else
            {
                CardHandleDataList.HandledObjects.Add(gameObject);
                isAdded = true;
            }

        }

    }
    /*
    void ListDeleter()
    {
        isAdded = false;
        CardHandleDataList.HandledObjects.Remove(BaseItemObj);

        foreach (GameObject i in AddedMaterialsChecker)
        {
            i.GetComponent<IInventoryObject>().isAdded = false;
            CardHandleDataList.HandledObjects.Remove(i.GetComponent<IInventoryObject>().BaseItemObj);
        }
    }
    */
    void CombineObject()
    {
        successfulMerges = 0; // Her çağrıldığında sayaç sıfırlanır

        if (AddedMaterialsChecker != null)
        {
            foreach (GameObject i in AddedMaterialsChecker)
            {
                for (int MergedItems = 0; MergedItems < BaseItemObj.MergedItems.Count; MergedItems++)
                {
                    if (i.GetComponent<IInventoryObject>().BaseItemObj == BaseItemObj.MergedItems[MergedItems])
                    {
                        successfulMerges++; 
                    }
                }
            }
        }
    }

    void TryAddUpgradedItem()
    {
        
        if (successfulMerges >= BaseItem.MergedItems.Count && gridEnter)
        {
            if (!objectsThatAdded.Contains(BaseItemObj))
            {
                //CardHandleDataList.HandledObjects.Add(BaseItem.RootMergeItem);
                objectsThatAdded.Add(BaseItemObj); 
            }
        }
    }
    void OutOfGrid()
    {

     
    }
    void GridEnterBoolCheck()
    {
        if (OnUpMiddle && OnDownMiddle && onRightMiddle && onLeftMiddle && OnUpRight && OnUpLeft && OnDownRight && OnDownLeft)
        {
            gridEnter = true;
        }
        else
        {
            gridEnter = false;
        }

    }

    public void RotateLeft()
    {
        // Rotate Object
        gameObject.transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, 90));

        SwapElements(VectorsMiddle, 0, 2);
        SwapElements(VectorsMiddle, 1, 3);

        SwapElements(VectorsOutside, 0, 2);
        SwapElements(VectorsOutside, 1, 3);

        SwapElements(VectorsRight, 0, 1);
        SwapElements(VectorsLeft, 0, 1);

        SwapBoxSizes();

        ScaleObjectRechange();

        RegisterYourself();
        gridEnter = false;

    }

    

    public void RotateRight()
    {
        throw new System.NotImplementedException();
    }

    public int SidePosX()
    {
        return SidePosXValue;
    }

    public int SidePosY()
    {
        return SidePosYValue;
    }

    public void SwapElements(List<Vector2> VectorList, int VectorIndex1, int VectorIndex2)
    {
        if (VectorIndex1 >= 0 && VectorIndex1 < VectorList.Count && VectorIndex2 >= 0 && VectorIndex2 < VectorList.Count)
        {
            Vector2 temp = VectorList[VectorIndex1];
            VectorList[VectorIndex1] = new Vector2(VectorList[VectorIndex1].y, VectorList[VectorIndex1].x);

            Vector2 temp2 = VectorList[VectorIndex2];
            VectorList[VectorIndex2] = new Vector2(VectorList[VectorIndex2].y, VectorList[VectorIndex2].x);

            (VectorList[VectorIndex1], VectorList[VectorIndex2]) = (VectorList[VectorIndex2], VectorList[VectorIndex1]);
        }
    }

    void Ray()
    {
        OnUpMiddle = Physics2D.OverlapCircle((Vector2)transform.position + VectorsMiddle[0], collisionRadius, Layer);
        OnDownMiddle = Physics2D.OverlapCircle((Vector2)transform.position + VectorsMiddle[1], collisionRadius, Layer);
        onRightMiddle = Physics2D.OverlapCircle((Vector2)transform.position + VectorsMiddle[2], collisionRadius, Layer);
        onLeftMiddle = Physics2D.OverlapCircle((Vector2)transform.position + VectorsMiddle[3], collisionRadius, Layer);

        OnUpRight = Physics2D.OverlapCircle((Vector2)transform.position + VectorsRight[0], collisionRadius, Layer);
        OnDownRight = Physics2D.OverlapCircle((Vector2)transform.position + VectorsRight[1], collisionRadius, Layer);

        OnUpLeft = Physics2D.OverlapCircle((Vector2)transform.position + VectorsLeft[0], collisionRadius, Layer);
        OnDownLeft = Physics2D.OverlapCircle((Vector2)transform.position + VectorsLeft[1], collisionRadius, Layer);

        OnUpNext = Physics2D.OverlapBox((Vector2)transform.position + VectorsOutside[0], UpandBottomboxSize, 0, LayerNextCheck);
        OnDownNext = Physics2D.OverlapBox((Vector2)transform.position + VectorsOutside[1], UpandBottomboxSize, 0, LayerNextCheck);
        onRightNext = Physics2D.OverlapBox((Vector2)transform.position + VectorsOutside[2], SideBoxSize, 0, LayerNextCheck);
        onLeftNext = Physics2D.OverlapBox((Vector2)transform.position + VectorsOutside[3], SideBoxSize, 0, LayerNextCheck);

        OnUpObjectDedect = Physics2D.OverlapBox((Vector2)transform.position + VectorsOutside[0], UpandBottomboxSize, 0, LayerObjectDedect);
        OnDownObjectDedect = Physics2D.OverlapBox((Vector2)transform.position + VectorsOutside[1], UpandBottomboxSize, 0, LayerObjectDedect);
        onRightObjectDedect = Physics2D.OverlapBox((Vector2)transform.position + VectorsOutside[2], SideBoxSize, 0, LayerObjectDedect);
        onLeftObjectDedect = Physics2D.OverlapBox((Vector2)transform.position + VectorsOutside[3], SideBoxSize, 0, LayerObjectDedect);


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere((Vector2)transform.position + VectorsMiddle[1], collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + VectorsMiddle[0], collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + VectorsMiddle[2], collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + VectorsMiddle[3], collisionRadius);

        Gizmos.DrawWireSphere((Vector2)transform.position + VectorsRight[0], collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + VectorsRight[1], collisionRadius);

        Gizmos.DrawWireSphere((Vector2)transform.position + VectorsLeft[0], collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + VectorsLeft[1], collisionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + VectorsOutside[1], UpandBottomboxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + VectorsOutside[0], UpandBottomboxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + VectorsOutside[2], SideBoxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + VectorsOutside[3], SideBoxSize);
    }

    public void ClickObject()
    {
        throw new NotImplementedException();
    }

    void SwapBoxSizes()
    {
        Vector2 temp = SideBoxSize;
        SideBoxSize.x = UpandBottomboxSize.y;
        SideBoxSize.y = UpandBottomboxSize.x;

        UpandBottomboxSize.x = temp.y;
        UpandBottomboxSize.y = temp.x;

    }

    public void MoveObjectStarting()
    {
        gridBasement.GetComponent<GridSystem>().Inv = this;

        gameObject.layer = LayerMask.NameToLayer("HandleObjectPlacement");
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;


        isHandle = true;


    }

    public void PowerUpBuffs()
    {
        Debug.Log("ObjectPowering");
    }
}

