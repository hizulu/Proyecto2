using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Jone Sainz Egea
// 15/04/2025
public class Beast : MonoBehaviour
{

    [Header("Componentes")]
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Animator anim;
    //[SerializeField] public Rigidbody rb;
    [SerializeField] public Transform playerTransform;
    [SerializeField] public Transform mountPoint;

    [Header("Par�metros")]
    [SerializeField] public float arrivalThreshold = 5f;
    [SerializeField] public float freeRoamRadius = 30f;
    [SerializeField] public float interactionThreshold = 8f;

    private BeastState currentState;

    public Blackboard blackboard { get; private set; }

    private Coroutine activeCoroutine; // Para gestionar que solo haya una corrutina en marcha a la vez
    private ICoroutineNode coroutineOwner;

    private void Awake()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (anim == null) anim = GetComponent<Animator>();
        if (blackboard == null) blackboard = new Blackboard();

        // Comenzamos en estado de libertad
        TransitionToState(new BeastFreeState());

        EventsManager.CallNormalEvents("AcariciarBestia_Bestia", PetBeastSelected);
        EventsManager.CallNormalEvents("SanarBestia_Bestia", HealBeastSelected);
        EventsManager.CallNormalEvents("AtaqueBestia_Bestia", AttackBeastSelected);
        EventsManager.CallNormalEvents("MontarBestia_Bestia", MountBeastSelected);
        EventsManager.CallNormalEvents("AccionBestia_Bestia", ActionBeastSelected);
    }

    private void OnDestroy()
    {
        EventsManager.StopCallNormalEvents("AcariciarBestia_Bestia", PetBeastSelected);
        EventsManager.StopCallNormalEvents("SanarBestia_Bestia", HealBeastSelected);
        EventsManager.StopCallNormalEvents("AtaqueBestia_Bestia", AttackBeastSelected);
        EventsManager.StopCallNormalEvents("MontarBestia_Bestia", MountBeastSelected);
        EventsManager.StopCallNormalEvents("AccionBestia_Bestia", ActionBeastSelected);
    }

    private void Update()
    {
        currentState?.OnUpdate(this);
    }

    public void TransitionToState(BeastState newState)
    {
        currentState?.OnExit(this);

        // Para asegurar que se realizan las acciones de fin de corrutina al cambiar de estado:
        if (activeCoroutine != null)
            coroutineOwner?.OnCoroutineEnd();

        currentState = newState;

        currentState?.OnEnter(this);
    }

    // Gesti�n de corrutinas de la Bestia
    public void StartNewCoroutine(IEnumerator routine, ICoroutineNode owner)
    {
        if (activeCoroutine != null)
            coroutineOwner?.OnCoroutineEnd();

        coroutineOwner = owner;
        activeCoroutine = StartCoroutine(routine);
        blackboard.SetValue("isCoroutineActive", true);
    }

    // Called from Brisa script
    public void CallBeast()
    {
        blackboard.SetValue("isConstrained", true);
        agent.ResetPath();

        TransitionToState(new BeastConstrainedState());

        Debug.Log("Bestia llamada por el jugador");
    }

    public void OpenBeastMenu()
    {
        // Por si se abre el men� sin estar en estado de constrained
        agent.ResetPath();
        blackboard.SetValue("menuOpenedFromOtherState", true);
        blackboard.SetValue("isConstrained", true);       
    }

    public void ResetBeastSelection()
    {
        // Resetear todos los valores
        blackboard.SetValue("isOptionPet", false);
        blackboard.SetValue("isOptionHeal", false);
        blackboard.SetValue("isOptionAttack", false);
        blackboard.SetValue("isOptionMount", false);
        blackboard.SetValue("isOptionAction", false);
    }

    public void PetBeastSelected()
    {
        ResetBeastSelection();
        blackboard.SetValue("isOptionPet", true);
        Debug.Log("Ha seleccionado pet");
    }

    public void HealBeastSelected()
    {
        ResetBeastSelection();
        blackboard.SetValue("isOptionHeal", true);
        Debug.Log("Ha seleccionado heal");
    }

    public void AttackBeastSelected()
    {
        ResetBeastSelection();
        blackboard.SetValue("isOptionAttack", true);
        Debug.Log("Ha seleccionado heal");
    }

    public void MountBeastSelected()
    {
        ResetBeastSelection();
        blackboard.SetValue("isOptionMount", true);
        Debug.Log("Ha seleccionado heal");
    }

    public void ActionBeastSelected()
    {
        ResetBeastSelection();
        blackboard.SetValue("isOptionAction", true);
        Debug.Log("Ha seleccionado heal");
    }

    public bool IsPlayerWithinInteractionDistance()
    {
        return Vector3.Distance(transform.position, playerTransform.position) < interactionThreshold;
    }
}
