using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [Tooltip("A prefab, consisting of only a line renderer")]
    public GameObject lineRendererPrefab;
    [Tooltip("A what rate the wave expands. In meters per second")]
    public float expansionSpeed = 1;
    [Tooltip("A what rate the wave center travels in the direction. In meters per second. Ignored if the wave does not have a rigidbody")]
    public float travelSpeed = 1;
    [Tooltip("How long should the wave last")]
    public float duration = 5;
    [Tooltip("Direction of wave in degrees (0 is right)")]
    public float direction;
    [Tooltip("With what strength do the wave pushes dynamic/kinematic objects")]
    public float pushStrength;
    [Tooltip("How big an arc should the wave be.0-360(360 for full circle)")]
    public float arcDegrees = 180;
    [Tooltip("How many evenly distributed rays should be raycasted")]
    public float raysCount = 100;

    float rayLength = 1;
    float radius = 0;
    float time = 0;



    private List<WaveRenderer> renderers = new List<WaveRenderer>();

    private List<List<Vector2>> raycastPointsByRenderers = new List<List<Vector2>>(10);
    private int currRenderer;

    private Dictionary<float, bool> staticPointsAtAnglePreviouslyDetected = new Dictionary<float, bool>();
    
    void Start()
    {

        //TODO: implement as position animation to not require rigidbody anymore
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody != null) { 
            Vector2 velocity = Vector2.up;
            velocity.x *= travelSpeed;
            velocity.y *= travelSpeed;
            rigidbody.velocity = velocity;
        }
    }


    void Raycast()
    {
        //Dictionary<Rigidbody2D, Vector2> bodies = new Dictionary<Rigidbody2D, Vector2>();

        // raycast in directions varying from -halfArcDegrees/2 to +halfArcDegrees/2

        // set initialAngle to -halfArcDegrees/2
        float initialAngle = direction - (arcDegrees / 2);
        for (int i = 0; i < raysCount; i++)
        {
            // calculate the correct angle for each raycast
            float angle = initialAngle + i * (arcDegrees / raysCount);

            // if a static collision for this angle has been previously detected, ignore the point and do not raycast for it
            if (staticPointsAtAnglePreviouslyDetected.ContainsKey(angle))
            {
                StaticCollisionDetected();
                continue;
            }


            // raycast
            Vector2 direction = Vector2.up;
            Vector2 offset = new Vector2(direction.x * radius, direction.y * radius);
            RaycastHit2D ray = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), direction, rayLength);

            // ray didn't hit
            if (ray.point.x == 0 && ray.point.y == 0)
            {
                Vector2 hitPoint = new Vector2(direction.x * (radius + rayLength), direction.y * (radius + rayLength));
                NonStaticCollisionOrNoCollisionDetected(hitPoint);
            }

            // ray hit
            else
            {
                Vector2 hitPoint = new Vector2(ray.point.x - transform.position.x, ray.point.y - transform.position.y);
                if (ray.rigidbody != null)
                {
                    
                    // ray hit collider with static rigidbody
                    if (ray.rigidbody.bodyType == RigidbodyType2D.Static || ray.rigidbody.bodyType == RigidbodyType2D.Kinematic
                        )
                    {
                        StaticCollisionDetected();
                        // record that you collided with static body at that angle
                        staticPointsAtAnglePreviouslyDetected.Add(angle, true);
                    }
                    // ray hit collider with dynamic/kinematic rigidbody - apply force
                    else
                    {
                        Vector2 force = ray.normal;
                        force *= -1;
                        force *= pushStrength;
                        ray.rigidbody.AddForce(force, ForceMode2D.Force);

                        /*if (bodies.ContainsKey(ray.rigidbody) == true)
                            bodies[ray.rigidbody] = force;
                        else
                            bodies.Add(ray.rigidbody, force);*/

                        NonStaticCollisionOrNoCollisionDetected(hitPoint);
                    }
                    
                }
                // ray hit collider without rigidbody e.g. static
                else
                {
                    StaticCollisionDetected();

                    // record that you collided with static body at that angle
                    staticPointsAtAnglePreviouslyDetected.Add(angle, true);
                }
            }
        }



        /*var enumerator = bodies.GetEnumerator();
        while (enumerator.MoveNext())
        {
            print(enumerator.Current.Key.name, MathHelper.Vector2ToDegrees(enumerator.Current.Value));

            enumerator.Current.Key.AddForce(enumerator.Current.Value * pushStrength);
        }*/
    }

    /// <summary>
    /// Used to draw all points from the raycastPointsByRenderers List
    /// </summary>
    private void DrawLineRenderers() {

        // find out how many renderers do you need to draw the points
        int numberOfNeededRenderers = raycastPointsByRenderers.Count;

        int drawnRenderersCount = 0;
        
        // start drawing points with each renderer you have
        foreach (WaveRenderer renderer in renderers)
        {
            if (raycastPointsByRenderers.Count <= drawnRenderersCount)
            {
                renderer.SetPoints(null);
            }
            else
            {
                renderer.SetPoints(raycastPointsByRenderers[drawnRenderersCount]);
            }

            MaterialPropertyBlock block = new MaterialPropertyBlock();
            renderer.lineRenderer.GetPropertyBlock(block);
            Vector3 screenCoordinate = Camera.main.WorldToViewportPoint(this.transform.position);
            screenCoordinate.z = 0;
            block.SetVector("_Center", screenCoordinate);
            renderer.lineRenderer.SetPropertyBlock(block);

            drawnRenderersCount++;
        }

        // if you still need more renderers, add the rest as new child objects
        while (drawnRenderersCount < numberOfNeededRenderers)
        {
            GameObject newRenderer = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity, transform);
            newRenderer.transform.SetParent(this.transform);
            newRenderer.transform.position = this.transform.position;


            WaveRenderer waveRenderer = new WaveRenderer(newRenderer, raycastPointsByRenderers[drawnRenderersCount]);


            MaterialPropertyBlock block = new MaterialPropertyBlock();
            waveRenderer.lineRenderer.GetPropertyBlock(block);
            Vector3 screenCoordinate = Camera.main.WorldToScreenPoint(this.transform.position);
            screenCoordinate.z = 0;
            block.SetVector("_Center", screenCoordinate);
            waveRenderer.lineRenderer.SetPropertyBlock(block);


            renderers.Add(waveRenderer );
            drawnRenderersCount++;
        }
        
    }

    /// <summary>
    /// This is called for every non-static collision and is responsible for filling the points arrays every frame, that later are drawn from the LineRenderers
    /// </summary>
    /// <param name="point"></param>
    private void NonStaticCollisionOrNoCollisionDetected(Vector2 point)
    {
        // if it needs to start a new list
        if (raycastPointsByRenderers.Count <= currRenderer)
        {
            raycastPointsByRenderers.Add(new List<Vector2>((int)raysCount));
        }
        // add the point to the correct renderer list
        raycastPointsByRenderers[currRenderer].Add(point);
    }
    // increase the currRenderer variable if neccessary, to mark that you need to add points to new renderer from that point
    private void StaticCollisionDetected() {
        if (raycastPointsByRenderers.Count > currRenderer)
        {
            currRenderer++;
        }
    }


    private void Update()
    {
        // add time and increase radius by expansion speed
        time += Time.deltaTime;
        radius += Time.deltaTime * expansionSpeed;

        // reset variables
        raycastPointsByRenderers.Clear();
        currRenderer = 0;


        Raycast();
        DrawLineRenderers();

        // check if you have to destroy the wave
        if (time >= duration)
        {
            Destroy(gameObject);
        }
    }

    private class WaveRenderer
    {
        public GameObject renderer;
        public LineRenderer lineRenderer;
        public WaveRenderer(GameObject renderer)
        {
            this.renderer = renderer;
            this.lineRenderer = renderer.GetComponent<LineRenderer>();
        }
        public WaveRenderer(GameObject renderer, List<Vector2> points)
        {
            this.renderer = renderer;
            this.lineRenderer = renderer.GetComponent<LineRenderer>();
            SetPoints(points);
        }
        public void SetPoints(List<Vector2> points)
        {
            if(points == null)
            {
                this.lineRenderer.positionCount = 0;
                return;
            }
            this.lineRenderer.positionCount = points.Count;
            for (int i = 0; i < points.Count; i++)
            {
                this.lineRenderer.SetPosition(i, points[i]);
            }

        }


    }
}
