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
    public ProductType productType;
    public CollectableType collectableType;
    public System.Action<Stackable> OnHaveObject;
    public Socket[,,] socketMatris;
    [HideInInspector] public List<Socket> sockets = new List<Socket>();
    [SerializeField] private Vector3Int maxMatrisCounts = Vector3Int.one;
    [SerializeField] private Vector3 stackIntervalVector = Vector3.up;

    [SerializeField] private Vector3 initPose = Vector3.up;
    
    private Coroutine _coroutine;
    private Stackable _tempStackable;
    private List<Socket> _tempSockets;


    protected System.Action OnGetFromArea;
    protected Stackable myStackable;
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
    private void InitSockets()
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
            if(stackable.GetLastFilledSocket() && stackable.productType==ProductType.PreparedProduct && MyStackable.GetLastFilledSocket())
                stackable.GetLastFilledSocket().stack.MergeCollectable(MyStackable.GetLastFilledSocket());
            if(MyStackable.IsSocketsFull) return;
            
            stackable.OnGetFromArea?.Invoke();
            GettingArea(stackable);
            
        }
        if(stackable.stackType==StackType.Input)
        {
            if(!MyStackable.GetLastFilledSocket()) return;

            stackable.OnGetFromArea?.Invoke();
            DeliverArea(stackable);
        }
        
    }
    
    public void StackObject()
    {
        
        _tempStackable.ReShape();

        for (int i = 0; i <= _tempSockets.Count - 1; i++)
        {            
            if (_tempSockets[i].isEmpty)
            {
                
                var _collectType =_tempSockets[i]._myStackable.collectableType;
                var _productType =_tempSockets[i]._myStackable.productType;
                
                if(_productType==ProductType.None) return;
                
                if(_productType!=ProductType.All)
                {                   
                    if(!_tempStackable.GetAvailableFilledSocket(_collectType,_productType)) return;
                   
                    _tempSockets[i].AddStack(_tempStackable.GetAvailableFilledSocket(_collectType,_productType).stack);
                    _tempStackable.GetAvailableFilledSocket(_collectType,_productType).stack=null;
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
        StackObject();

    }
    public void GettingArea(Stackable stackable)
    {
        _tempStackable = stackable;
        _tempSockets = sockets;
        StackObject();
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

    private bool IsSocketsFull => sockets[sockets.Count-1]==GetLastFilledSocket();
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
    public virtual Socket GetAvailableFilledSocket(CollectableType _collectableType)
    {
        for (int i = sockets.Count - 1; i >= 0; i--)
        {
            if (sockets[i].isEmpty) continue;
            
            if(sockets[i].stack._CollectableType !=_collectableType) continue;
             
            return sockets[i];
        }
        return null;
    }

    public virtual Socket GetAvailableFilledSocket(CollectableType _collectableType,ProductType _productType)
    {
        for (int i = sockets.Count - 1; i >= 0; i--)
        {
            if (sockets[i].isEmpty) continue;
            
            
            if(sockets[i].stack._ProductType !=_productType) continue;
            if(sockets[i].stack._CollectableType !=_collectableType) continue;
             
            return sockets[i];
        }
        return null;
    }
}
