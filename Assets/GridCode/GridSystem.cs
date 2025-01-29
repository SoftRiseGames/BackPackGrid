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
        else {
            Debug.Log("This object is not rotatable");
        }
        if(Inv is IHelper)
        {
            /*
            int SidePosx = ((IHelper)Inv).SidePosX();
            int SidePosY = ((IHelper)Inv).SidePosY();

            PivotDistanceX = SidePosx*0.5f;
            PivotDistancey= SidePosY*0.5f;

            Debug.Log(PivotDistanceX);
            Debug.Log(PivotDistancey);
            */
        }
        //handledObject.transform.position = grid.GetCellCenterWorld(cellPosition);

    }

    private void Update()
    {
        Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = grid.WorldToCell(selectedPosition);

        if (handledObject.GetComponent<IInventoryObject>().onRight && !handledObject.GetComponent<IInventoryObject>().onLeft && cellPosition.x < Mathf.Round(handledObject.transform.position.x))
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x, handledObject.transform.position.y);

        else if (!handledObject.GetComponent<IInventoryObject>().onRight && handledObject.GetComponent<IInventoryObject>().onLeft && cellPosition.x >= Mathf.Round(handledObject.transform.position.x))
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x, handledObject.transform.position.y);

        else if (!handledObject.GetComponent<IInventoryObject>().onRight && !handledObject.GetComponent<IInventoryObject>().onLeft)
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x, handledObject.transform.position.y);

        else if(handledObject.GetComponent<IInventoryObject>().onLeft && handledObject.GetComponent<IInventoryObject>().onRightNext && cellPosition.x >= Mathf.Round(handledObject.transform.position.x))
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x, handledObject.transform.position.y);


        else if (handledObject.GetComponent<IInventoryObject>().onRight && handledObject.GetComponent<IInventoryObject>().onLeftNext && cellPosition.x < Mathf.Round(handledObject.transform.position.x))
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x, handledObject.transform.position.y);


        // Yukarý ve aþaðý hareket kontrolü
        if (!handledObject.GetComponent<IInventoryObject>().OnUp && handledObject.GetComponent<IInventoryObject>().OnDown && cellPosition.y < handledObject.transform.position.y)
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x, grid.GetCellCenterWorld(cellPosition).y);

        else if (handledObject.GetComponent<IInventoryObject>().OnUp && !handledObject.GetComponent<IInventoryObject>().OnDown && cellPosition.y >= handledObject.transform.position.y)
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y);

        else if (!handledObject.GetComponent<IInventoryObject>().OnUp && !handledObject.GetComponent<IInventoryObject>().OnDown)
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y);


        else if (handledObject.GetComponent<IInventoryObject>().OnDown && handledObject.GetComponent<IInventoryObject>().OnUpNext && cellPosition.y >= Mathf.Round(handledObject.transform.position.y))
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y);


        else if (handledObject.GetComponent<IInventoryObject>().OnUp && handledObject.GetComponent<IInventoryObject>().OnDownNext && cellPosition.y< Mathf.Round(handledObject.transform.position.y))
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y);



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
