using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] GameObject popUpPanel,_startPanel;
    [SerializeField] Button _popUpOpenButton, _popUpCloseButton;

    protected override void Awake()
    {
        base.Awake();

        PopUpOpenButton();
        PopUpCloseButton();

    }

    public void StartPanelActive(bool state)
    {
       
        _startPanel.SetActive(state);
    }

    void PopUpOpenButton()
    {
        _popUpOpenButton.onClick.AddListener(() =>
        {
            popUpPanel.SetActive(true);
            popUpPanel.transform.localScale = Vector3.zero;
            popUpPanel.transform.DOScale(1, 0.4f).OnComplete(()=>GameStateEvent.Fire_OnChangeGameState(GameState.PopUp));

            _popUpOpenButton.transform.DOScale(0, 0.1f).OnComplete(() =>
                _popUpOpenButton.gameObject.SetActive(false));

            SoundEvents.Fire_OnPlaySfx(SoundManager.Instance.buttonClickSfx);

        });

        
    }

    void PopUpCloseButton()
    {
        _popUpCloseButton.onClick.AddListener(() =>
        {
            GameStateEvent.Fire_OnChangeGameState(GameState.Play);

            popUpPanel.transform.DOScale(0, 0.2f).OnComplete(() => popUpPanel.SetActive(false));
            _popUpOpenButton.gameObject.SetActive(true);
            _popUpOpenButton.transform.DOScale(1, 0.2f);

            SoundEvents.Fire_OnPlaySfx(SoundManager.Instance.buttonClickSfx);
        });
    }
     

}
