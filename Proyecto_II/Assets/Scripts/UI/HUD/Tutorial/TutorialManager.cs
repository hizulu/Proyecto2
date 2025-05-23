using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// Jone Sainz Egea
// 18/04/2025
// Clase que se encarga del control de los mensajes de tutorial activos
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [SerializeField] private GameObject tutorialMessagePrefab;
    [SerializeField] private Transform canvasParent;
    [SerializeField] private GameObject tutorialsEmpty;

    public InputActionAsset inputActions;
    private List<TutorialMessage> activeMessages = new List<TutorialMessage>();
    private List<TutorialTrigger> tutorialTriggers = new List<TutorialTrigger>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public TutorialMessage ShowMessage(Tutorial tutorial)
    {
        GameObject obj = Instantiate(tutorialMessagePrefab, canvasParent);
        TutorialMessage msg = obj.GetComponent<TutorialMessage>();

        StartCoroutine(FadeCanvasGroup(msg.CanvasGroup, 0f, 1f, 0.25f, 0.5f));
        activeMessages.Add(msg);

        return msg;
    }

    public void RemoveMessage(TutorialMessage message)
    {
        if (activeMessages.Contains(message))
            activeMessages.Remove(message);
    }

    public IEnumerator FadeOutAndDestroy(TutorialMessage msg)
    {
        yield return StartCoroutine(FadeCanvasGroup(msg.CanvasGroup, 1f, 0f, 0.4f, 0.2f));

        yield return null;

        if (msg == null)
            yield break;    
            
        Destroy(msg.gameObject);

        yield return null;
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup group, float from, float to, float duration, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        float t = 0f;

        if (group != null)
            group.alpha = from;

        while (t < duration)
        {
            if (group == null) yield break;
            group.alpha = Mathf.Lerp(from, to, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        if (group != null)
            group.alpha = to;
    }

    public void DeactivateTutorials()
    {
        tutorialTriggers = new List<TutorialTrigger>(FindObjectsOfType<TutorialTrigger>());

        foreach (var tutorialTrigger in tutorialTriggers)
        {
            tutorialTrigger.HasBeenCanceled();
        }

        foreach (TutorialMessage msg in activeMessages)
        {
            RemoveMessage(msg);
        }

        List<GameObject> tutorialMessages = new List<GameObject>();

        foreach (Transform child in canvasParent)
        {
            tutorialMessages.Add(child.gameObject);
        }

        foreach (GameObject item in tutorialMessages)
        {
            item.SetActive(false);
        }

        tutorialsEmpty.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
