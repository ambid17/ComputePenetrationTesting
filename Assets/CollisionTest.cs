using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    Collider myCollider;

    private float eighthInch = 0.003175f;
    private float quarterInch = 0.009f;

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }

    // To test this...
    // I have setup the scene to already show a place where the logs output.
    // While the game is playing, try moving around the object this script is attached to.
    
    // With the physics debugger open, you can see there should be SO MANY more locations the objects should be colliding
    void Update()
    {
        SimpleCollisionTest();
    }

    private void SimpleCollisionTest()
    {
        Collider myCollider = gameObject.GetComponent<Collider>();
        // Get all of the nearby colliders
        // This finds the other object
        Collider[] overlaps = Physics.OverlapBox(myCollider.bounds.center, myCollider.bounds.extents);
        Collider[] overlaps2 = Physics.OverlapBox(myCollider.transform.position, myCollider.bounds.extents);

        if (overlaps.Length > 0 || overlaps2.Length > 0)
        {
            Debug.Log("found overlaps");
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
                Debug.Log($"found overlap between {myCollider.gameObject.name} and {overlapCollider.gameObject.name}");
            }
        }
    }
}
