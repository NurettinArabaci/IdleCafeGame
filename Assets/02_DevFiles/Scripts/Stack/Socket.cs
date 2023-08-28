using UnityEngine;

public class Socket : MonoBehaviour
{
    private Transform _transform;
    public Transform _Transform
    {
        get => _transform;
        set => _transform = value;
    }

    public bool ChildIsEmpty
    {
        get
        {
            if (transform.childCount > 0)
                return false;
            return true;
        }
    }

    public Collectable stack { get; set; }

    void Awake()
    {
        _transform = transform;
    }

    

}