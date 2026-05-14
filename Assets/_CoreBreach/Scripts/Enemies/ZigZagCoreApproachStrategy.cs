using UnityEngine;

public class ZigZagCoreApproachStrategy : IMovementStrategy
{
    private readonly float zigZagStrength;
    private readonly float zigZagFrequency;

    public ZigZagCoreApproachStrategy(float zigZagStrength, float zigZagFrequency)
    {
        this.zigZagStrength = zigZagStrength;
        this.zigZagFrequency = zigZagFrequency;
    }

    public Vector3 GetMoveDirection(Transform enemyTransform, Transform targetTransform)
    {
        if (enemyTransform == null || targetTransform == null)
        {
            return Vector3.zero;
        }

        Vector3 toTarget = targetTransform.position - enemyTransform.position;
        toTarget.y = 0f;

        Vector3 forwardDirection = toTarget.normalized;
        Vector3 sideDirection = Vector3.Cross(Vector3.up, forwardDirection).normalized;

        float zigZagOffset = Mathf.Sin(Time.time * zigZagFrequency) * zigZagStrength;
        Vector3 finalDirection = forwardDirection + sideDirection * zigZagOffset;

        return finalDirection.normalized;
    }
}