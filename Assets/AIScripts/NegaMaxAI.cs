using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegaMaxAI : AIScript {

    public const int DEPTH = 3;

    public override KeyValuePair<int, int> makeMove(List<KeyValuePair<int, int>> availableMoves, BoardSpace[][] currentBoard) {

        BoardSpace[][][] possibleMoves = GetChildrenNodes(currentBoard, this.color);
        if (possibleMoves.Length > 0) {
            int maxChild = 0;
            int maxChildValue = NegaMax(possibleMoves[0], DEPTH, int.MinValue, int.MaxValue, this.color);
            for (int i = 1; i < possibleMoves.Length; ++i) {
                int value = NegaMax(possibleMoves[1], DEPTH, int.MinValue, int.MaxValue, this.color);
                if(value > maxChildValue) {
                    maxChildValue = value;
                    maxChild = i;
                }
            }
            Debug.Log(maxChildValue);
            return availableMoves[maxChild];
        }
       
        return availableMoves[Random.Range(0, availableMoves.Count)];
    }

    int NegaMax(BoardSpace[][] node, int depth, int alpha, int beta, BoardSpace color) {
        bool completed = IsGameCompleted(node);
        if(depth == 0 || completed) {
            int evaluation = EvaluationFunction(node, color, completed);
            Debug.Log("evaluation of following board by player " + color + " with score of " + evaluation + " board: ");
            printBoard(node);
            return color == this.color ? evaluation : -evaluation;
        }
        BoardSpace[][][] children = GetChildrenNodes(node, color == BoardSpace.BLACK ? BoardSpace.WHITE : BoardSpace.BLACK);
        int value = int.MinValue;
        foreach(BoardSpace[][] child in children) {
            value = Mathf.Max(value, -NegaMax(child, depth - 1, -beta, -alpha, color == BoardSpace.BLACK ? BoardSpace.WHITE : BoardSpace.BLACK));
            alpha = Mathf.Max(alpha, value);
            if(alpha >= beta) {
                break;
            }
        }
        return value;        
    }


    BoardSpace[][][] GetChildrenNodes(BoardSpace[][] node, BoardSpace color) {
        List<KeyValuePair<int, int>> validMoves = BoardScript.GetValidMoves(node, color == BoardSpace.BLACK ? 0u : 1u);
        BoardSpace[][][] childrenNodes = new BoardSpace[validMoves.Count][][];

        for(int i = 0; i < validMoves.Count; ++i) {
            childrenNodes[i] = CopyBoard(node);
            childrenNodes[i][validMoves[i].Key][validMoves[i].Value] = color;
            List<KeyValuePair<int, int>> changedSpaces = BoardScript.GetPointsChangedFromMove(childrenNodes[i], color == BoardSpace.BLACK ? 0u : 1u, validMoves[i].Value, validMoves[i].Key);
            
            foreach(KeyValuePair<int, int> changed in changedSpaces) {
                childrenNodes[i][changed.Key][changed.Value] = color;
            }
        }


        return childrenNodes;
    }

    BoardSpace[][] CopyBoard(BoardSpace[][] board) {
        BoardSpace[][] copy = new BoardSpace[board.Length][];
        for(int i = 0; i < board.Length; ++i) {
            copy[i] = new BoardSpace[board[i].Length];
            for(int j = 0; j < board[i].Length; ++j) {
                switch (board[i][j]) {
                    case (BoardSpace.BLACK):
                        copy[i][j] = BoardSpace.BLACK;
                        break;
                    case (BoardSpace.WHITE):
                        copy[i][j] = BoardSpace.WHITE;
                        break;
                    default:
                        copy[i][j] = BoardSpace.EMPTY;
                        break;
                }
            }
        }
        return copy;
    }


    bool IsGameCompleted(BoardSpace[][] node) {
        return BoardScript.GetValidMoves(node, 0).Count == 0 && BoardScript.GetValidMoves(node, 1).Count == 0;
    }

    int EvaluationFunction(BoardSpace[][] currentBoard, BoardSpace color, bool isGameCompleted)
    {
        int totalDifference = 0;
        foreach (BoardSpace[] row in currentBoard)
        {
            foreach (BoardSpace space in row)
            {
                if (space == color)
                {
                    totalDifference++;
                }
                else if (space != BoardSpace.EMPTY)
                {
                    totalDifference--;
                }
            }
        }// weighting corner pieces greater
        if (currentBoard[0][0] == color)
        {
            totalDifference += 7;
        }
        else if (currentBoard[0][0] != BoardSpace.EMPTY)
        {
            totalDifference -= 7;
        }
        if (currentBoard[7][0] == color)
        {
            totalDifference += 7;
        }
        else if (currentBoard[7][0] != BoardSpace.EMPTY)
        {
            totalDifference -= 7;
        }
        if (currentBoard[7][7] == color)
        {
            totalDifference += 7;
        }
        else if (currentBoard[7][7] != BoardSpace.EMPTY)
        {
            totalDifference -= 7;
        }
        if (currentBoard[0][7] == color)
        {
            totalDifference += 7;
        }
        else if (currentBoard[0][7] != BoardSpace.EMPTY)
        {
            totalDifference -= 7;
        }
        
        return totalDifference;
    }

    void printBoard(BoardSpace[][] board) {
        string output = "";
        foreach (BoardSpace[] row in board) {
            string rowString = "";
            foreach(BoardSpace tile in row) {
                switch (tile) {
                    case BoardSpace.BLACK:
                        rowString += "B";
                        break;
                    case BoardSpace.WHITE:
                        rowString += "W";
                        break;
                    default:
                        rowString += "E";
                        break;
                }
            }
            output += rowString + "\n";
            
        }
        Debug.Log(output);
    }
    
}
