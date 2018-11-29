using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegaMaxAI : AIScript {

    public override KeyValuePair<int, int> makeMove(List<KeyValuePair<int, int>> availableMoves, BoardSpace[][] currentBoard) {
        //Change to not be random
        return availableMoves[Random.Range(0, availableMoves.Count)];
    }

}
