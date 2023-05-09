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
    [SerializeField] private float _minSpawnDelay = 2f;
    [SerializeField] private float _maxSpawnDelay = 8f;
    [HideInInspector] public float nextSpawnTime;
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
        customers.Add(customer);
    }

    public void SetDelayTime()
    {
        nextSpawnTime = Time.time + Random.Range(_minSpawnDelay, _maxSpawnDelay);
    }
}
