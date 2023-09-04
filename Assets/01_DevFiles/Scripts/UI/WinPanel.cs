using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : PanelBase
{
    protected override void ButtonOnClick()
    {
        PanelPassive(PanelType.Win,()=>GameStateEvent.Fire_OnChangeGameState(GameState.Restart));

        
    }
}
