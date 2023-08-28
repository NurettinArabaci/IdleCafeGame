using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SocketController : Cachable<SocketController>
{
    [SerializeField] private Vector3Int maxMatrisCounts;
    [SerializeField] protected Vector3 stackIntervalVector;
    [SerializeField] Vector3 initPose;

    public Socket[,,] socketMatris;

    public Collectable cachedLastCollectable;

    
    void Awake()
    {
        socketMatris = new Socket[maxMatrisCounts.x, maxMatrisCounts.y, maxMatrisCounts.z];
        for (int j = 0; j < maxMatrisCounts.y; j++)
        {
            for (int i = 0; i < maxMatrisCounts.x; i++)
            {
                for (int k = 0; k < maxMatrisCounts.z; k++)
                {
                    Socket socket = new GameObject("Socket-" + i + " , " + j + " , " + k).AddComponent<Socket>(); ;
                    socketMatris[i, j, k] = socket;
                    socket._Transform.SetParent(transform);
                    socket._Transform.localPosition = new Vector3(i * stackIntervalVector.x, j * stackIntervalVector.y, k * stackIntervalVector.z) + initPose;
                }
            }
        }
    }

    

    public void AddStack(Collectable collectable)
    {
        Vector3Int vector = GetAppropirateSocket();
       
        if (vector == -Vector3Int.one) return;
        if (collectable == null) return;
        collectable.col.enabled = false;
        Socket socketHandler = socketMatris[vector.x, vector.y, vector.z];
        collectable.SetPos(socketHandler);

        socketHandler.stack = collectable;
        cachedLastCollectable = collectable;
    }

    public void AddStackToInit(Collectable collectable)
    {
        Vector3Int vector = GetAppropirateSocket();

        if (vector == -Vector3Int.one) return;
        if (collectable == null) return;
        collectable.col.enabled = false;
        Socket socketHandler = socketMatris[vector.x, vector.y, vector.z];
        collectable.mT.SetParent(socketHandler.transform);

        socketHandler.stack = collectable;
        cachedLastCollectable = collectable;
    }

    public bool SocketIsEmpty()
    {
        int counter = 0;
        foreach (var item in GetComponentsInChildren<Socket>())
        {
            if (!item.ChildIsEmpty)
                counter++;
            
        }
        if (counter > 0)
            return false;
        
        return true;
    }
    
    public List<Socket> GetFillSockets()
    {
        List<Socket> sockets = new List<Socket>();

        for (int i = maxMatrisCounts.z - 1; i >= 0; i--)
        {
            for (int j = maxMatrisCounts.y - 1; j >= 0; j--)
            {
                for (int k = maxMatrisCounts.x - 1; k >= 0; k--)
                {
                    if (socketMatris[k, j, i].stack != null)
                        sockets.Add(socketMatris[k, j, i]);
                }
            }
        }
        return sockets;
    }

    private Vector3Int GetAppropirateSocket()
    {
        for (int i = 0; i < maxMatrisCounts.x; i++)
        {
            for (int j = 0; j < maxMatrisCounts.y; j++)
            {
                for (int k = 0; k < maxMatrisCounts.z; k++)
                {
                    if (socketMatris[i, j, k].stack == null)
                        return new Vector3Int(i, j, k);
                }
            }
        }
        return -Vector3Int.one;
    }

    public List<Socket> GetEmptySockets()
    {
        List<Socket> sockets = new List<Socket>();

        for (int i = maxMatrisCounts.z - 1; i >= 0; i--)
        {
            for (int j = maxMatrisCounts.y - 1; j >= 0; j--)
            {
                for (int k = maxMatrisCounts.x - 1; k >= 0; k--)
                {
                    if (socketMatris[k, j, i].stack == null)
                        sockets.Add(socketMatris[k, j, i]);
                }
            }
        }
        return sockets;
    }


















    // private Vector3Int GetLastStackSocket()
    // {
    //     for (int i = maxMatrisCounts.z-1; i >= 0; i--)
    //     {
    //         for (int j = maxMatrisCounts.y-1; j >= 0; j--)
    //         {
    //             for (int k = maxMatrisCounts.x-1; k >= 0; k--)
    //             {
    //                 if (socketMatris[k, j, i].stack != null)
    //                     return new Vector3Int(k, j, i);
    //             }
    //         }
    //     }
    //     return -Vector3Int.one;
    // }

    // public Socket GetLastSocket()
    // {
    //     Vector3Int vector = GetLastStackSocket();
    //     if(vector==-Vector3Int.one)return null;
    //     Socket socketHandler = socketMatris[vector.x,vector.y,vector.z];
    //     return socketHandler;
    // }
}
