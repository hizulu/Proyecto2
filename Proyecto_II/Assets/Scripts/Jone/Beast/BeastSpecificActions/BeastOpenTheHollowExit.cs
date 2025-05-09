using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jone Sainz Egea
// 18/04/2025
public class BeastOpenTheHollowExit : BeastActionable
{
    [SerializeField] GameObject camGO;
    private CameraFade cam;

    private void Start()
    {
        cam = camGO.GetComponent<CameraFade>();
    }

    public override void OnBeast()
    {
        if (!beastIsIn)
        {
            Debug.Log("Beast is not in");
            return;
        }
        StartCoroutine(FadeToNextScene());
    }

    IEnumerator FadeToNextScene()
    {
        // TODO: some animation
        cam.DoFadeInOut();
        yield return new WaitForSeconds(1f);
        GameManager.Instance.LoadNextScene();
    }
}
