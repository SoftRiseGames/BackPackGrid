using UnityEngine;

public class GridRaycast : MonoBehaviour
{
    public Camera sceneCamera; 
    private Vector3 m_lastPosition;
    public LayerMask groundLayerMask; 

    
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = sceneCamera.ScreenToWorldPoint(mousePos);
        worldPos.z = 0; 

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, groundLayerMask);
        Debug.DrawRay(worldPos, Vector2.zero, Color.red);

        if (hit.collider != null)
        {
            m_lastPosition = hit.point;
        }

        return m_lastPosition;
    }
    public bool GetPlacementInput() => Input.GetMouseButtonDown(0);
}