using UnityEngine;


public interface IMovementStrategy
{
    Vector3 GetMoveDirection(Transform enemyTransform, Transform targetTransform);
}