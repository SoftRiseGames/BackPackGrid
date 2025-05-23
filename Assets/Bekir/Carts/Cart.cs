using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cart : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IItem
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private float _upScale;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _cartPlacementLayer;
    [SerializeField] 
    private BaseItem _baseItem;
    private bool _mouseHolding;
    private Vector3 _startPosition;
    [SerializeField] private Enemy enemy;
    [SerializeField] bool canMove;
    private void OnEnable()
    {
        EventManagerCode.OnEnemyTurn += CanMoveFalse;
        EnemyManager.onPlayerTurn += CanMoveTrue;
    }
    private void Start()
    {
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    private void OnDisable()
    {
        EventManagerCode.OnEnemyTurn -= CanMoveFalse;
        EnemyManager.onPlayerTurn -= CanMoveTrue;
    }
    void CanMoveFalse()
    {
        canMove = false;
    }
    void CanMoveTrue()
    {
        canMove = true;
    }
    public void Init(BaseItem baseItem) {
        _baseItem = baseItem;
        _itemImage.sprite = _baseItem.ItemSprite;
        _description.text = _baseItem.Description;
        _name.text = _baseItem.ItemName;


        _baseItem.ItemEffects_OnPlaced?.ForEach(effect => effect?.ExecuteEffect(enemy));
    }
    void Update()
    {
        /*
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        */
      

        HoldingCard();
        if(Input.GetMouseButtonUp(0)){
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector3.forward, 100, _cartPlacementLayer);

            if (hit2D.collider != null)
            {
               OnAttack();
            }
            else
                return;
        }
    }
    public void SetStartPosition(Vector3 startPosition){
        _startPosition = startPosition;
        transform.position = startPosition;
    }

    private void HoldingCard(){
        if (canMove)
        {
            if (!_mouseHolding) return;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
      
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (canMove)
        {
            DOTween.Kill(transform);
            _mouseHolding = true;
        }
       
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (canMove)
        {
            _mouseHolding = false;
            DOTween.Kill(transform);
            transform.DOLocalMoveY(_startPosition.y, _speed);
        }
      
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

    public void OnTour()
    {
        _baseItem.ItemEffects_OnEveryTour?.ForEach(effect => effect?.TourEffect(enemy));
    }

    public void OnAttack()
    {
        _baseItem.ItemEffects_OnEnemy?.ForEach(effect => effect?.ExecuteEffect(enemy));
        DOTween.Kill(transform);
        Destroy(gameObject);
    }

}