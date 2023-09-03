using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderController : MonoSingleton<OrderController>
{
    [SerializeField] private Customer customerPrefab;
    [Range(5,50)]
    [SerializeField] private int _customerAmount = 5;
    [SerializeField] private List<SitArea> sitAreas = new List<SitArea>();
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    
    private List<Customer> customers = new List<Customer>();

    public bool finisedCustomer => customers.Count <= 0 ;
        
    private void Start()
    {
        SpawnCustomer();
        
    }

    void Update() // TODO: Delete Update *********************************************************!!!!!!!!
    {
        if(Input.GetKeyDown(KeyCode.A))
            SelectCustomer();
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
        if(customers.Count<=0 || sitAreas.Count<=0) return;

        customers.Last().SetSitArea(RandomEmptyArea());
        customers.Remove(customers.Last());

    }


    

}
