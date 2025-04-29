using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KayphoonStudio;

public class BoardSinglePiece : MonoBehaviour
{
    public GameObject EmptyPlaceHolder_Circle;
    public GameObject EmptyPlaceHolder_Cross;
    public GameObject RealPiece_Circle;
    public GameObject RealPiece_Cross;

    public PieceType playerType;
    public PieceType occupiedType;

    protected GameObject emptyPlaceholder => (playerType == PieceType.Circle ? EmptyPlaceHolder_Circle : EmptyPlaceHolder_Cross);
    //protected GameObject realPiece => (selfType == PieceType.Circle ? RealPiece_Circle : RealPiece_Cross);

    public static BoardSinglePiece currenltySelected;

    public bool isOccupied = false;
    public int rowIndex = -1;
    public int colIndex = -1;


    void Start()
    {
        KS_NotificationCenter.AddEventListener(NKeys.OnPlayerRaycastNoTarget, OnPlayerHoveringExit);
    }

    public void ResetSelf()
    {
        EmptyPlaceHolder_Circle.SetActive(false);
        EmptyPlaceHolder_Cross.SetActive(false);
        RealPiece_Circle.SetActive(false);
        RealPiece_Cross.SetActive(false);
        isOccupied = false;
        currenltySelected = null;
    }

    public void SetPlayerPieceType(PieceType playerType)
    {
        this.playerType = playerType;
    }

    public BoardSinglePiece OnPlayerHoveringEnter()
    {
        if (isOccupied)
        {
            return null;
        }

        if (currenltySelected != this && currenltySelected != null)
        {
            currenltySelected.OnPlayerHoveringExit();
        }

        currenltySelected = this;

        emptyPlaceholder.SetActive(true);

        return this;
    }

    public void OnPlayerHoveringExit()
    {
        if (currenltySelected == this)
        {
            currenltySelected = null;
        }

        emptyPlaceholder.SetActive(false);
    }

    public void OccupyWithPiece(PieceType piece)
    {
        occupiedType = piece;
        switch (piece)
        {
            case PieceType.Circle:
                RealPiece_Circle.SetActive(true);
                break;
            case PieceType.Cross:
                RealPiece_Cross.SetActive(true);
                break;
            default:
                KS_Logger.LogError("PieceType is None!", this);
                return;
        }
        isOccupied = true;
        OnPlayerHoveringExit();
    }
}


public enum PieceType
{
    None,
    Circle,
    Cross
}
