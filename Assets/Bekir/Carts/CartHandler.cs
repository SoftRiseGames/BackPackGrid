using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CartHandler : MonoBehaviour
{
    [SerializeField] private Cart _cartPrefab;
    [SerializeField] private Vector2 _cartDistances;
    [SerializeField] public List<Cart> SpawnedCarts = new();
    [SerializeField] private Transform _pivot;
    public SerializedDictionary<string, BaseItem> _items = new();

    [SerializeField] List<BaseItem> AddItemList;

    public CardLoad LoadedCards;

    
    private void Start()
    {

        // 1. Tüm kart isimlerini, spawn sayısına göre listele
        List<string> allCardsToSpawn = new List<string>();

        foreach (string i in LoadedCards.LoadedObjectsList)
        {
            if (!_items.ContainsKey(i)) continue;

            for (int b = 0; b < _items[i].HandCardCount; b++)
            {
                allCardsToSpawn.Add(i);
            }
        }

        ShuffleList(allCardsToSpawn);

      
        foreach (string cardName in allCardsToSpawn)
        {
            SpawnCart(cardName);
        }
    }
    public void SpawnCart(string baseItemName)
    {
        if (!_items.ContainsKey(baseItemName)) return;
        BaseItem selecteItem = _items[baseItemName];
        Cart tempCreated = Instantiate(_cartPrefab);
        tempCreated.Init(selecteItem);
        SpawnedCarts.Add(tempCreated);
        tempCreated.transform.SetParent(_pivot);
        tempCreated.transform.localScale = Vector3.one;
        RePos();
        
    }
    void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
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