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
        //handledObject.transform.position = grid.GetCellCenterWorld(cellPosition);

        /*        
         if(gridInput.Object.GetComponent<TileCode>().onLeft == false && gridInput.Object !=null)
             handledObject.transform.position = new Vector2(handledObject.transform.position.x + PivotDistanceX, handledObject.transform.position.y + PivotDistancey);
         else if (gridInput.Object.GetComponent<TileCode>().onRight == false && gridInput.Object != null)
             handledObject.transform.position = new Vector2(handledObject.transform.position.x - PivotDistanceX, handledObject.transform.position.y - PivotDistancey);

         else
             handledObject.transform.position = new Vector2(handledObject.transform.position.x - PivotDistanceX, handledObject.transform.position.y - PivotDistancey);
         */

        if (handledObject.GetComponent<IInventoryObject>().onRight && !handledObject.GetComponent<IInventoryObject>().onLeft && cellPosition.x<Mathf.Round(handledObject.transform.position.x))
            handledObject.transform.position = grid.GetCellCenterWorld(cellPosition);
       
        else if (!handledObject.GetComponent<IInventoryObject>().onRight && handledObject.GetComponent<IInventoryObject>().onLeft && cellPosition.x >= Mathf.Round(handledObject.transform.position.x))
            handledObject.transform.position = grid.GetCellCenterWorld(cellPosition);
        
        else if(!handledObject.GetComponent<IInventoryObject>().onRight && !handledObject.GetComponent<IInventoryObject>().onLeft)
        {
            handledObject.transform.position = grid.GetCellCenterWorld(cellPosition);
        }
        
          
        


        Debug.Log(cellPosition.x);
        Debug.Log(handledObject.transform.position.x);
        /*
        Debug.Log(cellPosition.x);
        Debug.Log(handledObject.transform.position.x);
        */

    }

    void farestTile()
    {

    }
    public void RegisterYourself(int ypos)
    {
        //matrixe kayýt
        Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = grid.WorldToCell(selectedPosition);
        handledObject.transform.position = grid.GetCellCenterWorld(cellPosition);

        Debug.Log(grid.GetCellCenterWorld(cellPosition));

    }
}
