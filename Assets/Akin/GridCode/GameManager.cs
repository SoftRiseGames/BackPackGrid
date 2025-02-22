using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayerMask ignoreLayers;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tuþuna basýldýðýnda
        {
            // Fare pozisyonunu ekrana göre dünya koordinatlarýna çevir
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Fare pozisyonunda 2D Raycast at
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, ~ignoreLayers);

            if (hit.collider != null) // Eðer bir nesneye çarptýysa
            {
<<<<<<< Updated upstream:Assets/GridCode/GameManager.cs
                if (hit.collider.tag == "InvObject")
=======
                if (hit.collider.tag == "InvObject"){
                    
>>>>>>> Stashed changes:Assets/Akin/GridCode/GameManager.cs

                    hit.collider.GetComponent<IInventoryObject>().MoveObject();

                    hit.collider.GetComponent<IInventoryObject>().MoveObjectStarting();
<<<<<<< Updated upstream:Assets/GridCode/GameManager.cs
=======
                }
>>>>>>> Stashed changes:Assets/Akin/GridCode/GameManager.cs

                else
                    Debug.Log("not");

                Debug.Log("Týklanan nesne: " + hit.collider.name);
            }
        }
    }
}
