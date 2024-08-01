using UnityEngine;

public class BoneIndependence : MonoBehaviour
{
    public Transform independentBone;  // 독립적으로 움직일 본
    public Transform fixedBone;        // 고정될 본


    private Vector3 originalPosition;  // 고정될 본의 원래 위치
    private Quaternion originalRotation;  // 고정될 본의 원래 회전

    void Start()
    {
        // 고정될 본의 초기 위치와 회전을 저장합니다.
        if (fixedBone != null)
        {
            originalPosition = fixedBone.position;
            originalRotation = fixedBone.rotation;
        }
    }

    void LateUpdate()
    {
        // 매 업데이트 사이클마다 고정될 본을 원래의 위치와 회전으로 강제로 되돌립니다.
        if (fixedBone != null)
        {
            fixedBone.position = originalPosition;
            fixedBone.rotation = originalRotation;
        }
    }
}
