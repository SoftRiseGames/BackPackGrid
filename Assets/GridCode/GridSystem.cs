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
    }
    private void LateUpdate()
    {

        Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = grid.WorldToCell(selectedPosition);
        float pivotoffsetX = 0;
        float pivotoffsetY = 0;
        bool isTwoSide = false;

        if (handledObject.GetComponent<IInventoryObject>().onRightNext && handledObject.GetComponent<IInventoryObject>().onLeftNext)
            isTwoSide = true;
        else
            isTwoSide = false;


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

        if (!handledObject.GetComponent<IInventoryObject>().onRightNext && !handledObject.GetComponent<IInventoryObject>().onLeftNext && !handledObject.GetComponent<IInventoryObject>().OnDownNext && !handledObject.GetComponent<IInventoryObject>().OnUpNext)
        {
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x - pivotoffsetX, grid.GetCellCenterWorld(cellPosition).y);
        }


        if (handledObject.GetComponent<IInventoryObject>().OnDownNext  && cellPosition.y < handledObject.transform.position.y)
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);

        else if (handledObject.GetComponent<IInventoryObject>().OnUpNext && cellPosition.y >= handledObject.transform.position.y)
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y + pivotoffsetY);

      

        if (handledObject.GetComponent<IInventoryObject>().onRightNext && cellPosition.x >= Mathf.Round(handledObject.transform.position.x))
        {
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x + pivotoffsetX, handledObject.transform.position.y);
        }
        else if (handledObject.GetComponent<IInventoryObject>().onLeftNext && cellPosition.x < Mathf.Round(handledObject.transform.position.x))
        {

            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x - pivotoffsetX, handledObject.transform.position.y);
        }
    }
    void farestTile()
    {

    }

    public void RegisterYourself(int ypos)
    {
        // Matrixe kayýt
        Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = grid.WorldToCell(selectedPosition);
        handledObject.transform.position = grid.GetCellCenterWorld(cellPosition);

        Debug.Log(grid.GetCellCenterWorld(cellPosition));
    }
}