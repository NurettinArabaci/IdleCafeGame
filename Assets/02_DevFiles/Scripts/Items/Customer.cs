using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Customer : MonoBehaviour
{
    public enum CustomerState
    {
        None,
        GoTarget,
        ReachTarget,
        Completed,
        GoOut

    }

    private CustomerState state;
    private NavMeshAgent agent;
    private Animator animator;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        state= CustomerState.None;
    }

    private void ChangeState(CustomerState _state,SitArea _sitArea)
    {
        state = _state;
        StateControl(_sitArea);
    }

    public void SetSitArea(SitArea target)
    {
        
        StartCoroutine(DestinationControlCR(target));
    }
    
    private void StateControl(SitArea _sitArea)
    {
        var _transform = _sitArea.transform;
        switch (state)
        {
            case CustomerState.None:
                animator.SetTrigger("Idle");
                break;
            case CustomerState.GoTarget:
                animator.SetTrigger("Walk");
                agent.SetDestination(_transform.position);
                break;

            case CustomerState.ReachTarget:
                animator.SetTrigger("Sit");
                transform.DORotate(Vector3.up * _transform.rotation.eulerAngles.y, 0.4f);
                _sitArea.GetRandomOrder();
                break;

            case CustomerState.Completed:
                _sitArea.OpenCloseOrderPanel(false);
                break;

            case CustomerState.GoOut:
                animator.SetTrigger("Walk");
                agent.SetDestination(transform.position+Vector3.back*50);
                _sitArea.ChangeStackType();
                
                break;

            default:
                ChangeState(CustomerState.None,_sitArea);
                break;

                
        }
    }
    IEnumerator DestinationControlCR(SitArea sitArea)
    {
        ChangeState(CustomerState.GoTarget,sitArea);
        
        yield return new WaitUntil(()=>Vector3.Distance(transform.position, sitArea.transform.position) < 1f);
        ChangeState(CustomerState.ReachTarget,sitArea);
        
        yield return new WaitUntil(()=> !sitArea.IsAreaEmpty);
        ChangeState(CustomerState.Completed,sitArea);
        
        yield return new WaitForSeconds(3);
        ChangeState(CustomerState.GoOut,sitArea);
        
    }





}
