using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GravityController : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public Transform playerTransform;
    public Direction direction;

    public static GravityController instance;

    public enum Direction { Up, Down, Left, Right }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public void ChangeGravityDirection()
    {
        switch (direction)
        {
            case Direction.Up:
                transform.RotateAround(playerTransform.position, Vector3.right, 90f);
                break;
            case Direction.Down:
                transform.RotateAround(playerTransform.position, Vector3.left, 90f);
                break;
            case Direction.Left:
                transform.RotateAround(playerTransform.position, Vector3.forward, 90f);
                break;
            case Direction.Right:
                transform.RotateAround(playerTransform.position, Vector3.back, 90f);
                break;
        }
    }
}


