using UnityEngine;


	/// <summary>
	/// This component is used to create a patrol path, two points which enemies will move between.
	/// </summary>
	public partial class PatrolPath : MonoBehaviour
	{
    public Vector2 startPosition, endPosition;
    public Color gizmoColor = Color.yellow; // color for the patrol path

    public Mover CreateMover(float speed = 1) => new Mover(this, speed);

    void Reset()
    {
        startPosition = Vector3.left;
        endPosition = Vector3.right;
    }

    // Visualize the patrol path with Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(startPosition, endPosition);
        Gizmos.DrawSphere(startPosition, 0.1f);
        Gizmos.DrawSphere(endPosition, 0.1f);
    }
    //	/// <summary>
    //	/// One end of the patrol path.
    //	/// </summary>
    //	public Vector2 startPosition, endPosition;

    //	/// <summary>
    //	/// Create a Mover instance which is used to move an entity along the path at a certain speed.
    //	/// </summary>
    //	/// <param name="speed"></param>
    //	/// <returns></returns>
    //	public Mover CreateMover(float speed = 1) => new Mover(this, speed);

    //	void Reset()
    //	{
    //		startPosition = Vector3.left;
    //		endPosition = Vector3.right;
    //	}

}
