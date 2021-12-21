using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    Collider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }

    // Youtube example video with Steps to Reproduce: https://www.youtube.com/watch?v=HgP-M-fAdFU
    void Update()
    {
        SimpleCollisionTest();
    }

    private void SimpleCollisionTest()
    {
        // Get all of the nearby colliders
        // This finds the other object
        Collider[] overlaps = Physics.OverlapBox(myCollider.bounds.center, myCollider.bounds.extents);

        if (overlaps.Length > 0)
        {
            Debug.Log("Found an overlapping object");
        }
        
        // For each of those colliders nearby, see if I'm actually colliding with them
        foreach (Collider overlapCollider in overlaps)
        {
            bool isOverlapping = Physics.ComputePenetration(
                myCollider, myCollider.transform.position, myCollider.transform.rotation,
                overlapCollider, overlapCollider.transform.position, overlapCollider.transform.rotation,
                out Vector3 direction, out float distance
            );

            // If we are overlapping something, and it isn't ourselves, simply log it
            if (isOverlapping && overlapCollider.gameObject != myCollider.gameObject)
            {
                Debug.Log($"Found penetration between {myCollider.gameObject.name} and {overlapCollider.gameObject.name}");
            }
        }
    }
}
