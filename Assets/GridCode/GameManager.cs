using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayerMask ignoreLayers; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~ignoreLayers)) 
            {
                Debug.Log("Týklanan nesne: " + hit.collider.name);
            }
        }
    }
}
