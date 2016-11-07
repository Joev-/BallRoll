using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float SpeedMultiplier;
    public int Score = 0;

    private bool mCanMove = true;
    private Vector3 mStartPos;
    private Rigidbody mRigidBody;
    private Transform mTransform;

    public delegate void ScoreIncreased();
    public event ScoreIncreased OnScoreIncreased;

    public void OnEnable()
    {
        mTransform = GetComponent<Transform>();
        mRigidBody = GetComponent<Rigidbody>();
        mStartPos = mTransform.position;
    }

    public void FixedUpdate()
    {
        if (mCanMove)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveX, 0.0f, moveZ);

            mRigidBody.AddForce(movement * SpeedMultiplier);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            PickedUpItem();
        }
    }

    public void ResetPlayer()
    {
        mCanMove = true;
        mTransform.position = mStartPos;
        mRigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        mRigidBody.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void SetEnabled(bool enabled)
    {
        mCanMove = enabled;
    }

    private void PickedUpItem()
    {
        ++Score;
        OnScoreIncreased();
    }
}
