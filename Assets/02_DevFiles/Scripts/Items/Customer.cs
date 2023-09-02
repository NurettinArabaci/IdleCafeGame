using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Customer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    public void SetDestination(Transform target)
    {
        agent.SetDestination(target.position);
        StartCoroutine(DestinationControl(target));
    }
    

    IEnumerator DestinationControl(Transform target)
    {
        yield return new WaitUntil(()=>Vector3.Distance(transform.position, target.position) < 1f);
        animator.SetTrigger("Sit");
        transform.DORotate(Vector3.up*target.rotation.eulerAngles.y,0.4f);
    }





}
