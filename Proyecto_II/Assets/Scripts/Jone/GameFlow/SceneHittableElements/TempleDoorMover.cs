using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

// Jone Sainz Egea
// 05/05/2025
public class TempleDoorMover : MonoBehaviour, IMovableElement
{
    [SerializeField] private Transform newTransform;

    [SerializeField] NavMeshSurface beastNavMeshSurface;
    private NavMeshModifierVolume navMeshModifier;
    private NavMeshData navMeshData;

    [SerializeField] private int requiredID;
    public DialogManager dialogManager;

    private bool isLeverUnlocked = false;
    [SerializeField] LeverActionsTempleDoor leverReference;

    private bool isMoving = false;

    private void Awake()
    {
        navMeshModifier = GetComponent<NavMeshModifierVolume>();
        if (navMeshModifier == null)
        {
            Debug.LogWarning("The temple door doesn't have a NavMeshModifierVolume assigned.");
        }

        UpdateNavMesh();
    }

    private IEnumerator MoveTempleDoor(Transform target, float duration)
    {
        leverReference.isDoorOpen = true;
        
        isMoving = true;
        Quaternion startRotationTempleDoor = transform.rotation;
        Quaternion targetRotationTempleDoor = target.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // Normaliza el tiempo para interpolaci�n correcta
            transform.rotation = Quaternion.Slerp(startRotationTempleDoor, targetRotationTempleDoor, t);


            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotationTempleDoor;

        isMoving = false;

        UpdateNavMesh();
    }

    public void StartMoving(Vector3 target, float speed)
    {
        if (dialogManager.DialogIDRead(requiredID))
        {
            if (!isLeverUnlocked)
            {
                isLeverUnlocked = true;
                StartCoroutine(MoveTempleDoor(newTransform, speed));
            }
        }
        else
            return; // TODO: implement visible message for player
    } 
    public bool IsMoving() => isMoving;

    private void UpdateNavMesh()
    {
        StartCoroutine(TemporarilyUpdateNavMesh());
    }

    private IEnumerator TemporarilyUpdateNavMesh()
    {
        // Obtener todos los renderers con los tags correspondientes
        MeshRenderer[] toAdd = GameObject.FindGameObjectsWithTag("AddForNavMesh")
            .SelectMany(go => go.GetComponentsInChildren<MeshRenderer>(true)).ToArray();

        MeshRenderer[] toRemove = GameObject.FindGameObjectsWithTag("RemoveForNavMesh")
            .SelectMany(go => go.GetComponentsInChildren<MeshRenderer>(true)).ToArray();

        // Guardar estado original
        Dictionary<MeshRenderer, bool> originalStates = new Dictionary<MeshRenderer, bool>();
        foreach (var rend in toAdd)
        {
            originalStates[rend] = rend.enabled;
            rend.enabled = true; // Asegurar que est�n activos para ser considerados
        }
        foreach (var rend in toRemove)
        {
            originalStates[rend] = rend.enabled;
            rend.enabled = false; // Asegurar que no se incluyan en el bake
        }

        // Desactivar temporalmente el Modifier para que no interfiera
        navMeshModifier.enabled = false;

        // Espera 1 frame para asegurarse de que Unity registre los cambios
        yield return null;

        // Actualizar el NavMesh
        navMeshData = beastNavMeshSurface.navMeshData;
        beastNavMeshSurface.UpdateNavMesh(navMeshData);
        Debug.Log("NavMesh updated");

        // Restaurar el estado original
        foreach (var kvp in originalStates)
        {
            kvp.Key.enabled = kvp.Value;
        }
    }
}
