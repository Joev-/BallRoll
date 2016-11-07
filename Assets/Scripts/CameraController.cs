using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    private bool mMoveable = true;
    private Vector3 mOffset;

    void Start()
    {
        mOffset = transform.position - Player.transform.position;
    }

    void LateUpdate()
    {
        if (mMoveable)
        {
            transform.position = Player.transform.position + mOffset;
        }
    }

    public void SetMoveable(bool canMove)
    {
        mMoveable = canMove;
    }
}
