using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    public static ComputerManager Instance;
    public List<Computers> computers = new List<Computers>();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}

[Serializable] public class Computers
{
    public Computer computerObject;
    public bool isIdle = true;
    public float cost;
}