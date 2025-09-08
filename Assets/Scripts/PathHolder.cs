using System.Collections.Generic;
using UnityEngine;

public class PathHolder : MonoBehaviour
{
    
    public List<Transform> pathPoints;

    public static PathHolder Instance;

    private void Awake()
    {
        if(Instance != null && Instance!= this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
