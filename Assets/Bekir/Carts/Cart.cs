using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cart : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _name;
    private BaseItem _baseItem;
    private bool _mouseEnter;
    
    public void Init(BaseItem baseItem) {
        _baseItem = baseItem;
        _itemImage.sprite = _baseItem.ItemSprite;
        _description.text = _baseItem.Description;
        _name.text = _baseItem.ItemName;
    }
    void Update()
    {
        
    }
    private void HoldingCard(){
        if (!_mouseEnter)return;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _mouseEnter = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _mouseEnter = false;
    }
}