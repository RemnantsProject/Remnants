using UnityEngine;

public class CameraSuckEffect : MonoBehaviour
{
    public Transform target;          // 카메라가 도착할 위치
    public float duration = 0.5f;     // 빨려들어가는 시간
    public float targetFOV = 30f;     // 최종 시야각(줌인 연출)
    private float originalFOV;
    private Vector3 startPos;
    private Quaternion startRot;
    private bool isMoving = false;
    private float time;
    private Camera cam;
    public float shakeAmount = 0.08f;  // 셰이크 강도
    public float shakeFrequency = 30f; // 셰이크 속도
    void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam != null)
            originalFOV = cam.fieldOfView;
    }

    public void SuckIn()
    {
        if (target == null) return;
        startPos = transform.position;
        startRot = transform.rotation;
        time = 0f;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving || target == null) return;
        time += Time.deltaTime;
        float t = Mathf.Clamp01(time / duration);

        // 위치와 회전, FOV 보간
        Vector3 basePos = Vector3.Lerp(startPos, target.position, t);
        Quaternion baseRot = Quaternion.Lerp(startRot, target.rotation, t);

        // 흔들림 계산 (빨려들어갈수록 강도 감소)
        float shake = shakeAmount * (1f - t);
        Vector3 shakeOffset = Random.insideUnitSphere * shake;
        Quaternion shakeRot = Quaternion.Euler(
            Random.Range(-shake, shake),
            Random.Range(-shake, shake),
            0);

        // 실제 적용
        transform.position = basePos + shakeOffset;
        transform.rotation = baseRot * shakeRot;
        if (cam != null) cam.fieldOfView = Mathf.Lerp(originalFOV, targetFOV, t);

        if (t >= 1f) isMoving = false;
    }
}
