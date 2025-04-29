using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KayphoonStudio;

public class PlayerRaycast : MonoBehaviour
{
    public Transform rayStartPoint;
    public LayerMask layer;
    public float rayLength;


    public bool playerTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        KS_NotificationCenter.AddEventListener(NKeys.OnPlayerTurn, OnPlayerTurn);
    }

    void OnPlayerTurn()
    {
        playerTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerTurn) return;

        RaycastHit hit;
        BoardSinglePiece currentSelectedPiece = null;

        if (Physics.Raycast(rayStartPoint.position, rayStartPoint.forward, out hit, rayLength, layer))
        {
            BoardSinglePiece hitPiece = hit.collider.GetComponent<BoardSinglePiece>();
            if (hitPiece != null)
            {
                currentSelectedPiece = hitPiece.OnPlayerHoveringEnter();
            }
            else
            {
                KS_Logger.LogError("No BoardSinglePiece component on object " + hit.collider.name);
            }
        }
        else
        {
            KS_NotificationCenter.DispatchEvent(NKeys.OnPlayerRaycastNoTarget);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentSelectedPiece == null) return;

            TicTacToeBoard.Instance.PlayerPutPiece(currentSelectedPiece);
            playerTurn = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rayStartPoint.position, rayLength * rayStartPoint.forward);
    }
}
