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
        /*
        // Soldan ve sağdan temas kontrolü
        if (!handledObject.GetComponent<IInventoryObject>().onRight && !handledObject.GetComponent<IInventoryObject>().onLeft)
        {
            // Nesne sola ya da sağa yerleşmemişse, sadece kendisine ait alanına yerleşir
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x - pivotoffsetX, handledObject.transform.position.y);
        }
        else if (handledObject.GetComponent<IInventoryObject>().onRight && !handledObject.GetComponent<IInventoryObject>().onLeft && cellPosition.x < handledObject.transform.position.x)
        {
            // Eğer sağa yerleşmişse, sağa hareket etmesini sağla
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x - pivotoffsetX, handledObject.transform.position.y);
        }
        else if (!handledObject.GetComponent<IInventoryObject>().onRight && handledObject.GetComponent<IInventoryObject>().onLeft && cellPosition.x > handledObject.transform.position.x)
        {
            // Eğer sola yerleşmişse, sola hareket etmesini sağla
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x - pivotoffsetX, handledObject.transform.position.y);
        }

        // Yukarı ve aşağı hareket kontrolü
        if (handledObject.GetComponent<IInventoryObject>().OnUp && !handledObject.GetComponent<IInventoryObject>().OnDown)
        {
            // Yalnızca yukarı hareket etmesini sağla
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            Debug.Log("a");
        }
        else if (!handledObject.GetComponent<IInventoryObject>().OnUp && handledObject.GetComponent<IInventoryObject>().OnDown)
        {
            // Yalnızca aşağı hareket etmesini sağla
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y + pivotoffsetY);
            Debug.Log("b");
        }
        else if (!handledObject.GetComponent<IInventoryObject>().OnUp && !handledObject.GetComponent<IInventoryObject>().OnDown)
        {
            // Yukarı ve aşağı hareket etmiyorsa, sadece en yakın yeri al
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            Debug.Log("c");
        }

        // "Next" bool'ları için kontrol
        // Eğer herhangi bir 'Next' kontrolü yapılacaksa, ancak yalnızca diğer yönler (sağ-sol, yukarı-aşağı) temas etmiyorsa hareket etsin
        else if (handledObject.GetComponent<IInventoryObject>().OnUp && handledObject.GetComponent<IInventoryObject>().OnDown && handledObject.GetComponent<IInventoryObject>().OnDownNext)
        {
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            Debug.Log("e");
        }
        else if (handledObject.GetComponent<IInventoryObject>().OnUp && handledObject.GetComponent<IInventoryObject>().OnDown && handledObject.GetComponent<IInventoryObject>().OnUpNext)
        {
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);
            Debug.Log("f");
        }

        Debug.Log(handledObject.GetComponent<IInventoryObject>().OnUp);
        Debug.Log(handledObject.GetComponent<IInventoryObject>().OnDown);
        */
        if (!handledObject.GetComponent<IInventoryObject>().onRightNext && !handledObject.GetComponent<IInventoryObject>().onLeftNext && !handledObject.GetComponent<IInventoryObject>().OnDownNext && !handledObject.GetComponent<IInventoryObject>().OnUpNext)
        {
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x-pivotoffsetX,grid.GetCellCenterWorld(cellPosition).y);
        }

        if (!handledObject.GetComponent<IInventoryObject>().OnDownNext && handledObject.GetComponent<IInventoryObject>().OnUpNext && cellPosition.y>= handledObject.transform.position.y)
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y + pivotoffsetY);

        else if (handledObject.GetComponent<IInventoryObject>().OnDownNext && !handledObject.GetComponent<IInventoryObject>().OnUpNext && cellPosition.y <handledObject.transform.position.y)
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y - pivotoffsetY);

        else if (handledObject.GetComponent<IInventoryObject>().OnDownNext && handledObject.GetComponent<IInventoryObject>().OnUpNext)
            handledObject.transform.position = new Vector2(handledObject.transform.position.x, grid.GetCellCenterWorld(cellPosition).y);


        if (!handledObject.GetComponent<IInventoryObject>().onRightNext && handledObject.GetComponent<IInventoryObject>().onLeftNext && cellPosition.x < Mathf.Round(handledObject.transform.position.x))
        {
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x - pivotoffsetX, handledObject.transform.position.y);
            Debug.Log("a");
        }
            

        else if (handledObject.GetComponent<IInventoryObject>().onRightNext && !handledObject.GetComponent<IInventoryObject>().onLeftNext && cellPosition.x >= Mathf.Round(handledObject.transform.position.x))
        {

            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x + pivotoffsetX, handledObject.transform.position.y);
            Debug.Log("b");
        }

        else if (handledObject.GetComponent<IInventoryObject>().onRightNext && handledObject.GetComponent<IInventoryObject>().onLeftNext && cellPosition.x < Mathf.Round(handledObject.transform.position.x))
        {

            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x - pivotoffsetX, handledObject.transform.position.y);
            Debug.Log("c");
        }
        else if (handledObject.GetComponent<IInventoryObject>().onRightNext && handledObject.GetComponent<IInventoryObject>().onLeftNext && cellPosition.x >= Mathf.Round(handledObject.transform.position.x))
        {
            handledObject.transform.position = new Vector2(grid.GetCellCenterWorld(cellPosition).x + pivotoffsetX, handledObject.transform.position.y);
            Debug.Log("d");
        }
           

        /*

        Debug.Log("onleft " +handledObject.GetComponent<IInventoryObject>().onLeftNext);

        Debug.Log("onRight " + handledObject.GetComponent<IInventoryObject>().onRightNext);


        Debug.Log("onUp " + handledObject.GetComponent<IInventoryObject>().OnUpNext);


        Debug.Log("onDown " + handledObject.GetComponent<IInventoryObject>().OnDownNext);
        */
    }

    void farestTile()
    {

    }

    public void RegisterYourself(int ypos)
    {
        // Matrixe kayıt
        Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = grid.WorldToCell(selectedPosition);
        handledObject.transform.position = grid.GetCellCenterWorld(cellPosition);

        Debug.Log(grid.GetCellCenterWorld(cellPosition));
    }
}
