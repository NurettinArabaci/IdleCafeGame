using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BankArea : MonoBehaviour
{
    private SocketController _socketController;
    public SocketController socketController => _socketController ??= GetComponent<SocketController>();
    [SerializeField] private Vector3Int stackCountVector;
    [SerializeField] private Vector3 stackIntervalVector;

    

    protected void SetCanvas()
    {
        //costText.text = currentVal.ToString();
    }
    public void Collide()//Agent agent)
    {
       // agent.StockUp(this);
    }

    public void StockUpArea(List<Socket> sockets)
    {
        GetComponentInChildren<Animator>().SetTrigger("OpenCover");
        StartCoroutine(StockUpCR(sockets));
    }

    IEnumerator StockUpCR(List<Socket> sockets)
    {
        for (int i = 0; i < sockets.Count; i++)
        {
            socketController.AddStack(sockets[i].stack);
            sockets[i].stack = null;
            SetCanvas();
            yield return new WaitForSeconds(.2f);
        }
    }

    public void TakeFromArea(SocketController otherSocket)
    {
        
        StartCoroutine(TakeCR(otherSocket));

    }

    IEnumerator TakeCR(SocketController otherSocket)
    {
        List<Socket> cachedSockets = socketController.GetFillSockets();
        /*for (int i = 0; i < cachedSockets.Count; i++)
        {
            Socket cachedSocket = cachedSockets[i];
            if(otherSocket.GetEmptySockets().Count==0) yield break;


            otherSocket.AddStack(cachedSocket.stack);
            cachedSocket.stack = null;
            //currentVal--;
            SetCanvas();
            yield return new WaitForSeconds(.2f);
        }*/
        if (otherSocket.GetEmptySockets().Count == 0) yield break;

        var _lastSocket = cachedSockets[cachedSockets.Count - 1];
        GetComponentInChildren<Animator>().SetTrigger("OpenCover");
        yield return new WaitForSeconds(.1f);
        otherSocket.AddStack(_lastSocket.stack);
        _lastSocket.stack = null;
        yield return new WaitForSeconds(.2f);
    }







 

}
