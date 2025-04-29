using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using KayphoonStudio;

public class TicTacToeBoard : KS_Singleton<TicTacToeBoard>
{
    public List<BoardSinglePiece> Pieces;
    public PieceType PlayerType;
    public PieceType AiType;
    
    public UnityEvent OnPlayerPiecePlaced;
    public UnityEvent OnAiPiecePlaced;
    public UnityEvent OnGameEnd;

    public GameObject PlayerCirclePieces;
    public GameObject PlayerCrossPieces;
    public GameObject AiCirclePieces;
    public GameObject AiCrossPieces;
    

    // Winning combinations: indices into Pieces list
    private readonly int[][] winCombos = new int[][] {
        new[] {0,1,2}, new[] {3,4,5}, new[] {6,7,8}, // rows
        new[] {0,3,6}, new[] {1,4,7}, new[] {2,5,8}, // columns
        new[] {0,4,8}, new[] {2,4,6}                 // diagonals
    };

    void Start()
    {
        KS_Timer.DelayedAction(1f, () =>
        {
            // Set the player type to Circle by default
            PlayerSelectPieceType(PieceType.Circle);
        });
    }

    [ContextMenu("Set Circle")]
    public void DebugSetPlayerCircle()
    {
        PlayerSelectPieceType(PieceType.Circle);
    }

    public void SwapPlayerType()
    {
        PlayerSelectPieceType(PlayerType == PieceType.Circle ? PieceType.Cross : PieceType.Circle);
    }

    public void PlayerSelectPieceType(PieceType piece)
    {
        PlayerType = piece;
        AiType = PlayerType == PieceType.Circle ? PieceType.Cross : PieceType.Circle;

        // set all piece's type for UI or highlighting
        foreach (BoardSinglePiece p in Pieces)
        {
            p.SetPlayerPieceType(piece);
        }

        // game started
        switch (piece)
        {
            case PieceType.Circle:
                KS_NotificationCenter.DispatchEvent(NKeys.OnPlayerTurn);    // Player starts first
                PlayerCirclePieces.SetActive(true);
                PlayerCrossPieces.SetActive(false);
                AiCirclePieces.SetActive(false);
                AiCrossPieces.SetActive(true);
                break;
            case PieceType.Cross:
                KS_Timer.DelayedAction(3.5f, () => {
                    TicTacToeAI.Instance.PlayNext(); // AI starts first
                });
                PlayerCirclePieces.SetActive(false);
                PlayerCrossPieces.SetActive(true);
                AiCirclePieces.SetActive(true);
                AiCrossPieces.SetActive(false);
                break;
        }
    }

    public void PlayerPutPiece(BoardSinglePiece piece)
    {
        piece.OccupyWithPiece(PlayerType);
        OnPlayerPiecePlaced?.Invoke();
        if (!CheckBoardWinStatus())
        {
            TicTacToeAI.Instance.PlayNext();    // if game not finished, continue AI turn
        }
    }

    public void AiPutPiece(BoardSinglePiece piece)
    {
        piece.OccupyWithPiece(AiType);
        OnAiPiecePlaced?.Invoke();
        if (!CheckBoardWinStatus())
        {
            KS_NotificationCenter.DispatchEvent(NKeys.OnPlayerTurn); // if game not finished, continue player turn
        }
    }

    public bool CheckBoardWinStatus()
    {
        // Check each winning combination
        foreach (var combo in winCombos)
        {
            BoardSinglePiece a = Pieces[combo[0]];
            BoardSinglePiece b = Pieces[combo[1]];
            BoardSinglePiece c = Pieces[combo[2]];

            if (a.isOccupied && b.isOccupied && c.isOccupied)
            {
                PieceType typeA = a.occupiedType;
                // If all three are the same
                if (typeA == b.occupiedType && typeA == c.occupiedType)
                {
                    if (typeA == PlayerType)
                    {
                        PlayerWin();
                    }
                    else if (typeA == AiType)
                    {
                        AiWin();
                    }
                    // Early return on win
                    return true;
                }
            }
        }

        // Check for tie
        bool anyEmpty = false;
        foreach (var p in Pieces)
        {
            if (!p.isOccupied)
            {
                anyEmpty = true;
                break;
            }
        }
        if (!anyEmpty)
        {
            Tie();
            return true;
        }

        return false;
    }

    public List<BoardSinglePiece> GetEmptySpaces()
    {
        var emptySpaces = new List<BoardSinglePiece>();
        foreach (BoardSinglePiece p in Pieces)
        {
            if (!p.isOccupied) emptySpaces.Add(p);
        }
        return emptySpaces;
    }


    public void PlayerWin()
    {
        KS_Logger.Log("Player wins!", this);
        OnOneGameEnd(GameWinState.PlayerWin);
    }

    public void AiWin()
    {
        KS_Logger.Log("AI wins!", this);
        OnOneGameEnd(GameWinState.AiWin);
    }

    public void Tie()
    {
        KS_Logger.Log("It's a tie!", this);
        OnOneGameEnd(GameWinState.Draw);
    }

    public void OnOneGameEnd(GameWinState state)
    {
        KS_Timer.DelayedAction(2f, () =>
        {
            OnGameEnd?.Invoke();
            KS_NotificationCenter.DispatchEvent(NKeys.OnGameEnd, state);

            KS_Timer.DelayedAction(1f, () =>
            {
                foreach (BoardSinglePiece p in Pieces)
                {
                    p.ResetSelf();
                }

                SwapPlayerType();
            });    
        });
    }
}
