using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StackType
{
    None,
    Input,
    Output
}
public class Stackable : MonoBehaviour
{
    public StackType stackType;
    public CollectableType collectableType;
    public Socket[,,] socketMatris;
    [HideInInspector] public List<Socket> sockets = new List<Socket>();
    [SerializeField] private Vector3Int maxMatrisCounts = Vector3Int.one;
    [SerializeField] private Vector3 stackIntervalVector = Vector3.up;

    [SerializeField] private Vector3 initPose = Vector3.up;
    [SerializeField] private Collectable collectable; // TODO: delete
    private Coroutine _coroutine;
    private Stackable _tempStackable;
    private List<Socket> _tempSockets;




    protected Stackable myStackable;
    protected System.Action OnGetFromArea;
    public System.Action<Stackable> OnHaveObject;
    public Stackable MyStackable 
    {
        get=> myStackable;
        set=> myStackable = value;
    }


protected virtual void Awake()
{
    InitSockets();
    myStackable=this;

}
void InitSockets()
{
    socketMatris = new Socket[maxMatrisCounts.x, maxMatrisCounts.y, maxMatrisCounts.z];
    for (int j = 0; j < maxMatrisCounts.y; j++)
    {
        for (int i = 0; i < maxMatrisCounts.x; i++)
        {
            for (int k = 0; k < maxMatrisCounts.z; k++)
            {
                    Socket _stackable = new GameObject($"Socket-{i},{j},{k}").AddComponent<Socket>();
                    _stackable.Init(this);
                    socketMatris[i, j, k] = _stackable;
                    _stackable.transform.SetParent(transform);
                    _stackable.transform.localPosition = new Vector3(i * stackIntervalVector.x, j * stackIntervalVector.y, k * stackIntervalVector.z) + initPose;
                    sockets.Add(_stackable);
            }
        }
    }   
}
    private void Start()
    {
        
        if (!collectable) return;
        for (int i = 0; i < socketMatris.Length; i++)
        {
            Collectable col = Instantiate(collectable);
            sockets[i].InitStack(col);
        }

        
        
    }

    public virtual void ReShape()
    {
        for (int i = 0 ; i < sockets.Count - 1; i++)
        {
            if (sockets[i].isEmpty)
            {
                if (!sockets[i + 1].isEmpty)
                {
                    sockets[i + 1].MoveStack(sockets[i]);
                    ReShape();
                }
            }
        }
    }
    
    public virtual void GetArea(Stackable stackable)
    {
        if(stackable.stackType==StackType.Output)
        {
            GettingArea(stackable);
            
        }
        if(stackable.stackType==StackType.Input)
        {
            DeliverArea(stackable);
        }
        
    }
    
    public void StackObject(Stackable stackable)
    {
        _tempStackable.ReShape();
        stackable.OnGetFromArea?.Invoke();

        for (int i = 0; i <= _tempSockets.Count - 1; i++)
        {
            
            if (_tempSockets[i].isEmpty)
            {
                var _types =_tempSockets[i]._myStackable.collectableType;
                if(_types!=CollectableType.None)
                {
                    
                    if(!_tempStackable.GetAvailableFilledSocket(_types)) return;
                    
                    _tempSockets[i].AddStack(_tempStackable.GetAvailableFilledSocket(_types).stack);
                    _tempStackable.GetAvailableFilledSocket(_types).stack=null;
                    _tempSockets[i]._myStackable.OnHaveObject?.Invoke(_tempStackable);
                    
                    _tempStackable.ReShape();
                    return;
                }

                if(!_tempStackable.GetLastFilledSocket()) return;
                _tempSockets[i].AddStack(_tempStackable.GetLastFilledSocket().stack);
                _tempStackable.GetLastFilledSocket().stack = null;
                

                return;

            }
        }
    }

    public void DeliverArea(Stackable stackable)
    {
        _tempStackable=this;
        _tempSockets=stackable.sockets;
        StackObject(stackable);
        

    }
    public void GettingArea(Stackable stackable)
    {
        _tempStackable = stackable;
        _tempSockets = sockets;
        StackObject(stackable);

    }
    
    
    public virtual void Process(Stackable _myStackable)
    {
        
        _coroutine=StartCoroutine(ProcessCR(_myStackable));
    }

    public virtual void StopProcess()
    {
        if(_coroutine==null) return;

        StopCoroutine(_coroutine);
    }
    
    IEnumerator ProcessCR(Stackable _stackable)
    {
        float duration = 0.3f;

        GetArea(_stackable);

        while (duration>0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }

        Process(_stackable);
        
    }

    
    public virtual Socket GetLastFilledSocket()
    {
        for (int i = sockets.Count - 1; i >= 0; i--)
        {
            if (sockets[i].isEmpty) continue;

            return sockets[i];
        }
        return null;
    }

    public virtual Socket GetLastEmptySocket()
    {
        for (int i = 0; i<=sockets.Count - 1; i++)
        {
            if (!sockets[i].isEmpty) continue;

            return sockets[i];
        }
        return null;
    }

    public virtual Socket GetAvailableFilledSocket(CollectableType _type)
    {
        for (int i = sockets.Count - 1; i >= 0; i--)
        {
            if (sockets[i].isEmpty) continue;
            
            
            if(sockets[i].stack._CollectableType!=_type) continue;
             
            return sockets[i];
        }
        return null;
    }
}
