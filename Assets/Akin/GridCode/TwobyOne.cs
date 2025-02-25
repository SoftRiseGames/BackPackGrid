using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class TwobyOne : MonoBehaviour, IInventoryObject, IRotatable, IHelper
{
    public int SidePosXValue;
    public int SidePosYValue;

    public Grid gridBasement;
    public GridRaycast gridInput;

    public GameObject handledObject;
    public GameObject grid;

    bool isDragging = false;

    public bool OnDown { get; private set; }
    public bool OnUp { get; private set; }
    public bool onLeft { get; private set; }
    public bool onRight { get; private set; }

    public bool OnDownNext { get; private set; }
    public bool OnUpNext { get; private set; }
    public bool onLeftNext { get; private set; }
    public bool onRightNext { get; private set; }


    public bool OnDownObjectDedect { get; private set; }
    public bool OnUpObjectDedect { get; private set; }
    public bool onLeftObjectDedect { get; private set; }
    public bool onRightObjectDedect { get; private set; }


    public float collisionRadius = 0.25f;
    public Vector2 UpandBottomboxSize;
    public Vector2 SideBoxSize;
    public List<Vector2> Vectors;
    public List<Vector2> VectorUp;
    private Color debugCollisionColor = Color.red;
    public LayerMask Layer;
    public LayerMask LayerNextCheck;
    public LayerMask LayerObjectDedect;

    float pivotOffsetX = 0;
    float pivotOffsetY = 0;

    bool gridEnter;

    public Vector2 StartPosition;

    private void Start()
    {
        GridIntegration();
        ScaleObject();
        StartPosition = transform.position;
    }

    private void Update()
    {
        Ray();
        GridEnterBoolCheck();

        if(gameObject.name == "ObjectReference")
        {
            Debug.Log("onUP "+OnUpObjectDedect);
            Debug.Log("OnDown "+OnDownObjectDedect);
            Debug.Log("OnRight "+onRightObjectDedect);
            Debug.Log("OnLeft "+onLeftObjectDedect);
        }
    }

    

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            transform.position = mousePosition;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        gridBasement.GetComponent<GridSystem>().Inv = null;
        gameObject.layer = LayerMask.NameToLayer("HandleObjectLocked");

        if (gridEnter)
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

        if (!gridEnter)
            transform.position = StartPosition;
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

    void ScaleObject()
    {
        if (handledObject.transform.localScale.x / 2 == 1)
        {
            Debug.Log("pivot");
            pivotOffsetX = .5f * handledObject.GetComponent<TwobyOne>().SidePosXValue;
        }
        if (handledObject.transform.localScale.y / 2 == 1)
        {
            pivotOffsetY = .5f * handledObject.GetComponent<TwobyOne>().SidePosYValue;
        }
    }

    public void RegisterYourself()
    {
        Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = gridBasement.WorldToCell(selectedPosition);

        if (gridEnter)
        {
            IInventoryObject inventoryObject = handledObject.GetComponent<IInventoryObject>();
            Vector2 objectPosition = handledObject.transform.position;
            Vector2 cellCenterPosition = gridBasement.GetCellCenterWorld(cellPosition);

            if (isDragging)
            {
                objectPosition.y = cellCenterPosition.y - pivotOffsetY;
                objectPosition.x = cellCenterPosition.x + pivotOffsetX;
            }
            isDragging = false;

            // Y ekseni hizalama
            if (inventoryObject.OnDownNext && !inventoryObject.OnUpNext && cellPosition.y < objectPosition.y  && !OnDownObjectDedect)
            {
                objectPosition.y = cellCenterPosition.y - pivotOffsetY;
                objectPosition.y = cellCenterPosition.y + pivotOffsetY;
            }


            else if (inventoryObject.OnUpNext && !inventoryObject.OnDownNext && cellPosition.y >= objectPosition.y && !OnUpObjectDedect )
                objectPosition.y = cellCenterPosition.y + pivotOffsetY;

            else if (inventoryObject.OnUpNext && inventoryObject.OnDownNext)
            {
                if (cellPosition.y >= objectPosition.y + (pivotOffsetY * 2) && !OnUpObjectDedect)
                    objectPosition.y = cellCenterPosition.y - pivotOffsetY;
                else if (cellPosition.y < objectPosition.y - (pivotOffsetY * 2) && !OnDownObjectDedect)
                    objectPosition.y = cellCenterPosition.y + pivotOffsetY;
            }

            // X ekseni hizalama
            if (!inventoryObject.onRightNext && !inventoryObject.onLeftNext &&
                !inventoryObject.OnDownNext && !inventoryObject.OnUpNext)
            {
                objectPosition.x = (cellPosition.x < Mathf.Round(objectPosition.x))
                    ? cellCenterPosition.x - pivotOffsetX
                    : cellCenterPosition.x + pivotOffsetX;
            }
            else if (inventoryObject.onRightNext && !inventoryObject.onLeftNext && cellPosition.x >= Mathf.Round(objectPosition.x) && !onRightObjectDedect)
                objectPosition.x = cellCenterPosition.x - pivotOffsetX;

            else if (inventoryObject.onLeftNext && !inventoryObject.onRightNext && cellPosition.x < Mathf.Round(objectPosition.x) && !onLeftObjectDedect)
                objectPosition.x = cellCenterPosition.x - pivotOffsetX;

            else if (inventoryObject.onLeftNext && inventoryObject.onRightNext && !onRightObjectDedect )
                objectPosition.x = cellCenterPosition.x - pivotOffsetX;

            float dragThreshold = 0.1f;
            bool canMoveHorizontally = !inventoryObject.onRightNext || !inventoryObject.onLeftNext;
            bool canMoveVertically = !inventoryObject.OnUpNext || !inventoryObject.OnDownNext;

            if (Input.GetAxis("Mouse X") > dragThreshold && !inventoryObject.onRightNext)
                isDragging = true;
            else if (Input.GetAxis("Mouse X") < -dragThreshold && !inventoryObject.onLeftNext)
                isDragging = true;
            else if (Input.GetAxis("Mouse Y") > dragThreshold && !inventoryObject.OnUpNext)
                isDragging = true;
            else if (Input.GetAxis("Mouse Y") < -dragThreshold && !inventoryObject.OnDownNext)
                isDragging = true;

            // Eğer obje yukarı ve aşağı hareket edemiyorsa, sağa veya sola hareket etmesini sağla
            if (!canMoveVertically && canMoveHorizontally)
            {
                if (Input.GetAxis("Mouse X") > dragThreshold)
                    isDragging = true;
                else if (Input.GetAxis("Mouse X") < -dragThreshold)
                    isDragging = true;
            }
            // Eğer obje sağa ve sola hareket edemiyorsa, yukarı veya aşağı hareket etmesini sağla
            else if (!canMoveHorizontally && canMoveVertically)
            {
                if (Input.GetAxis("Mouse Y") > dragThreshold)
                    isDragging = true;
                else if (Input.GetAxis("Mouse Y") < -dragThreshold)
                    isDragging = true;
            }

            handledObject.transform.position = objectPosition;
        }
    }

    void GridEnterBoolCheck()
    {
        if (OnUp && OnDown && onRight && onLeft)
            gridEnter = true;
        else
            gridEnter = false;
    }

    public void RotateLeft(Action<int> callback)
    {
        // Rotate Object
        gameObject.transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, 90));

        // Swap vectors (change orientation)
        SwapElements(Vectors, 0, 2);
        SwapElements(Vectors, 1, 3);

        SwapElements(VectorUp, 0, 2);
        SwapElements(VectorUp, 1, 3);

        // Swap the sizes of the boxes based on rotation
        SwapBoxSizes();

        // Recalculate scale and pivot offset
        ScaleObjectRechange();

        // Call Callback
        callback?.Invoke(3);
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
        OnUp = Physics2D.OverlapCircle((Vector2)transform.position + Vectors[0], collisionRadius,Layer);
        OnDown = Physics2D.OverlapCircle((Vector2)transform.position + Vectors[1], collisionRadius, Layer);
        onRight = Physics2D.OverlapCircle((Vector2)transform.position + Vectors[2], collisionRadius, Layer);
        onLeft = Physics2D.OverlapCircle((Vector2)transform.position + Vectors[3], collisionRadius, Layer);

        OnUpNext = Physics2D.OverlapBox((Vector2)transform.position + VectorUp[0], UpandBottomboxSize, 0, LayerNextCheck);
        OnDownNext = Physics2D.OverlapBox((Vector2)transform.position + VectorUp[1], UpandBottomboxSize, 0, LayerNextCheck);
        onRightNext = Physics2D.OverlapBox((Vector2)transform.position + VectorUp[2], SideBoxSize, 0, LayerNextCheck);
        onLeftNext = Physics2D.OverlapBox((Vector2)transform.position + VectorUp[3], SideBoxSize, 0, LayerNextCheck);

        OnUpObjectDedect = Physics2D.OverlapBox((Vector2)transform.position + VectorUp[0], UpandBottomboxSize, 0, LayerObjectDedect);
        OnDownObjectDedect = Physics2D.OverlapBox((Vector2)transform.position + VectorUp[1], UpandBottomboxSize, 0, LayerObjectDedect);
        onRightObjectDedect = Physics2D.OverlapBox((Vector2)transform.position + VectorUp[2], SideBoxSize, 0, LayerObjectDedect);
        onLeftObjectDedect = Physics2D.OverlapBox((Vector2)transform.position + VectorUp[3], SideBoxSize, 0, LayerObjectDedect);

        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere((Vector2)transform.position + Vectors[1], collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + Vectors[0], collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + Vectors[2], collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + Vectors[3], collisionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + VectorUp[1], UpandBottomboxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + VectorUp[0], UpandBottomboxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + VectorUp[2], SideBoxSize);
        Gizmos.DrawWireCube((Vector2)transform.position + VectorUp[3], SideBoxSize);
    }

    public void ClickObject()
    {
        throw new NotImplementedException();
    }

    void SwapBoxSizes()
    {
        // Swap SideBoxSize and UpandBottomboxSize based on rotation
        Vector2 temp = SideBoxSize;
        SideBoxSize.x = UpandBottomboxSize.y;
        SideBoxSize.y = UpandBottomboxSize.x;

        UpandBottomboxSize.x = temp.y;
        UpandBottomboxSize.y = temp.x;

    }


    public void MoveObject()
    {
        gridBasement.GetComponent<GridSystem>().Inv = this;
        isDragging = true;

        gameObject.layer = LayerMask.NameToLayer("HandleObjectPlacement");
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }


    public void MoveObjectStarting()
    {
        gridBasement.GetComponent<GridSystem>().Inv = this;
        isDragging = true;

        gameObject.layer = LayerMask.NameToLayer("HandleObjectPlacement");
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;

        Debug.Log("click");
    }
}
