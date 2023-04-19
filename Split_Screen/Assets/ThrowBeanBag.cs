using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static Oculus.Avatar2.CAPI;

public class ThrowBeanBag : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SampleAvatarEntity local, mirror;
    [SerializeField] Rigidbody blackPrefab, whitePrefab;

    [SerializeField] Rigidbody blackLocal, blackMirror, whiteLocal, whiteMirror;
    [SerializeField] Transform trackingSpace;
    [SerializeField] float force = 10;

    [SerializeField] Transform localIndexLeft, localIndexRight, mirrorIndexLeft, mirrorIndexRight;

    [SerializeField] Material LeftWhiteMat, LeftBlackMat, RightWhiteMat, RightBlackMat;

    
    void Start()
    {
    }

    
    

    // Update is called once per frame
    void Update()
    {
        if (localIndexLeft == null || localIndexRight == null || mirrorIndexLeft == null || mirrorIndexRight == null)
        {
            localIndexLeft = local.GetSkeletonTransform(ovrAvatar2JointType.LeftHandIndexDistal);
            localIndexRight = local.GetSkeletonTransform(ovrAvatar2JointType.RightHandIndexDistal);
            mirrorIndexLeft = mirror.GetSkeletonTransform(ovrAvatar2JointType.LeftHandIndexDistal);
            mirrorIndexRight = mirror.GetSkeletonTransform(ovrAvatar2JointType.RightHandIndexDistal);
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            whiteLocal = Instantiate(whitePrefab, local.GetSkeletonTransform(ovrAvatar2JointType.LeftHandWrist));
            whiteMirror = Instantiate(whitePrefab, mirror.GetSkeletonTransform(ovrAvatar2JointType.LeftHandWrist));
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            blackLocal = Instantiate(blackPrefab, local.GetSkeletonTransform(ovrAvatar2JointType.RightHandWrist));
            blackMirror = Instantiate(blackPrefab, mirror.GetSkeletonTransform(ovrAvatar2JointType.RightHandWrist));
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            whiteLocal.transform.SetParent(null);
            whiteMirror.transform.SetParent(null);
            whiteLocal.isKinematic = false;
            whiteMirror.isKinematic = false;
            var vel = trackingSpace.transform.TransformVector(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch));

            whiteLocal.AddForce(vel * force);
            vel.z = -vel.z;
            whiteMirror.AddForce(vel * force);

        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            blackLocal.transform.SetParent(null);
            blackMirror.transform.SetParent(null);

            blackLocal.isKinematic = false;
            blackMirror.isKinematic = false;

            var vel = trackingSpace.transform.TransformVector(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
            blackLocal.AddForce(vel * force);
            vel.z = -vel.z;
            blackMirror.AddForce(vel * force);
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            
            whiteLocal = Instantiate(whitePrefab, localIndexLeft.position, localIndexLeft.rotation);
            whiteLocal.GetComponent<MeshRenderer>().material = LeftWhiteMat;
            whiteLocal.isKinematic = false;
            whiteLocal.useGravity = false;
            whiteLocal.velocity = local.GetSkeletonTransform(ovrAvatar2JointType.LeftHandWrist).right;
            blackMirror = Instantiate(blackPrefab, mirrorIndexLeft.position, mirrorIndexLeft.rotation);
            blackMirror.GetComponent<MeshRenderer>().material = LeftBlackMat;
            blackMirror.velocity = -local.GetSkeletonTransform(ovrAvatar2JointType.LeftHandWrist).right;
            blackMirror.isKinematic = false;
            blackMirror.useGravity = false;

        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            blackLocal = Instantiate(blackPrefab, localIndexRight.position, localIndexRight.rotation);
            blackLocal.GetComponent<MeshRenderer>().material = RightBlackMat;
            blackLocal.isKinematic = false;
            blackLocal.useGravity = false;
            blackLocal.velocity = -local.GetSkeletonTransform(ovrAvatar2JointType.RightHandWrist).right;
            whiteMirror = Instantiate(whitePrefab, mirrorIndexRight.position, mirrorIndexRight.rotation);
            whiteMirror.GetComponent<MeshRenderer>().material = RightWhiteMat;
            whiteMirror.isKinematic = false;
            whiteMirror.useGravity = false;
            whiteMirror.velocity = local.GetSkeletonTransform(ovrAvatar2JointType.RightHandWrist).right;
        }
    }
}
