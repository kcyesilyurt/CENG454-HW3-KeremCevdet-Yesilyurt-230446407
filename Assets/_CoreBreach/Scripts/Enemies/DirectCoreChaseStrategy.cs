using UnityEngine;

public class DirectCoreChaseStrategy : IMovementStrategy
{
    public Vector3 GetMoveDirection(Transform enemyTransform, Transform targetTransform)
    {
        if (enemyTransform == null || targetTransform == null)
        {
            return Vector3.zero;
        }

        Vector3 direction = targetTransform.position - enemyTransform.position;
        direction.y = 0f;

        return direction.normalized;
    }
}