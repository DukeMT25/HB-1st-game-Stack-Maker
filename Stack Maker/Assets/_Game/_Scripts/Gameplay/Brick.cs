    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private GameObject brickObj;

    public GameObject GetbrickObj()
    {
        return brickObj;
    }

    public void SetbrickObj(GameObject value)
    {
        brickObj = value;
    }

    private bool isActive;

    public bool GetisActive()
    {
        return isActive;
    }

    public void SetisActive(bool value)
    {
        isActive = value;
    }

    public Brick(GameObject brickObj, bool isActive)
    {
        this.SetbrickObj(brickObj);
        this.SetisActive(isActive);
    }
}
