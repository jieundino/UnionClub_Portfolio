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
    // ī�޶� �÷��̾� ����ٴ� �� ������
    public Vector2 mapSize;

    //ī�޶� �����̴� �ӵ��� ī�޶� ������
    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;

    void Start()
    {
        // �÷��̾ ã�Ƽ� �÷��̾��� Transform ������ ������.
        this.player = GameObject.Find("Dust").GetComponent<Transform>();

        // ī�޶��� ������ ���ؼ� �ֱ�.
        height = Camera.main.orthographicSize;  // ī�޶��� ���� ������� ������.
        // ī�޶��� �ʺ� ������ ������.
        width = height * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        LimitCameraArea();
    }

    void LimitCameraArea()
    {
        // Lerp �Լ�(���� ���� ���)�� �̿��Ͽ� ī�޶� �÷��̾� ���󰡴� �������� �ε巯����.
        transform.position = Vector3.Lerp(transform.position,
                                          player.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed);
        // ī�޶��� ��ġ�� �÷��̾� ��ġ�� ī�޶��� ��� ��ġ�� ���ؼ� �÷��̾ ���󰡰� ��.

        // ������ ������ ���� ����� ���ϵ��� Mathf.Clamp �Լ� �̿��Ͽ�
        // �÷��̾ ī�޶��� �ִ� �̵� ��� �̵��ص� �÷��̾ ������ �� ���� ���� ���� �� �ְ� ��.
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        // ���ѵ� ���� ������ ī�޶��� ��ġ �ű�.
        transform.position = new Vector3(clampX, clampY, -10f);
    }

    // �� â���� �� ������ �ð������� ǥ����.
    // �׷��� �� â�� Gizmos�� Ȱ���صΰ� �� ǥ�ø� ���� �� �����  �����ϰ� ������.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
