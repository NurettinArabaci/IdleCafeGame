using UnityEngine.EventSystems;

public class JoystickController : FloatingJoystick
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.gameState == GameState.Begin)
        {
            GameStateEvent.Fire_OnChangeGameState(GameState.Play);
        }
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

}