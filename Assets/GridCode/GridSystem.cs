using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject handledObject;
    public Grid grid;
    public GridRaycast gridInput;
    float PivotDistanceX;
    float PivotDistancey;
    public IInventoryObject Inv;



    private Vector3 lastMousePosition;


    private void Start()
    {
        Inv = GameObject.Find("ObjectReference").GetComponent<IInventoryObject>();
        if (Inv is IRotatable)
        {
            ((IRotatable)Inv).RotateLeft(RegisterYourself);
        }
        else
        {
            Debug.Log("This object is not rotatable");
        }
        if (Inv is IHelper)
        {
          
        }
    }

    private void Update()
    {
        Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = grid.WorldToCell(selectedPosition);
        float pivotoffsetX = 0;
        float pivotoffsetY = 0;
        if (handledObject.transform.localScale.x / 2 == 1)
        {
            Debug.Log("pivot");
            pivotoffsetX = .5f * handledObject.GetComponent<TwobyOne>().SidePosXValue;
        }
        if(handledObject.transform.localScale.y/2 == 1)
        {
            pivotoffsetY = .5f * handledObject.GetComponent<TwobyOne>().SidePosYValue;
        }

        Debug.Log(pivotoffsetX);   

        if (handledObject.GetComponent<IInventoryObject>().onRight && !handledObject.GetComponent<IInventoryObject>().onLeft && cellPosition.x < Mathf.Round(handledObject.transform.position.x))
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x-pivotoffsetX, handledObject.transform.position.y);

        else if (!handledObject.GetComponent<IInventoryObject>().onRight && handledObject.GetComponent<IInventoryObject>().onLeft && cellPosition.x >= Mathf.Round(handledObject.transform.position.x))
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x+pivotoffsetX, handledObject.transform.position.y);

        else if (!handledObject.GetComponent<IInventoryObject>().onRight && !handledObject.GetComponent<IInventoryObject>().onLeft)
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x-pivotoffsetX, handledObject.transform.position.y);

        else if (handledObject.GetComponent<IInventoryObject>().onLeft && handledObject.GetComponent<IInventoryObject>().onRight && handledObject.GetComponent<IInventoryObject>().onRightNext && cellPosition.x >= Mathf.Round(handledObject.transform.position.x))
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x+pivotoffsetX, handledObject.transform.position.y);


        else if (handledObject.GetComponent<IInventoryObject>().onLeft && handledObject.GetComponent<IInventoryObject>().onRight && handledObject.GetComponent<IInventoryObject>().onLeftNext && cellPosition.x < Mathf.Round(handledObject.transform.position.x))
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x-pivotoffsetX, handledObject.transform.position.y);


        // Yukarý ve aþaðý hareket kontrolü
        if (handledObject.GetComponent<IInventoryObject>().OnUp && !handledObject.GetComponent<IInventoryObject>().OnDown && cellPosition.y < handledObject.transform.position.y)
        {
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            Debug.Log("a");
        }
           

        else if (!handledObject.GetComponent<IInventoryObject>().OnUp && handledObject.GetComponent<IInventoryObject>().OnDown && cellPosition.y >= handledObject.transform.position.y)
        {
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            Debug.Log("b");
        }

        else if (!handledObject.GetComponent<IInventoryObject>().OnUp && !handledObject.GetComponent<IInventoryObject>().OnDown)
        {
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            Debug.Log("c");
        }
                     

        else if((handledObject.GetComponent<IInventoryObject>().OnUp && handledObject.GetComponent<IInventoryObject>().OnDown && handledObject.GetComponent<IInventoryObject>().OnDownNext && cellPosition.y < handledObject.transform.position.y))
        {
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            Debug.Log("e");
        }
           

        else if ((handledObject.GetComponent<IInventoryObject>().OnUp && handledObject.GetComponent<IInventoryObject>().OnDown && handledObject.GetComponent<IInventoryObject>().OnUpNext && cellPosition.y >= handledObject.transform.position.y))
        {
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            Debug.Log("f");

        }
        
        Debug.Log(handledObject.GetComponent<IInventoryObject>().OnUp);
        Debug.Log(handledObject.GetComponent<IInventoryObject>().OnDown);
        
    }

    void farestTile()
    {

    }
    public void RegisterYourself(int ypos)
    {
        //matrixe kayıt
        Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = grid.WorldToCell(selectedPosition);
        handledObject.transform.position = grid.GetCellCenterWorld(cellPosition);

        Debug.Log(grid.GetCellCenterWorld(cellPosition));

    }
}