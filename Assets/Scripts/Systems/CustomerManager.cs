using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;
    public float maxCustomer;
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] private Transform _spawnPoint;
    public List<CustomerStateMachine> customers = new List<CustomerStateMachine>();
    [SerializeField] private float _minSpawnDelay = 2f;
    [SerializeField] private float _maxSpawnDelay = 8f;
    private float _nextSpawnTime;
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
        if (customers.Count < maxCustomer && Time.time > _nextSpawnTime)
        {
            SpawnNewCustomer();
            _nextSpawnTime = Time.time + Random.Range(_minSpawnDelay, _maxSpawnDelay);
        }
    }
    private void SpawnNewCustomer()
    {
        GameObject customerGameObject = Instantiate(_customerPrefab, _spawnPoint.position, Quaternion.identity);
        CustomerStateMachine customer = customerGameObject.GetComponent<CustomerStateMachine>();
        QueueController.Instance.AddQueue(customer);
        customers.Add(customer);
    }
}
