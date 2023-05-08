using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueController : MonoBehaviour
{
    public static QueueController Instance;
    public Transform customerPos;

    public List<CustomerStateMachine> queueList = new List<CustomerStateMachine>();
    public List<Transform> queueLocations = new List<Transform>();
    void Awake()
    {
        queueList = new List<CustomerStateMachine>();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddQueue(CustomerStateMachine customer)
    {
        queueList.Add(customer);
        RefreshQueue();
    }

    public void RefreshQueue(bool forceRefresh = false)
    {
        int i = 0;
        foreach(CustomerStateMachine customer in queueList)
        {
            if(!customer.inQueue || forceRefresh)
                customer.QueueUp(queueLocations[i]);

            i++;
        }
    }
}
