using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject handledObject;
    public Grid grid;
    public GridRaycast gridInput;
    float PivotDistanceX;
    float PivotDistancey;
    public IObjectSetting Inv;


    private void Start()
    {
       
    }

    private void OnMouseDrag()
    {
       
    }
    private void Update()
    {
        if(Inv != null)
        {
            Debug.Log("INV");
        }

        if (Inv != null && Inv.InventoryObjectData.gridEnter)
        {
            Debug.Log("Sað týk");
            Inv.RegisterCaller();
        }


        if (Inv != null && Inv is IRotatable)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ((IRotatable)Inv).RotateLeft();

                if (Inv.InventoryObjectData.gridEnter)
                {
                    Inv.RegisterCaller();
                }
            }

        }

        if (Inv != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("sol TIk");
                Inv.ObjectOutOfGridCaller();
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