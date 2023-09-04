using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderController : MonoSingleton<OrderController>
{
    [SerializeField] private Customer customerPrefab;
    [Range(5,50)]
    [SerializeField] private int _customerAmount = 10;
    [SerializeField] private List<SitArea> sitAreas = new List<SitArea>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    
    private int _initSitAreaCount = 0;
    private List<Customer> customers = new List<Customer>();
    [HideInInspector]
    public List<Customer> tempCustomers = new List<Customer>();

    private bool finishedCustomer => customers.Count <= 0 ;
    private bool finishedTempCustomer => tempCustomers.Count <= 0 ;
        
    private bool HasEmptySitArea => sitAreas.Count > 0;

    void OnEnable()
    {
        GameStateEvent.OnPlayGame += StartSelectCustomer;
    }

    private void Start()
    {
        SpawnCustomer();

        
        _initSitAreaCount = sitAreas.Count;
    }

    void StartSelectCustomer()
    {
        StartCoroutine(StartSelectCustomerCR());
    }

    IEnumerator StartSelectCustomerCR()
    {
        SelectCustomer();
        yield return new WaitForSeconds(Random.Range(1, 10));
        StartCoroutine(StartSelectCustomerCR());
    }

    public bool CompletedOrder()
    {
        return finishedTempCustomer && _initSitAreaCount>= sitAreas.Count;
    }
    public void AddListEmptyArea(SitArea addArea)
    {
        if(sitAreas.Contains(addArea)) return;

        sitAreas.Add(addArea);
    }

    public void RemoveListEmptyArea(SitArea removeArea)
    {
        if(!sitAreas.Contains(removeArea)) return;
        
        sitAreas.Remove(removeArea);
    }

    public SitArea RandomEmptyArea()
    {
        var area= sitAreas[Random.Range(0,sitAreas.Count)];
        RemoveListEmptyArea(area);
        return area;
    }

    private Vector3 RandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0,spawnPoints.Count)].position;
    }
     

    public void SpawnCustomer()
    {
        for(int i=0; i < _customerAmount ; i++)
        {
            var _customer = Instantiate(customerPrefab,RandomSpawnPoint(),Quaternion.identity,transform) ;
            customers.Add(_customer);
        }
    }

    public void SelectCustomer()
    {
        if(finishedCustomer || !HasEmptySitArea) return;

        customers.Last().SetSitArea(RandomEmptyArea());
        var lastCustomer= customers.Last();
        customers.Remove(lastCustomer);
        tempCustomers.Add(lastCustomer);

    }

    void OnDisable()
    {
        GameStateEvent.OnPlayGame -= StartSelectCustomer;
    }

    

}
