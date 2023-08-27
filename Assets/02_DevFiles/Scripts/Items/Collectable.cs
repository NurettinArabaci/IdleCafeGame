using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Collider _col;
    public Collider col => _col ??= GetComponent<Collider>();

    private Transform _mT;
    public Transform mT => _mT ??= transform;

    public void Collected()
    {
        CollectableEvents.Fire_OnCollectableWithStack(gameObject);
    }

    public Vector3 GetPose()
    {
        return transform.position;
    }

    public void SetStack(StackController stackController)
    {
        stackController.InsertStack(this);
    }

    public void SetPos(Socket socket)
    {
        if (PosCR != null)
            StopCoroutine(PosCR);
        PosCR = null;

        PosCR = StartCoroutine(SetPosCr(socket.transform));

    }

    Coroutine PosCR;

    IEnumerator SetPosCr(Transform targetT, System.Action action = null)
    {
        float timer = 0;
        float duration = .8f;
        Vector3 cachedPos = mT.position;
        col.enabled = false;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            mT.position = Vector3.Lerp(cachedPos, cachedPos + Vector3.up * 8, timer / duration);
            yield return null;
        }
        timer = 0;
        duration = .4f;
        cachedPos = mT.position;
        Quaternion cachedRot = mT.rotation;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            mT.position = Vector3.Lerp(cachedPos, targetT.position, timer / duration);
            mT.rotation = Quaternion.Lerp(cachedRot, targetT.rotation, timer / duration);

            yield return null;
        }

        mT.SetParent(targetT);
        mT.localPosition = Vector3.zero;
        mT.localRotation = Quaternion.Euler(Vector3.zero);
        yield return new WaitForSeconds(1);

        action?.Invoke();
    }
}
