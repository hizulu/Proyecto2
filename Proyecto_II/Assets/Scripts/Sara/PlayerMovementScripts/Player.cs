using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * NOMBRE SCRIPT: Player
 * AUTOR: Sara Yue Madruga Mart�n
 * FECHA: 09/03/2025
 * DESCRIPCI�N: Script que gestiona toda la l�gica de la m�quina de estado con el modelo 3D del juego.
 * VERSI�N: 1.0. 
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
    //public CinemachineVirtualCamera CamPlayer { get; private set; }

    private PlayerStateMachine playerStateMachine;

    public PlayerInput PlayerInput { get; private set; }

    [SerializeField] private BeastSelectionPanel beastPanel;
    [SerializeField] private MapPanel mapPanel;

    [SerializeField] public CinemachineVirtualCamera playerCam;
    public CinemachinePOV CamComponents;
    [SerializeField] public GameObject AreaMoveBeast;
    [SerializeField] public GameObject CursorMarker;

    private void Awake()
    {
        PlayerAnimationData.Initialize();

        RbPlayer = GetComponent<Rigidbody>();

        GroundCheckCollider = GetComponentInChildren<BoxCollider>();

        AnimPlayer = GetComponent<Animator>();

        PlayerInput = GetComponent<PlayerInput>();

        //CamComponents = playerCam.GetCinemachineComponent<CinemachinePOV>();

        playerStateMachine = new PlayerStateMachine(this);

        PlayerInput.UIPanelActions.Inventory.performed += OpenCloseInventory;
        PlayerInput.UIPanelActions.ClosePanelGeneral.performed += OpenCloseInventory;
        PlayerInput.UIPanelActions.BeastPanel.performed += OpenCloseBeastPanel;
        PlayerInput.UIPanelActions.Map.performed += OpenCloseMapPanel;
    }

    private void OnDestroy()
    {
        PlayerInput.UIPanelActions.Inventory.performed -= OpenCloseInventory;
        PlayerInput.UIPanelActions.ClosePanelGeneral.performed -= OpenCloseInventory;
        PlayerInput.UIPanelActions.BeastPanel.performed -= OpenCloseBeastPanel;
        PlayerInput.UIPanelActions.Map.performed -= OpenCloseMapPanel;
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

    public void OpenCloseBeastPanel(InputAction.CallbackContext context)
    {
        beastPanel.OpenCloseBeastPanel(context);
    }

    public void OpenCloseMapPanel(InputAction.CallbackContext context)
    {
        if (mapPanel == null)
        {
            Debug.LogError("mapPanel is null! Please assign it in the inspector.");
            return;
        }
        mapPanel.OpenCloseMapPanel(context);
    }

        #region M�todos temporales (No se sabe si se quedar�n en el script de Player o se mover�n).

        public void GolpearPrueba()
    {
        StartCoroutine(EnableHitBox());
    }

    private IEnumerator EnableHitBox()
    {
      hitBox.SetActive(true); // Activa el hitbox
      yield return new WaitForSeconds(attackDuration);
      hitBox.SetActive(false); // Lo desactiva despu�s de un tiempo
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
