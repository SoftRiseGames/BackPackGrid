using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CartHandler : MonoBehaviour
{
    [SerializeField] private Cart _cartPrefab;
    [SerializeField] private Vector2 _cartDistances;
    public List<Cart> SpawnedCarts = new();
    [SerializeField] private Transform _pivot;
    public SerializedDictionary<string, BaseItem> _items = new();

    public CardLoad LoadedCards;
    [SerializeField] int MaxHandleCardCount;
    [SerializeField] GameObject CardDeckPivot;
    private int TotalCardToHand;
    int LastDeck;
    [SerializeField]List<string> allCardsToSpawn = new List<string>();
    private void OnEnable()
    {
        EventManagerCode.OnEnemyTurn += TotalCardCount;
    }
    private void OnDisable()
    {
        EventManagerCode.OnEnemyTurn -= TotalCardCount;
    }
    private void Start()
    {

      
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
            if (TotalCardToHand < MaxHandleCardCount)
            {
                SpawnCart(cardName);
                LastDeck = LastDeck + 1;
                TotalCardToHand = TotalCardToHand + 1;
            }
        }

        Debug.Log(LastDeck);
    }
    void TotalCardCount()
    {
        int activeChildCount = 0;

        foreach (Transform child in CardDeckPivot.transform)
        {
            if (child.gameObject.activeSelf)
            {
                activeChildCount++;
            }
        }

        TotalCardToHand = activeChildCount;
        Debug.Log(TotalCardToHand);
    }
    //Kart Çekme Eventi;
    /*
    public void OutHandToHand(string baseItemName)
    {
        if (!_items.ContainsKey(baseItemName)) return;
        BaseItem selecteItem = _items[baseItemName];
        Cart tempCreated = Instantiate(_cartPrefab);
        tempCreated.Init(selecteItem);
        SpawnedCarts.Add(tempCreated);
        tempCreated.transform.SetParent(_pivot);
        tempCreated.transform.localScale = Vector3.one;
        tempCreated.transform.position = new Vector2(0, 0, 0);
    }
    */
    public void AddNewCard()
    {
        if (LastDeck < allCardsToSpawn.Count-1)
        {
            if (TotalCardToHand < MaxHandleCardCount)
            {
                TotalCardToHand = TotalCardToHand + 1;
                LastDeck = LastDeck + 1;
                SpawnCart(allCardsToSpawn[LastDeck]);
                Debug.Log(TotalCardToHand);
            }
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