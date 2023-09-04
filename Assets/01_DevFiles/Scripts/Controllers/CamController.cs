using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0f, 2f, -10f);
    [SerializeField] float distanceDamp = 10f;

    Transform myT;
    void Awake()
    {
        myT = transform;
    }
    

    void LateUpdate()
    {
        Vector3 toPos = target.position + offset;
        Vector3 curPos = Vector3.Lerp(myT.position,toPos, distanceDamp * Time.deltaTime);
        myT.position = curPos;
    }
}