using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footIK_Location : MonoBehaviour
{
    public Transform boneA; // 움직임의 기준이 될 본 A
    public Transform boneB; // 제약을 받을 본 B
    public Transform boneC;

    private Vector3 lastPositionA; // 이전 프레임에서 본 A의 위치

    void Start()
    {
        // 초기 위치 저장
        lastPositionA = boneA.position;
    }

    void Update()
    {
        // 본 A의 현재 위치와 이전 위치 사이의 차이 벡터 계산
        Vector3 deltaPosition = boneA.position - lastPositionA;

        // 본 B를 본 A의 움직임의 반대 방향으로 같은 거리만큼 이동
        boneB.position -= deltaPosition;
        boneC.position -= deltaPosition;

        // 본 A의 현재 위치를 다음 프레임을 위해 저장
        lastPositionA = boneA.position;
        
    }
}