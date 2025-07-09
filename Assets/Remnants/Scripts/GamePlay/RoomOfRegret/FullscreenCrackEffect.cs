using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenCrackEffect : MonoBehaviour
{
    [SerializeField] private GameObject crackQuad;              // ScreenCrackQuad
    [SerializeField] private string intensityProp = "_Intensity";

    [SerializeField] private float crackDuration = 0.5f;
    [SerializeField] private float holdDuration = 0.2f;

    private Material crackMat;
    private int intensityID;

    private void Awake()
    {       
            // 1) crackQuad 기본 캐싱
            if (crackQuad == null) { enabled = false; return; }

            // 2) Quad를 카메라 자식으로 붙였을 경우,
            //    크기를 자동으로 계산해서 화면 전체를 덮게 설정
            var cam = Camera.main;
            float near = cam.nearClipPlane + 0.01f;
            float h = 2f * near * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            float w = h * cam.aspect;
            crackQuad.transform.SetParent(cam.transform, false);
            crackQuad.transform.localPosition = Vector3.forward * near;
            crackQuad.transform.localRotation = Quaternion.identity;
            crackQuad.transform.localScale = new Vector3(w, h, 1f);

            // 3) ProceduralCracks 머티리얼 인스턴스화
            crackMat = Instantiate(crackQuad.GetComponent<MeshRenderer>().sharedMaterial);
            crackQuad.GetComponent<MeshRenderer>().material = crackMat;
            intensityID = Shader.PropertyToID("_Intensity");
            crackMat.SetFloat(intensityID, 0f);
        
    }

    public void PlayFullScreenCrack()
    {
        StartCoroutine(CrackRoutine());
    }

    private IEnumerator CrackRoutine()
    {
        // 1) 0→1로 크랙 세기
        float t = 0f;
        while (t < crackDuration)
        {
            t += Time.deltaTime;
            crackMat.SetFloat(intensityID, t / crackDuration);
            yield return null;
        }
        crackMat.SetFloat(intensityID, 1f);

        // 2) 잠깐 유지
        yield return new WaitForSeconds(holdDuration);

        // 3) 1→0으로 되돌리기
        t = 0f;
        while (t < crackDuration)
        {
            t += Time.deltaTime;
            crackMat.SetFloat(intensityID, 1f - t / crackDuration);
            yield return null;
        }
        crackMat.SetFloat(intensityID, 0f);
    }
}