using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour {
    public const float MOVE_AMOUNT = 10f;
    public const float MOVE_TIME = 0.1f;
    public enum Direction
    {
        Vertical,
        Horizontal,
    };
    public Direction direction;
    private ParticleSystem particles;
    private float distance;
    private float StartTime;
    private bool MovingForward;
    private Vector3 StartPos;
    public float StartDelay;
    private bool start;
    // Use this for initialization
    void Start () {
        particles = transform.parent.gameObject.transform.Find("Particle System").GetComponent<ParticleSystem>();
        distance = particles.startLifetime * particles.startSpeed;
        StartPos = transform.position;
        start = false;
        Invoke("StartMovement", StartDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (direction == Direction.Horizontal)
            {
                if (transform.parent.gameObject.transform.rotation.y != 0)
                {
                    if (MovingForward)
                        transform.position = Vector3.Lerp(StartPos, StartPos + new Vector3(0, 0, distance), (Time.time - StartTime) / (distance * MOVE_TIME));
                    else
                        transform.position = Vector3.Lerp(StartPos + new Vector3(0, 0, distance), StartPos, (Time.time - StartTime) / (distance * MOVE_TIME));
                }
                else
                {
                    if (MovingForward)
                        transform.position = Vector3.Lerp(StartPos, StartPos + new Vector3(distance * -1, 0, 0), (Time.time - StartTime) / (distance * MOVE_TIME));
                    else
                        transform.position = Vector3.Lerp(StartPos + new Vector3(distance * -1, 0, 0), StartPos, (Time.time - StartTime) / (distance * MOVE_TIME));
                }
            }
            else if (direction == Direction.Vertical)
            {
                if (MovingForward)
                    transform.position = Vector3.Lerp(StartPos, StartPos + new Vector3(0, distance, 0), (Time.time - StartTime) / (distance * MOVE_TIME));
                else
                    transform.position = Vector3.Lerp(StartPos + new Vector3(0, distance, 0), StartPos, (Time.time - StartTime) / (distance * MOVE_TIME));
            }
            if ((Time.time - StartTime) / (distance * MOVE_TIME) > 1.5)
                ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        MovingForward = !MovingForward;
        StartTime = Time.time;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("player"))
            col.gameObject.transform.SetParent(this.gameObject.transform);
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name.Equals("player"))
            col.gameObject.transform.SetParent(null);
    }

    void StartMovement()
    {
        ChangeDirection();
        start = true;
    }
}
