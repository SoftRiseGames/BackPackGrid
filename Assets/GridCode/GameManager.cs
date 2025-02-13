using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayerMask ignoreLayers;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tu�una bas�ld���nda
        {
            // Fare pozisyonunu ekrana g�re d�nya koordinatlar�na �evir
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Fare pozisyonunda 2D Raycast at
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, ~ignoreLayers);

            if (hit.collider != null) // E�er bir nesneye �arpt�ysa
            {
                if (hit.collider.tag == "InvObject")
                    hit.collider.GetComponent<IInventoryObject>().MoveObjectStarting();
                else
                    Debug.Log("not");

                Debug.Log("T�klanan nesne: " + hit.collider.name);
            }
        }
    }
}
