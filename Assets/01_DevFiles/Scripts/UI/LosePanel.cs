using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LosePanel : PanelBase
{
    private void Start()
    {
        panelType = PanelType.Lose;
    }

    protected override void ButtonOnClick()
    {
        
        PanelPassive(PanelType.Lose);
        GameStateEvent.Fire_OnChangeGameState(GameState.Restart);
    }
}
