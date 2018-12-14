Othello

Jared Okun and Sam Gould

Othello implemented with Negamax

Our evaluation function tries to maximize score by finding the greatest amount of pieces of the same color on the board.
The function can search any N moves ahead. The function also introduces some randomness by choosing between identically scored best
moves and having a very low chance to make a suboptimal move. 
The evaluation function also weights corner pieces as much higher than any other space and the score takes a hit if the other color moves there as well.

There is also a NegamaxEval function that uses a less optimized version of negamax and a completely random AI.

These can be toggled as well as if it is human or AI on the board.

