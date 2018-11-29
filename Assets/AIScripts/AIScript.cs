using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class AIScript {

    protected BoardSpace color;

    public abstract KeyValuePair<int, int> makeMove(List<KeyValuePair<int, int>> availableMoves, BoardSpace[][] currentBoard);

    public void setColor(BoardSpace color) {
        this.color = color;
    }

}
