using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Oculus.Avatar2.CAPI;
using UnityEngine.SocialPlatforms;

public class Racket : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;

    Rigidbody ball;
    [SerializeField] private Transform trackingSpace, leftHand;
    [SerializeField] private float forceMultiplier;
    [SerializeField] private Collider collider;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            if (ball != null)
            {
                Destroy(ball.gameObject);
            }

            ball = Instantiate(ballPrefab,leftHand).GetComponent<Rigidbody>();
            ball.gameObject.SetActive(true);
            ball.transform.localPosition = Vector3.zero;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            if (ball == null) return;
            ball.transform.parent = null;
            ball.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            var vel = trackingSpace.transform.TransformVector(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
            Vector3 contactPoint = collision.ClosestPoint(transform.position);
            Vector3 normal = (ball.position - contactPoint).normalized;
            ball.AddForce(normal * vel.magnitude * forceMultiplier);
            //StartCoroutine(DisableCollider());
        }
    }
    IEnumerator DisableCollider()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(0.25f);
        collider.enabled = true;
    }
}
