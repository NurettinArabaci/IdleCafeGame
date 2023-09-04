using UnityEngine;

public abstract class Cachable<T> : MonoBehaviour where T : Component
{
    protected PlayerController playerController;

    public T Init(PlayerController _playerController)
    {
        this.playerController = _playerController;
        return this as T;
        
    }

}

