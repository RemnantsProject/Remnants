using UnityEngine;

public class SuckTrigger : MonoBehaviour
{
    public CameraSuckEffect cameraSuckEffect; // 메인카메라의 CameraSuckEffect
    public Transform target; // 카메라가 빨려들어갈 위치(빈 오브젝트 등)

    void OnMouseDown()
    {
        if (cameraSuckEffect != null && target != null)
        {
            cameraSuckEffect.target = target;
            cameraSuckEffect.SuckIn();
        }
    }
}
