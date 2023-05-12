using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickControl : MonoBehaviour
{
    public UnityAction<GameObject> AddBrick;
    public UnityAction<GameObject> RemoveBrick;
    public UnityAction ClearBrick;
    public UnityAction<GameObject> WinPos;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brick")
        {
            AddBrick(other.gameObject);

        }
        if (other.tag == "Bridge")
        {
            RemoveBrick(other.gameObject);
        }
        if (other.tag == "Win")
        {
            ClearBrick();
        }
        if (other.tag == "Chest")
        {
            WinPos(other.gameObject);
        }
    }
}
