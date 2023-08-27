using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SocketController : MonoBehaviour
{
    [SerializeField] private Vector3Int maxMatrisCounts;
    [SerializeField] protected Vector3 stackIntervalVector;
    public Socket[,,] socketMatris;
    PlayerController player;

    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        socketMatris = new Socket[maxMatrisCounts.x, maxMatrisCounts.y, maxMatrisCounts.z];
        for (int j = 0; j < maxMatrisCounts.y; j++)
        {
            for (int i = 0; i < maxMatrisCounts.x; i++)
            {
                for (int k = 0; k < maxMatrisCounts.z; k++)
                {
                    var obj = new GameObject("Socket-" + i + " , " + j + " , " + k);
                    Socket socket = obj.AddComponent<Socket>();
                    socketMatris[i, j, k] = socket;
                    socket.mT.SetParent(transform);
                    socket.mT.localPosition = new Vector3(j * stackIntervalVector.x, i * stackIntervalVector.y, k * stackIntervalVector.z);
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
        socketHandler.stack = collectable;
        collectable.SetPos(socketHandler);
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
