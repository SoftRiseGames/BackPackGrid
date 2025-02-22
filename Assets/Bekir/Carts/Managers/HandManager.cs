using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<IItem> Items = new();
    [SerializeField] private Transform _pivot;
    public void Init()
    {
    }
}
