using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private List<PanelBase> panels = new List<PanelBase>();
    [SerializeField] private TextMeshProUGUI _moneyText;

    public Transform _moneyParent;



    private void OnEnable()
    {
        GameStateEvent.OnBeginGame += OnResetGame;
        GameStateEvent.OnPlayGame += OnPlayGame;
        GameStateEvent.OnWinGame += OnWinGame;
        GameStateEvent.OnLoseGame += OnLoseGame;
        UIEvents.OnUpdateMoney += OnUpdateMoney;
    }
    
    public void OnResetGame()
    {
        foreach (var item in panels)
        {
            item.OnResetPanel();
            item.PanelActive(PanelType.Start);
            
        }
    }

    public void OnLoseGame()
    {
        foreach (var item in panels)
        {
            item.OnResetPanel();
            item.PanelActive(PanelType.Lose);
        }
    }

    private void OnWinGame()
    {
        foreach (var item in panels)
        {
            item.PanelActive(PanelType.Win);
        }
        
    }

    private void OnPlayGame()
    {
        foreach (var item in panels)
        {
            item.PanelPassive(PanelType.Start);
        }
    }

    public void OnUpdateMoney(int income)
    {
        _moneyText.GetComponent<Animator>().SetTrigger("CollectMoney");
        _moneyText.SetText(income.ToString() + " $");
        
    }

    private void OnDisable()
    {
        GameStateEvent.OnBeginGame -= OnResetGame;
        GameStateEvent.OnPlayGame -= OnPlayGame;
        GameStateEvent.OnWinGame -= OnWinGame;
        GameStateEvent.OnLoseGame -= OnLoseGame;
        UIEvents.OnUpdateMoney -= OnUpdateMoney;
    }
}
