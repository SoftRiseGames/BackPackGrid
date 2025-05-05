using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject handledObject;
    public Grid grid;
    public GridRaycast gridInput;
    float PivotDistanceX;
    float PivotDistancey;
    public IInventoryObject Inv;


    private void Start()
    {
       
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

    private void LateUpdate()
    {

    }

    public void RegisterYourself(int ypos)
    {
        // Matrixe kayýt-
    }
}