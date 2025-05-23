using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Jone Sainz Egea
// 17/04/2025
// Clase que gestiona el estado de montar a la bestia, en la que la bestia pasa a estar bajo el control del jugador
public class BeastMountedState : BeastState
{
    private Vector3 lastPosition;
    private bool wasWalking = false;
    private float stillTimer = 0f;
    private const float movementThreshold = 0.01f;
    private const float stopDelay = 0.2f; // Tiempo m�nimo sin moverse antes de detener animaci�n

    public override void OnEnter(Beast beast)
    {
        Debug.Log("Entering BeastMountedState, Brisa taking over Beast control...");
        EventsManager.TriggerNormalEvent("MontarBestia_Player");
        beast.agent.enabled = false;

        // Subir al jugador para que al colocar la bestia no atraviese el suelo
        beast.transform.SetParent(beast.mountPoint);
        beast.transform.localPosition = Vector3.zero;
        beast.transform.localRotation = Quaternion.identity;

        float alturaBestia = 4f;
        beast.playerTransform.position += new Vector3(0, alturaBestia, 0);

        lastPosition = beast.transform.position;
    }
    public override void OnUpdate(Beast beast)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Tab))             //TODO: sustituirlo por NEW INPUT SYSTEM
        {
            beast.TransitionToState(new BeastFreeState());
        }

        float distanceMoved = Vector3.Distance(beast.transform.position, lastPosition);
        bool isMoving = distanceMoved > movementThreshold;

        if (isMoving)
        {
            stillTimer = 0f;

            if (!wasWalking)
            {
                beast.anim.SetBool("isRunning", true);
                wasWalking = true;
            }
        }
        else
        {
            stillTimer += Time.deltaTime;

            if (wasWalking && stillTimer >= stopDelay)
            {
                beast.anim.SetBool("isRunning", false);
                wasWalking = false;
            }
        }

        lastPosition = beast.transform.position;
    }
    public override void OnExit(Beast beast)
    {
        Debug.Log("Leaving BeastMountedState, AI taking over Beast control");
        beast.transform.SetParent(null);
        Vector3 posPlayerDismount = beast.transform.position - beast.transform.right * 2f + Vector3.up * 3f;
        beast.playerTransform.position = posPlayerDismount;
        beast.agent.enabled = true;
        EventsManager.TriggerNormalEvent("EnsureBrisaDismounts");
        //beast.rb.constraints = RigidbodyConstraints.None;

        beast.anim.SetBool("isRunning", false);
    }
}
