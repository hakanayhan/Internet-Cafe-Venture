using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;
    public float maxCustomer;
    public Transform spawnPoint;
    public Transform exitPoint;
    public Transform checkoutLocation;
    [SerializeField] private GameObject _customerPrefab;
    public List<CustomerStateMachine> customers = new List<CustomerStateMachine>();
    public double minSpawnDelay = 2;
    public double maxSpawnDelay = 8;
    [HideInInspector] public float nextSpawnTime;
    List<string> names = new List<string>()
    {
    "Abigail", "Alexander", "Aiden", "Amelia", "Andrew", "Anna", "Ava",
    "Benjamin", "Charlotte", "Daniel", "David", "Elijah", "Elizabeth", "Emily", "Emma",
    "Ethan", "Grace", "Hannah", "Isabella", "James", "Joseph", "Joshua", "Liam", "Lily",
    "Lucas", "Matthew", "Mia", "Michael", "Oliver", "Olivia", "Owen", "Ryan", "Samuel",
    "Sarah", "Sophia", "Sophie", "Thomas", "Victoria", "William", "Zoe"
    };
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if (customers.Count < maxCustomer && Time.time > nextSpawnTime)
        {
            SpawnNewCustomer();
            SetDelayTime();
        }
    }
    private void SpawnNewCustomer()
    {
        GameObject customerGameObject = Instantiate(_customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerStateMachine customer = customerGameObject.GetComponent<CustomerStateMachine>();
        QueueController.Instance.AddQueue(customer);
        SetCustomerName(customer);
        customers.Add(customer);
    }

    private void SetCustomerName(CustomerStateMachine customer)
    {
        int i = Random.Range(0, names.Count);
        customer.customerName = names[i];
    }

    public void SetDelayTime()
    {
        nextSpawnTime = Time.time + Random.Range((float)minSpawnDelay, (float)maxSpawnDelay);
    }
}
