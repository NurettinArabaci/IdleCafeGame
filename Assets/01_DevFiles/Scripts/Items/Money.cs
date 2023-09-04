using UnityEngine;
using DG.Tweening;
using TMPro;

public class Money : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private GameObject _moneyObject;

    private int _moneyAmount;
    public int MoneyAmount
    { 
        get => _moneyAmount; 
        private set => _moneyAmount = value;
    }

    private Collider _collider;
    public Collider _Collider => _collider ??= _collider = GetComponent<Collider>();
    
    void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    void OnEnable()
    {
        _textUI.gameObject.SetActive(false);
        _moneyAmount = Random.Range(50, 120);
        GetComponent<Collider> ().enabled = false;
        
    }

    public void Collect()
    {
        _moneyObject.SetActive(false);
        _textUI.SetText(_moneyAmount.ToString() + " $");
        _textUI.gameObject.SetActive(true);
        _textUI.transform.SetParent(UIManager.Instance._moneyParent);
        
        _textUI.transform.DOScale(_textUI.transform.localScale*1.5f,0.3f).SetLoops(2,LoopType.Yoyo).OnComplete(()=>
        _textUI.transform.DOLocalMove(Vector3.zero,0.5f).SetEase(Ease.OutFlash).OnComplete(()=>
        {
            UIEvents.Fire_OnUpdateMoney(PlayerPrefs.GetInt("Money"));
            Destroy(_textUI.gameObject,0.1f);
            
        }));

        Destroy(gameObject);
    }
}
