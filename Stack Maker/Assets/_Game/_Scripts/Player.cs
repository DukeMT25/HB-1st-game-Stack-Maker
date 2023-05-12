using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject People;
    [SerializeField] private GameObject TheStack;
    [SerializeField] private GameObject Cube;
    [SerializeField] private DetectSwipe DetectSwipe;
    [SerializeField] private BrickControl brickControl;
    [SerializeField] float speed;

    private enum Direct
    {
        None,
        Up,
        Down,
        Right,
        Left
    }
    private Direct direct;
    private Vector3 moveTarget;

    private bool isWin = false;
    private bool isMoving = false;
    private int countBrick;

    string brick = "Brick";
    int inGameBrick;

    private float sizeBrick = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (DetectSwipe != null)
        {
            DetectSwipe.swipeLeft += DetectSwipe_Left;
            DetectSwipe.swipeRight += DetectSwipe_Right;
            DetectSwipe.swipeUp += DetectSwipe_Up;
            DetectSwipe.swipeDown += DetectSwipe_Down;
        }
        if (brickControl != null)
        {
            brickControl.AddBrick += AddBrick;
            brickControl.RemoveBrick += RemoveBrick;
            brickControl.ClearBrick += ClearBrick;
            brickControl.WinPos += WinPos;
        }
        OnInit();
    }

    public void OnInit()
    {

        inGameBrick = PlayerPrefs.GetInt(brick, 0);

        isWin = false;
        isMoving = false;
        direct = Direct.None;
        countBrick = 0;
    }

    void FixedUpdate()
    {
        if (Cube == null)
        {
            return;
        }
        if (!isWin)
        {
            if (isMoving && direct == Direct.Left)
            {
                Gogogo(Vector3.left);
            }
            if (isMoving && direct == Direct.Right)
            {
                Gogogo(Vector3.right);
            }
            if (isMoving && direct == Direct.Up)
            {
                Gogogo(Vector3.forward);
            }
            if (isMoving && direct == Direct.Down)
            {
                Gogogo(Vector3.back);
            }
        }
    }

    private void DetectSwipe_Left()
    {
        if (!isMoving)
        {
            isMoving = true;
            direct = Direct.Left;
        }
    }

    private void DetectSwipe_Right()
    {
        if (!isMoving)
        {
            isMoving = true;
            direct = Direct.Right;
        }
    }

    private void DetectSwipe_Up()
    {
        if (!isMoving)
        {
            isMoving = true;
            direct = Direct.Up;
        }
    }

    private void DetectSwipe_Down()
    {
        if (!isMoving)
        {
            isMoving = true;
            direct = Direct.Down;
        }
    }

    private void Gogogo(Vector3 vector3)
    {
        RaycastHit hit;
        LayerMask brickLayer = LayerMask.GetMask("Brick");
        if (Physics.Raycast(Cube.transform.position, vector3, out hit, 1f, brickLayer))
        {
            moveTarget = hit.collider.transform.position;
            moveTarget.y = rb.transform.position.y;
            rb.transform.position = Vector3.MoveTowards(rb.transform.position, moveTarget, speed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }
    }

    private void AddBrick(GameObject brickObj)
    {
        brickObj.gameObject.SetActive(false);
        countBrick++;
        inGameBrick++;

        GameObject clone_brick = Instantiate(brickObj);
        clone_brick.gameObject.SetActive(true);
        clone_brick.gameObject.tag = "Stack";

        clone_brick.transform.SetParent(TheStack.transform);

        clone_brick.transform.localPosition = new Vector3(Cube.transform.localPosition.x, Cube.transform.localPosition.y + sizeBrick * countBrick, Cube.transform.localPosition.z);

        People.transform.localPosition = new Vector3(People.transform.localPosition.x, People.transform.localPosition.y + sizeBrick, People.transform.localPosition.z);
    }

    private void RemoveBrick(GameObject brickObj)
    {
        if (countBrick > 0 && brickObj.gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            brickObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            brickObj.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            countBrick--;
            inGameBrick--;

            if (TheStack.transform.GetChild(countBrick + 1).gameObject.CompareTag("Stack"))
            {
                Destroy(TheStack.transform.GetChild(countBrick + 1).gameObject);
            }
            People.transform.localPosition = new Vector3(People.transform.localPosition.x, People.transform.localPosition.y - sizeBrick, People.transform.localPosition.z);
        }
    }


    private void ClearBrick()
    {

        if (TheStack.transform.childCount > 0)
        {
            for (int i = 0; i < TheStack.transform.childCount; i++)
            {
                if (TheStack.transform.GetChild(i).gameObject.CompareTag("Stack"))
                {
                    Destroy(TheStack.transform.GetChild(i).gameObject);
                    People.transform.localPosition = new Vector3(People.transform.localPosition.x, People.transform.localPosition.y - sizeBrick, People.transform.localPosition.z);
                }
            }
            countBrick = 0;
        }
    }

    private void WinPos(GameObject boxObj)
    {
        if (!isWin)
        {
            boxObj.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            boxObj.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            isWin = true;


            PlayerPrefs.SetInt(brick, inGameBrick);
            PlayerPrefs.Save();
        }

    }
}