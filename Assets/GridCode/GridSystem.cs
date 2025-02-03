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

    private void OnMouseDrag()
    {
       
    }
    private void Update()
    {
        
        if (Inv != null )
        {

            Inv.RegisterYourself();

            /*
            Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
            Vector3Int cellPosition = grid.WorldToCell(selectedPosition);
            float pivotoffsetX = 0;
            float pivotoffsetY = 0;


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

            if (!handledObject.GetComponent<IInventoryObject>().onRightNext && !handledObject.GetComponent<IInventoryObject>().onLeftNext && !handledObject.GetComponent<IInventoryObject>().OnDownNext && !handledObject.GetComponent<IInventoryObject>().OnUpNext && cellPosition.x < handledObject.transform.position.x)
            {
                handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x - pivotoffsetX, grid.GetCellCenterWorld(cellPosition).y);
            }

            else if (!handledObject.GetComponent<IInventoryObject>().onRightNext && !handledObject.GetComponent<IInventoryObject>().onLeftNext && !handledObject.GetComponent<IInventoryObject>().OnDownNext && !handledObject.GetComponent<IInventoryObject>().OnUpNext && cellPosition.x >= handledObject.transform.position.x)
            {
                handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x + pivotoffsetX, grid.GetCellCenterWorld(cellPosition).y);
            }


            if (handledObject.GetComponent<IInventoryObject>().OnDownNext && !handledObject.GetComponent<IInventoryObject>().OnUpNext && cellPosition.y < handledObject.transform.position.y)
            {
                handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            }


            else if (handledObject.GetComponent<IInventoryObject>().OnUpNext && !handledObject.GetComponent<IInventoryObject>().OnDownNext && cellPosition.y >= handledObject.transform.position.y)
            {
                handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y + pivotoffsetY);
            }


            else if (handledObject.GetComponent<IInventoryObject>().OnUpNext && handledObject.GetComponent<IInventoryObject>().OnDownNext && cellPosition.y >= handledObject.transform.position.y + 1f)
            {
                handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y + pivotoffsetY);
            }

            else if (handledObject.GetComponent<IInventoryObject>().OnUpNext && handledObject.GetComponent<IInventoryObject>().OnDownNext && cellPosition.y < handledObject.transform.position.y - 1f)
            {
                handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            }

            Debug.Log(handledObject.GetComponent<IInventoryObject>().OnDownNext);
            Debug.Log(handledObject.GetComponent<IInventoryObject>().OnUpNext);



            if (handledObject.GetComponent<IInventoryObject>().onRightNext && cellPosition.x >= Mathf.Round(handledObject.transform.position.x))
            {
                handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x + pivotoffsetX, handledObject.transform.position.y);
            }
            else if (handledObject.GetComponent<IInventoryObject>().onLeftNext && cellPosition.x < Mathf.Round(handledObject.transform.position.x))
            {

                handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x - pivotoffsetX, handledObject.transform.position.y);
            }
             */

        }
        else
            return;

       

        }
        private void LateUpdate()
    {

    }
    void farestTile()
    {

    }

    public void RegisterYourself(int ypos)
    {
        // Matrixe kayýt-
    }
}