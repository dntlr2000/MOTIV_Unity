using UnityEngine;

public class MaintainBoneIndependence : MonoBehaviour
{
    public Transform controlledBone; // 움직일 본
    public Transform independentBone1; // 영향 받지 않을 첫 번째 본
    public Transform independentBone2; // 영향 받지 않을 두 번째 본

    private Vector3 initialPosition1;
    private Quaternion initialRotation1;
    private Vector3 initialPosition2;
    private Quaternion initialRotation2;

    void Start()
    {
        // 독립 본들의 초기 위치와 회전을 저장
        if (independentBone1 != null)
        {
            initialPosition1 = independentBone1.position;
            initialRotation1 = independentBone1.rotation;
        }
        if (independentBone2 != null)
        {
            initialPosition2 = independentBone2.position;
            initialRotation2 = independentBone2.rotation;
        }
    }

    void Update()
    {
        // 매 프레임마다 독립 본들을 원래의 위치와 회전으로 강제로 복원
        if (independentBone1 != null)
        {
            independentBone1.position = initialPosition1;
            independentBone1.rotation = initialRotation1;
        }
        if (independentBone2 != null)
        {
            independentBone2.position = initialPosition2;
            independentBone2.rotation = initialRotation2;
        }
    }
}
