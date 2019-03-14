using UnityEngine;

public class Manager
{
    protected virtual void Awake()
    {
        Debug.Log(string.Format("Instance {0} is now awake", this));
    }

    protected virtual void Start()
    {
        Debug.Log(string.Format("Instance {0} is now starting", this));
    }

    protected virtual void Update()
    {

    }
}
