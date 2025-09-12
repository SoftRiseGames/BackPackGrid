using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DataClass
{
    public Grid gridBasement;
    public GridRaycast gridInput;
    public GameObject handledObject;
    public Vector2 cellCenterPosition;
    public float pivotOffsetX = 0;
    public float pivotOffsetY = 0;
    public IInventoryObject inventoryObject;
    public bool gridEnter;
}

[Serializable]
public class TwoByOneData : IInventoryObject/* IRotatable, IPowerItem*/
{
    bool isDragging;
    bool isHandle;
    public DataClass data = new DataClass();

    
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

    public List<GameObject> AddedMaterialsChecker { get; } = new List<GameObject>();

    public bool CanEnterPosition { get; set; }

    public List<GameObject> CollideList { get; set; } = new();
    public bool isAdded { get; set; }
    
    public void GridIntegration(GameObject gameObject)
    {
        data.gridBasement = GameObject.Find("Grid").GetComponent<Grid>();
        data.gridInput = GameObject.Find("Grid").GetComponent<GridRaycast>();
        data.handledObject = gameObject;

    }

    public void RegisterYourself(GameObject gameObject)
    {
        if (!gridEnter)
            CanEnterPosition = true;
        else
            CanEnterPosition = false;
        isDragging = false;
        Debug.Log("gridEnter " + gridEnter);
       
        data.gridInput = GameObject.Find("Grid").GetComponent<GridRaycast>();
        data.gridBasement = GameObject.Find("Grid").GetComponent<Grid>();
        data.handledObject = gameObject;

        Debug.Log(data.handledObject.name);
        Vector3 selectedPosition = data.gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = data.gridBasement.WorldToCell(selectedPosition);

        if (!gridEnter || data.handledObject == null)
        {
            return;
        }

        if (!gridEnter)
            CanEnterPosition = true;

        data.inventoryObject = data.handledObject.GetComponent<IInventoryObject>();
        data.cellCenterPosition = data.gridBasement.GetCellCenterWorld(cellPosition);

        bool snapped = false;

        if (CanEnterPosition)
        {
            Debug.Log("CanEnter");
            Vector3 newPosition = data.cellCenterPosition;

            if (data.cellCenterPosition.x >= gameObject.transform.position.x)
                newPosition.x -= data.pivotOffsetX;
            else
                newPosition.x += data.pivotOffsetX;

            if (data.cellCenterPosition.y >= gameObject.transform.position.y)
                newPosition.y -= data.pivotOffsetY;
            else
                newPosition.y += data.pivotOffsetY;


            data.handledObject.transform.position = newPosition;
            CanEnterPosition = false;
            snapped = true;
        }
        else
        {
            Vector3 currentPosition = data.handledObject.transform.position;
            Vector3 newPosition = currentPosition;
            Debug.Log("CanNotEnter");
            // Snap X
            if ((data.inventoryObject.onRightNext && !onRightObjectDedect && cellPosition.x >= currentPosition.x) ||
                (data.inventoryObject.onLeftNext && !onLeftObjectDedect && cellPosition.x < currentPosition.x))
            {
                newPosition.x = data.cellCenterPosition.x - data.pivotOffsetX;
            }

            // Snap Y
            if ((data.inventoryObject.OnUpNext && !OnUpObjectDedect && cellPosition.y >= currentPosition.y) ||
                (data.inventoryObject.OnDownNext && !OnDownObjectDedect && cellPosition.y < currentPosition.y))
            {
                newPosition.y = data.cellCenterPosition.y - data.pivotOffsetY;
            }

            if (newPosition != currentPosition)
            {
                data.handledObject.transform.position = newPosition;
                snapped = true;
            }
        }

        if (!snapped)
        {
            Debug.Log("Snap yapýlamadý. Engel veya geçersiz pozisyon.");
        }
      
    }

    public void Consume()
    {
        //throw new System.NotImplementedException();
    }
    public void MoveObjectStarting(GameObject gameObject)
    {
        Debug.Log("girdi");
        data.gridBasement = GameObject.Find("Grid").GetComponent<Grid>();
        Debug.Log(data.gridBasement.name);

        data.gridBasement.GetComponent<GridSystem>().Inv = gameObject.GetComponent<IObjectSetting>();

        gameObject.layer = LayerMask.NameToLayer("HandleObjectPlacement");
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;


        isHandle = true;
        
        

    }
    public void MoveObjectStopping(GameObject gameObject,Vector3 StartingPosition)
    {
        Debug.Log("Çýktý");
        data.gridBasement = null;
        gameObject.layer = LayerMask.NameToLayer("HandleObjectPlacement");
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        data.gridBasement = GameObject.Find("Grid").GetComponent<Grid>();
        data.gridBasement.GetComponent<GridSystem>().Inv = null;


        if (gridEnter)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }


        if (!gridEnter)
        {
            gameObject.transform.position = StartingPosition;
            CanEnterPosition = true;
        }
       
    }

    public void ObjectOutOfGrid(Transform transform, Vector3 StartPosition)
    {

        if (gridEnter && Input.GetMouseButtonDown(1))
        {
            transform.position = StartPosition;
        }
    }
}