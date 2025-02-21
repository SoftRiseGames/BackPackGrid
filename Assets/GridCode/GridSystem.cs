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
            if (Input.GetKeyDown(KeyCode.R))
                Debug.Log("rotate");
        }
        else
        {
            Debug.Log("This object is not rotatable");
        }
    }

    private void OnMouseDrag()
    {
       
    }
    private void Update()
    {
        if (Inv != null && Inv.gridEnter)
        {
            Inv.RegisterYourself();
        }
        if (Inv != null && Inv is IRotatable)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ((IRotatable)Inv).RotateLeft();
                if (Inv.gridEnter)
                {
                    Inv.RegisterYourself();
                }
            }
                
        }
    }
 
    public void RegisterYourself(int ypos)
    {
        // Matrixe kay�t-
    }
}