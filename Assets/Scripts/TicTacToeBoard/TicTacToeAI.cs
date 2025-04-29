using System.Collections.Generic;
using UnityEngine;
using KayphoonStudio;

public class TicTacToeAI : KS_Singleton<TicTacToeAI>
{
    [Header("AI Settings")]
    [Range(0f, 1f)]
    public float mistakeRate = 0f; // 0 = perfect play, 1 = always random move

    // Winning combinations indices
    private static readonly int[][] winCombos = new int[][] {
        new[] {0,1,2}, new[] {3,4,5}, new[] {6,7,8},
        new[] {0,3,6}, new[] {1,4,7}, new[] {2,5,8},
        new[] {0,4,8}, new[] {2,4,6}
    };

    /// <summary>
    /// Main entry for AI move with potential mistakes
    /// </summary>
    public void PlayNext()
    {
        List<BoardSinglePiece> empty = TicTacToeBoard.Instance.GetEmptySpaces();
        if (empty.Count == 0)
            return;

        BoardSinglePiece chosen;
        // Decide if AI should make a mistake
        if (Random.value < mistakeRate)
        {
            // Pick a random mistake
            chosen = empty[Random.Range(0, empty.Count)];
        }
        else
        {
            // Optimal move via Minimax
            chosen = GetBestMove();
            // Fallback to random if something goes wrong
            if (chosen == null)
                chosen = empty[Random.Range(0, empty.Count)];
        }

        KS_Timer.DelayedAction(1f, () => {
            TicTacToeBoard.Instance.AiPutPiece(chosen);
        });
    }

    /// <summary>
    /// Determines the best move using minimax algorithm
    /// </summary>
    private BoardSinglePiece GetBestMove()
    {
        var pieces = TicTacToeBoard.Instance.Pieces;
        var board = new PieceType[9];
        for (int i = 0; i < 9; i++)
            board[i] = pieces[i].isOccupied ? pieces[i].occupiedType : PieceType.None;

        int bestScore = int.MinValue;
        int bestIndex = -1;
        List<BoardSinglePiece> empty = TicTacToeBoard.Instance.GetEmptySpaces();

        foreach (var cell in empty)
        {
            int idx = pieces.IndexOf(cell);
            board[idx] = TicTacToeBoard.Instance.AiType;
            int score = Minimax(board, false);
            board[idx] = PieceType.None;
            if (score > bestScore)
            {
                bestScore = score;
                bestIndex = idx;
            }
        }

        return bestIndex >= 0 ? pieces[bestIndex] : null;
    }

    /// <summary>
    /// Minimax recursion: maximizing for AI, minimizing for Player
    /// </summary>
    private int Minimax(PieceType[] board, bool isMaximizing)
    {
        int eval = EvaluateBoard(board);
        if (eval != 0 || IsBoardFull(board))
            return eval;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == PieceType.None)
                {
                    board[i] = TicTacToeBoard.Instance.AiType;
                    bestScore = Mathf.Max(bestScore, Minimax(board, false));
                    board[i] = PieceType.None;
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == PieceType.None)
                {
                    board[i] = TicTacToeBoard.Instance.PlayerType;
                    bestScore = Mathf.Min(bestScore, Minimax(board, true));
                    board[i] = PieceType.None;
                }
            }
            return bestScore;
        }
    }

    /// <summary>
    /// Evaluates board: +10 AI win, -10 Player win, 0 otherwise
    /// </summary>
    private int EvaluateBoard(PieceType[] board)
    {
        foreach (var combo in winCombos)
        {
            PieceType a = board[combo[0]];
            if (a == PieceType.None) continue;
            if (a == board[combo[1]] && a == board[combo[2]])
            {
                if (a == TicTacToeBoard.Instance.AiType)
                    return +10;
                else if (a == TicTacToeBoard.Instance.PlayerType)
                    return -10;
            }
        }
        return 0;
    }

    /// <summary>
    /// Checks if any empty cells remain
    /// </summary>
    private bool IsBoardFull(PieceType[] board)
    {
        foreach (var cell in board)
            if (cell == PieceType.None)
                return false;
        return true;
    }
}
