using UnityEngine;
using UnityEngine.Events;
using System;

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

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset, UpOffset;
    public Vector2 bottomNextOffset, rightNextOffset, leftNextOffset, UpNextOffset;
    private Color debugCollisionColor = Color.red;
    public LayerMask LayerSide;
    public LayerMask layerUpDown;
    public LayerMask LayerNextCheck;
    private void Start()
    {
        GridIntegration();
    }
    private void Update()
    {
        Ray();
    }
    private void OnMouseDown()
    {
        Debug.Log(gridBasement.GetComponent<GridSystem>().Inv);
        gridBasement.GetComponent<GridSystem>().Inv = this;
        Debug.Log("clicked");
        Debug.Log(gridBasement.GetComponent<GridSystem>().Inv);

        isDragging = true;

    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Z eksenini sabit tut
            transform.position = mousePosition;
        }
    }


    void OnMouseUp()
    {
        isDragging = false;
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
    public void RegisterYourself()
    {
        Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = gridBasement.WorldToCell(selectedPosition);
        float pivotoffsetX = 0;
        float pivotoffsetY = 0;

        if (OnUp && OnDown && onRight && onLeft)
        {
           
       
            isDragging = false;
            // Pivot Offsets hesaplama
            if (handledObject.transform.localScale.x / 2 == 1)
            {
                Debug.Log("pivot");
                pivotoffsetX = .5f * handledObject.GetComponent<TwobyOne>().SidePosXValue;
            }
            if (handledObject.transform.localScale.y / 2 == 1)
            {
                pivotoffsetY = .5f * handledObject.GetComponent<TwobyOne>().SidePosYValue;
            }

            Debug.Log(pivotoffsetX);

         

            if (handledObject.GetComponent<IInventoryObject>().OnDownNext && !handledObject.GetComponent<IInventoryObject>().OnUpNext && cellPosition.y < handledObject.transform.position.y)
            {
                handledObject.transform.position = new Vector2(handledObject.transform.position.x, gridBasement.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            }


            else if (handledObject.GetComponent<IInventoryObject>().OnUpNext && !handledObject.GetComponent<IInventoryObject>().OnDownNext && cellPosition.y >= handledObject.transform.position.y)
            {
                handledObject.transform.position = new Vector2(handledObject.transform.position.x, gridBasement.GetCellCenterWorld(cellPosition).y + pivotoffsetY);
            }


            else if (handledObject.GetComponent<IInventoryObject>().OnUpNext && handledObject.GetComponent<IInventoryObject>().OnDownNext && cellPosition.y >= handledObject.transform.position.y + (pivotoffsetY * 2))
            {
                handledObject.transform.position = new Vector2(handledObject.transform.position.x, gridBasement.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            }

            else if (handledObject.GetComponent<IInventoryObject>().OnUpNext && handledObject.GetComponent<IInventoryObject>().OnDownNext && cellPosition.y < handledObject.transform.position.y - (pivotoffsetY * 2))
            {
                handledObject.transform.position = new Vector2(handledObject.transform.position.x, gridBasement.GetCellCenterWorld(cellPosition).y + pivotoffsetY);
            }

            Debug.Log(handledObject.GetComponent<IInventoryObject>().OnDownNext);
            Debug.Log(handledObject.GetComponent<IInventoryObject>().OnUpNext);


            if (!handledObject.GetComponent<IInventoryObject>().onRightNext && !handledObject.GetComponent<IInventoryObject>().onLeftNext && !handledObject.GetComponent<IInventoryObject>().OnDownNext && !handledObject.GetComponent<IInventoryObject>().OnUpNext && cellPosition.x < handledObject.transform.position.x)
            {
                handledObject.transform.position = new Vector2(gridBasement.GetCellCenterWorld(cellPosition).x - pivotoffsetX, gridBasement.GetCellCenterWorld(cellPosition).y);
            }

            else if (!handledObject.GetComponent<IInventoryObject>().onRightNext && !handledObject.GetComponent<IInventoryObject>().onLeftNext && !handledObject.GetComponent<IInventoryObject>().OnDownNext && !handledObject.GetComponent<IInventoryObject>().OnUpNext && cellPosition.x >= handledObject.transform.position.x)
            {
                handledObject.transform.position = new Vector2(gridBasement.GetCellCenterWorld(cellPosition).x + pivotoffsetX, gridBasement.GetCellCenterWorld(cellPosition).y);
            }



            else if (handledObject.GetComponent<IInventoryObject>().onRightNext && !handledObject.GetComponent<IInventoryObject>().onLeftNext && cellPosition.x >= handledObject.transform.position.x)
            {
                handledObject.transform.position = new Vector2(gridBasement.GetCellCenterWorld(cellPosition).x + pivotoffsetX, handledObject.transform.position.y);
            }
            else if (handledObject.GetComponent<IInventoryObject>().onLeftNext && !handledObject.GetComponent<IInventoryObject>().onRightNext && cellPosition.x < (handledObject.transform.position.x))
            {

                handledObject.transform.position = new Vector2(gridBasement.GetCellCenterWorld(cellPosition).x - pivotoffsetX, handledObject.transform.position.y);
            }

            else if (handledObject.GetComponent<IInventoryObject>().onLeftNext && handledObject.GetComponent<IInventoryObject>().onRightNext && cellPosition.x < (handledObject.transform.position.x))
            {

                handledObject.transform.position = new Vector2(gridBasement.GetCellCenterWorld(cellPosition).x - pivotoffsetX, handledObject.transform.position.y);
            }
            else if (handledObject.GetComponent<IInventoryObject>().onLeftNext && handledObject.GetComponent<IInventoryObject>().onRightNext && cellPosition.x >= (handledObject.transform.position.x))
            {

                handledObject.transform.position = new Vector2(gridBasement.GetCellCenterWorld(cellPosition).x - pivotoffsetX, handledObject.transform.position.y);
            }
        }


    }

    

    public void RotateLeft(Action<int> callback)
    {
        // Rotate Object
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



    void Ray()
    {
        OnUp = Physics2D.OverlapCircle((Vector2)transform.position + UpOffset, collisionRadius, layerUpDown);
        OnDown = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, layerUpDown);
        onRight = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, LayerSide);
        onLeft = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, LayerSide);


        OnUpNext = Physics2D.OverlapCircle((Vector2)transform.position + UpNextOffset, collisionRadius, LayerNextCheck);
        OnDownNext = Physics2D.OverlapCircle((Vector2)transform.position + bottomNextOffset, collisionRadius, LayerNextCheck);
        onRightNext = Physics2D.OverlapCircle((Vector2)transform.position + rightNextOffset, collisionRadius, LayerNextCheck);
        onLeftNext = Physics2D.OverlapCircle((Vector2)transform.position + leftNextOffset, collisionRadius, LayerNextCheck);


    }
  
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + UpOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomNextOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + UpNextOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightNextOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftNextOffset, collisionRadius);
    }

    public void ClickObject()
    {
        throw new NotImplementedException();
    }

  
}



public interface IRotatable
{
    void RotateLeft(Action<int> callback);
    void RotateRight();
}




public interface IInventoryObject
{
    void GridIntegration();
    void RegisterYourself();
    void Consume();

   public bool OnUp { get; }
   public bool OnDown { get; }
   public bool onRight { get; }
   public bool onLeft { get; }

    public bool OnDownNext { get;}
    public bool OnUpNext { get;}
    public bool onLeftNext { get;}
    public bool onRightNext { get;}
}
