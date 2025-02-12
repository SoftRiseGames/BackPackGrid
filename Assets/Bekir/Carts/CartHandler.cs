using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cysharp.Threading.Tasks;

public class CartHandler : MonoBehaviour
{
    [SerializeField] private Cart _cartPrefab;
    [SerializeField] private Vector2 _cartDistances;
    [HideInInspector] public List<Cart> SpawnedCarts = new();
    [SerializeField] private Transform _pivot;
    
    public SerializedDictionary<string, BaseItem> _items = new();

    public void SpawnCart(string baseItemName)
    {
        if(!_items.ContainsKey(baseItemName)) return;

        Cart tempCreated = Instantiate(_cartPrefab);
        SpawnedCarts.Add(tempCreated);
        tempCreated.transform.SetParent(_pivot);
        tempCreated.transform.localScale = Vector3.one;

        RePosition();
    }
    public async void RePosition()
    {
        for(int i = 0; i < SpawnedCarts.Count; i++)
        {
            SpawnedCarts[i].transform.position = new Vector3(_pivot.position.x + i + _cartDistances.x, _pivot.position.y,0);
            await UniTask.Delay(250, false);
        }
    }
}