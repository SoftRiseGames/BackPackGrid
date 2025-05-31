using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CartHandler : MonoBehaviour
{
    [SerializeField] private Cart _cartPrefab;
    [SerializeField] private Vector2 _cartDistances;
    [HideInInspector] public List<Cart> SpawnedCarts = new();
    [SerializeField] private Transform _pivot;
    public SerializedDictionary<string, BaseItem> _items = new();

    public CardLoad LoadedCards;

    
    private void Start()
    {
        foreach(string i in LoadedCards.LoadedObjectsList)
        {
            SpawnCart(i);
        }
    }
    public void SpawnCart(string baseItemName)
    {
        if(!_items.ContainsKey(baseItemName)) return;
        for(int i = 0; i< _items[baseItemName].HandCardCount; i++)
        {
            BaseItem selecteItem = _items[baseItemName];
            Cart tempCreated = Instantiate(_cartPrefab);
            tempCreated.Init(selecteItem);
            SpawnedCarts.Add(tempCreated);
            tempCreated.transform.SetParent(_pivot);
            tempCreated.transform.localScale = Vector3.one;
            RePos();
        }
        
    }
    /// <summary>
    /// Kartlar spawn olunca hesinin pozisyonunu tekrar ayarlıyor
    /// </summary>
    private void RePos()
    {
        float stepX = -(SpawnedCarts.Count * _cartDistances.x) / 2;
        float plusY = _cartDistances.y / SpawnedCarts.Count;

        float stepY = 0;
        for(int i = 0; i < SpawnedCarts.Count; i++)
        {
            Vector3 newPos = new Vector3(stepX, _pivot.transform.position.y + stepY, 0);
            SpawnedCarts[i].SetStartPosition(newPos);
            stepX += _cartDistances.x;
            if (i < SpawnedCarts.Count / 2)
            {
                stepY += plusY;
            }
            else if(i > 0)
            {
                stepY -= plusY;
            }
        }
    }
}