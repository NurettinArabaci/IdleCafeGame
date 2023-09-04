using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using TMPro;

public class ItemController : MonoBehaviour
{
    [SerializeField] bool _isLock;
    [Space(10)]
    [SerializeField] ProductSo myProduct;
    [SerializeField] Image _filledObj;
    [SerializeField] GameObject _itemsParent, _openItemParent;

    [SerializeField] TextMeshProUGUI _costText;
    Coroutine itemOpenCR;


    [SerializeField] int _openCost = 1000;

    private void Initialize()
    {
        _costText.SetText(_openCost.ToString() + " $");

        myProduct.isLock = _isLock;
        if(myProduct.isLock)
        {
            _itemsParent.SetActive(false);
            _openItemParent.SetActive(true);
            return;
        }
        _itemsParent.SetActive(true);
        _itemsParent.transform.localScale=Vector3.one;
        _openItemParent.SetActive(false);
    }

    void Awake()
    {
        Initialize();
    }
    
    private void OpenItem(int playerMoney)
    {
        UIEvents.Fire_OnUpdateMoney(playerMoney -_openCost);
        _itemsParent.SetActive(true);
        _itemsParent.transform.localScale=Vector3.zero;
        _itemsParent.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutQuint);
        _openItemParent.SetActive(false);
        myProduct.isLock = false;
    }

    IEnumerator OpenItemCR(int playerMoney)
    {
        float initDuration=1.2f;
    
        float duration = initDuration;
    
        while(duration>0)
        {
            duration -= Time.deltaTime;
            _filledObj.fillAmount += Time.deltaTime / initDuration;
            yield return null;
        }

        OpenItem(playerMoney);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            if(player.Money < _openCost) return;
            itemOpenCR = StartCoroutine(OpenItemCR(player.Money));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            if(itemOpenCR==null) return;
            StopCoroutine(itemOpenCR);
            _filledObj.fillAmount = 0;
        }
    }
}
