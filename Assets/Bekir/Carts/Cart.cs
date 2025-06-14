using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Cart : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IItem
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _itemBG;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private float _upScale;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _cartPlacementLayer;
    public BaseItem _baseItem;
    private bool _mouseHolding;
    private Vector3 _startPosition;
    [SerializeField] private Enemy enemy;
    [SerializeField] bool canMove;
    public int PassiveTourCount;
    private PlayerHandler PlayerExecute;
    public bool isPlayerCollider;
    public bool isPlayed;
    [HideInInspector] public bool isCheckedPassiveSituation;
    [HideInInspector] public float CardDamage;

    [SerializeField] List<Image> ManaImages;
    private int ManaCount;
    Collider2D collider;

    public EnemyManager EnemyOrder;
    private void OnEnable()
    {
        EventManagerCode.OnEnemyTurn += CanMoveFalse;
        EnemyManager.onPlayerTurn += CanMoveTrue;
        EventManagerCode.OnEnemyTurn += TourPerTime;
    }
    private void Start()
    {
        PlayerExecute = GameObject.Find("Player").GetComponent<PlayerHandler>();
        PassiveTourCount = _baseItem.PassiveTourCount;
        CardDamage = _baseItem.TotalDamage;
        ManaCount = _baseItem.ManaCount;
        ManaCountImage();
        EnemyOrder = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    }
    
    private void OnDisable()
    {
        EventManagerCode.OnEnemyTurn -= CanMoveFalse;
        EnemyManager.onPlayerTurn -= CanMoveTrue;
        EventManagerCode.OnEnemyTurn -= TourPerTime;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.GetComponent<Animator>().SetBool("isEnemyHighlight", true);
            gameObject.transform.DOScale(.5f,.2f);
        }
        else if (collision.gameObject.tag == "Player")
        {
            gameObject.transform.DOScale(.5f, .2f);
            isPlayerCollider = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemy.GetComponent<Animator>().SetBool("isEnemyHighlight", false);
            gameObject.transform.DOScale(1f, .2f);
            enemy = null;
        }
        else if(collision.gameObject.tag == "Player")
        {
            gameObject.transform.DOScale(1f, .2f);
            isPlayerCollider = false;
        }
    }
    void ManaCountImage()
    {
        for(int i = 0; i< ManaCount; i++)
        {
            ManaImages[i].gameObject.SetActive(true);
        }
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
        _baseItem.ItemEffects_OnPlaced?.ForEach(effect => effect?.ExecuteEffect(enemy,PlayerExecute,gameObject.GetComponent<Cart>()));
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

            if (hit2D.collider != null && (GameManagerBekir.instance.ManaCount>=ManaCount))
            {
                GameManagerBekir.instance.ManaCount = GameManagerBekir.instance.ManaCount - ManaCount;
               collider = hit2D.collider;
               Execute();
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

        if (!_mouseHolding) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        
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
        if (canMove)
        {
            DOTween.Kill(transform);
            transform.DOLocalMoveY(_startPosition.y, _speed);

            foreach (Enemy e in EnemyOrder.enemies)
            {
                e.GetComponent<Enemy>().OrderImage.gameObject.SetActive(false);
                e.GetComponent<Animator>().SetBool("isEnemyOrder", false);
            }
              
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canMove)
        {
            DOTween.Kill(transform);
            transform.DOLocalMoveY(transform.localPosition.y + _upScale, _speed);


            foreach (Enemy e in EnemyOrder.enemies)
            {
                if (_baseItem.order < e.Order)
                {
                    e.GetComponent<Animator>().SetBool("isEnemyOrder", true);
                }
             
            }
        }
       
    }

    public void TourPerTime()
    {
        if (isPlayed)
        {
            if (PassiveTourCount > 0 && _baseItem.isHavePassive && collider.GetComponent<Enemy>()._health>0)
            {
                _baseItem.ItemEffects_OnEveryTour?.ForEach(effect => effect?.PassiveEffect(PlayerExecute, collider.GetComponent<Enemy>(), gameObject.GetComponent<Cart>())); ;
                PassiveTourCount = PassiveTourCount - 1;
            }
            else
            {
                _baseItem.ItemEffects_OnEveryTour?.ForEach(effect => effect?.PassiveReset(PlayerExecute, collider.GetComponent<Enemy>(), gameObject.GetComponent<Cart>()));
                Destroy(gameObject);
            }
        }
        TourPassiveEffect();
        
       
    }
    public void TourPassiveEffect()
    {
        _baseItem.ItemEffects_OnEffectedObject?.ForEach(effect => effect?.TourEffect(enemy, gameObject.transform.GetComponent<Cart>()));
        
    }
    public void PassiveSetup()
    {
        if(_baseItem.isHavePassive)
            _baseItem.ItemEffects_OnEveryTour?.ForEach(effect => effect?.PassiveStartSetting(PlayerExecute, collider.GetComponent<Enemy>(), gameObject.GetComponent<Cart>()));

    }
    public void Execute()
    {
        if ((enemy != null && _baseItem.isEnemyEffect))
        {
            if (_baseItem.order >= enemy.Order)
            {
                Enemy EnemyCollider = enemy;
                EventManagerCode.DMGEffectAction.Invoke();
                _baseItem.ItemEffects_OnEffectedObject?.ForEach(effect => effect?.ExecuteEffect(enemy, PlayerExecute, gameObject.transform.GetComponent<Cart>()));
                DOTween.Kill(transform);
                GameObject.Find("Pool").GetComponent<CartHandler>().SpawnedCarts.Remove(gameObject.GetComponent<Cart>());
                PassiveSetup();
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                _description.gameObject.SetActive(false);
                _name.gameObject.SetActive(false);
                _itemBG.gameObject.SetActive(false);
                _itemImage.gameObject.SetActive(false);
                foreach (Image i in ManaImages)
                {
                    i.gameObject.SetActive(false);
                }
                //canMove = false;
                transform.position = new Vector2(17.1f, transform.position.y);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                isPlayed = true;
            }
        }

        else if ((isPlayerCollider == true && _baseItem.isCharacterEffect == true))
        {
            Debug.Log("Human");
            _baseItem.ItemEffects_OnEffectedObject?.ForEach(effect => effect?.ExecuteEffect(enemy, PlayerExecute, gameObject.transform.GetComponent<Cart>()));
            DOTween.Kill(transform);
            GameObject.Find("Pool").GetComponent<CartHandler>().SpawnedCarts.Remove(gameObject.GetComponent<Cart>());
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _description.gameObject.SetActive(false);
            _name.gameObject.SetActive(false);
            _itemBG.gameObject.SetActive(false);
            _itemImage.gameObject.SetActive(false);
            foreach (Image i in ManaImages)
            {
                i.gameObject.SetActive(false);
            }
            transform.position = new Vector2(17.1f, transform.position.y);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isPlayed = true;
         
           
        }
       
    }

}