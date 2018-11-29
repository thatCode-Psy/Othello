using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : AIScript {



    public override KeyValuePair<int, int> makeMove(List<KeyValuePair<int, int>> availableMoves, BoardSpace[][] currentBoard) {

        return availableMoves[Random.Range(0, availableMoves.Count)];
    }

}
