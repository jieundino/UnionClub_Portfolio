using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    Vector3 cameraPosition;

    public Vector2 center;
    // 카메라가 플레이어 따라다닐 맵 사이즈
    public Vector2 mapSize;

    //카메라 움직이는 속도와 카메라 사이즈
    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;

    void Start()
    {
        // 플레이어를 찾아서 플레이어의 Transform 정보를 가져옴.
        this.player = GameObject.Find("Dust").GetComponent<Transform>();

        // 카메라의 사이즈 정해서 넣기.
        height = Camera.main.orthographicSize;  // 카메라의 높이 사이즈로 가져옴.
        // 카메라의 너비 사이즈 가져옴.
        width = height * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        LimitCameraArea();
    }

    void LimitCameraArea()
    {
        // Lerp 함수(선형 보간 기법)를 이용하여 카메라가 플레이어 따라가는 움직임이 부드러워짐.
        transform.position = Vector3.Lerp(transform.position,
                                          player.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed);
        // 카메라의 위치를 플레이어 위치에 카메라의 가운데 위치를 더해서 플레이어를 따라가게 함.

        // 변수가 일정한 값을 벗어나지 못하도록 Mathf.Clamp 함수 이용하여
        // 플레이어가 카메라의 최대 이동 벗어나 이동해도 플레이어를 정해진 맵 범위 내만 비출 수 있게 함.
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        // 제한된 범위 안으로 카메라의 위치 옮김.
        transform.position = new Vector3(clampX, clampY, -10f);
    }

    // 씬 창에서 맵 영역을 시각적으로 표현함.
    // 그래서 씬 창의 Gizmos를 활성해두고 이 표시를 보며 맵 사이즈를  적절하게 조절함.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
