using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * NOMBRE SCRIPT: Player
 * AUTOR: Sara Yue Madruga Martín
 * FECHA: 09/03/2025
 * DESCRIPCIÓN: Script que gestiona toda la lógica de la máquina de estado con el modelo 3D del juego.
 * VERSIÓN: 1.0. 
 */
public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Collisions")]
    [field: SerializeField] public PlayerLayerData LayerData { get; private set; }

    [field: Header("Animaciones")]
    [field: SerializeField] public PlayerAnimationData PlayerAnimationData { get; private set; }

    [SerializeField] public GameObject PaloBrisa;
    [SerializeField] public GameObject hitBox;
    [SerializeField] private float attackDuration = 0.2f;
    public Rigidbody RbPlayer { get; private set; }
    public BoxCollider GroundCheckCollider { get; private set; }
    public Animator AnimPlayer { get; private set; }
    public CinemachineVirtualCamera CamPlayer { get; private set; }

    private PlayerStateMachine playerStateMachine;

    public PlayerInput PlayerInput { get; private set; }

    protected PlayerStatsData statsData;

    private void Awake()
    {
        PlayerAnimationData.Initialize();

        RbPlayer = GetComponent<Rigidbody>();

        GroundCheckCollider = GetComponentInChildren<BoxCollider>();

        AnimPlayer = GetComponent<Animator>();

        PlayerInput = GetComponent<PlayerInput>();

        playerStateMachine = new PlayerStateMachine(this);

        PlayerInput.PlayerActions.Inventory.performed += OpenCloseInventory;

        statsData = Data.StatsData;
    }

    private void OnDestroy()
    {
        PlayerInput.PlayerActions.Inventory.performed -= OpenCloseInventory;
    }

    void Start()
    {
        playerStateMachine.ChangeState(playerStateMachine.IdleState);
        PaloBrisa.SetActive(false);
        hitBox.SetActive(false);
        ResetHealth();
    }
    private void FixedUpdate()
    {
        playerStateMachine.UpdatePhysics();
    }

    private void Update()
    {
        playerStateMachine.HandleInput();
        playerStateMachine.UpdateLogic();
    }

    private void OnTriggerEnter(Collider collider)
    {
        playerStateMachine.OnTriggerEnter(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        playerStateMachine.OnTriggerExit(collider);
    }

    public void OpenCloseInventory(InputAction.CallbackContext context)
    {
        InventoryManager.Instance.OpenCloseInventory(context);
    }

    public void TakeDamage(float _enemyDamage)
    {
        statsData.CurrentHealth -= _enemyDamage;
        Debug.Log(statsData.CurrentHealth);
        if (statsData.CurrentHealth <= 0)
            Debug.Log("murió");
            //stateMachine.ChangeState(stateMachine.HalfDeadState);
    }

    #region Métodos temporales (No se sabe si se quedarán en el script de Player o se moverán).

    public void GolpearPrueba()
    {
        StartCoroutine(EnableHitBox());
    }

    private IEnumerator EnableHitBox()
    {
      hitBox.SetActive(true); // Activa el hitbox
      yield return new WaitForSeconds(attackDuration);
      hitBox.SetActive(false); // Lo desactiva después de un tiempo
    }

    public void PaloRecogido()
    {
        PaloBrisa.SetActive(true);
    }

    public void ResetHealth()
    {
        Data.StatsData.CurrentHealth = Data.StatsData.MaxHealth;
        //Debug.Log("Se ha reseteado la vida" + Data.StatsData.CurrentHealth);
    }
    #endregion
}
