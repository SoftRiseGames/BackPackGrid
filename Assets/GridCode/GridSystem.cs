using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject handledObject; 
    public Grid grid; 
    public GridRaycast gridInput;

    public IInventoryObject Inv;


    private void Start()
    {
        if (Inv is IRotatable)
        {
            ((IRotatable)Inv).RotateLeft(RegisterYourself);
        }
        else {
            Debug.Log("This object is not rotatable");
        }
        if(Inv is IHelper)
        {

            int SidePosx = ((IHelper)Inv).SidePosX();
            int SidePosY = ((IHelper)Inv).SidePosY();

            float PivotDistanceX = SidePosx*0.5f;
            float PivotDistancey= SidePosY*0.5f;

        }
        //handledObject.transform.position = grid.GetCellCenterWorld(cellPosition);

    }


    public void RegisterYourself(int ypos)
    {
        //matrixe kayýt
        
    }
}
