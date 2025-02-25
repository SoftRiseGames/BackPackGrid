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
                if (hit.collider.tag == "InvObject"){
                    

                    hit.collider.GetComponent<IInventoryObject>().MoveObject();

                    hit.collider.GetComponent<IInventoryObject>().MoveObjectStarting();
                }

                else
                    Debug.Log("not");

                Debug.Log("Týklanan nesne: " + hit.collider.name);
            }
        }
    }
}
