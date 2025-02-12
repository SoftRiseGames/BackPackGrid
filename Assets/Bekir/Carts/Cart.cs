using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cart : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private float _upScale;
    [SerializeField] private float _speed;
    private BaseItem _baseItem;
    private bool _mouseHolding;
    private Vector3 _startPosition;

    public void Init(BaseItem baseItem) {
        _baseItem = baseItem;
        _itemImage.sprite = _baseItem.ItemSprite;
        _description.text = _baseItem.Description;
        _name.text = _baseItem.ItemName;
    }
    void Update()
    {
        HoldingCard();
    }
    public void SetStartPosition(Vector3 startPosition){
        _startPosition = startPosition;
        transform.position = startPosition;
    }

    private void HoldingCard(){
        if (!_mouseHolding)return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        DOTween.Kill(transform);
        _mouseHolding = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _mouseHolding = false;
        DOTween.Kill(transform);
        transform.DOLocalMoveY(_startPosition.y, _speed);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.Kill(transform);
        transform.DOLocalMoveY(_startPosition.y, _speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DOTween.Kill(transform);
        transform.DOLocalMoveY(transform.localPosition.y + _upScale, _speed);
    }
}