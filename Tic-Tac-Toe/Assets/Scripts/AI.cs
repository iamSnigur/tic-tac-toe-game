using System;

public class AI
{
    private int[] _board;

    private int MiniMax(int position, int depth, int alpha, int beta, bool isMaximizing)
    {
        if(depth == 0)
        {
            return 0;
        }

        if(isMaximizing)
        {
            var maxEval = int.MinValue;
            
            foreach(var c in _board)
            {
                var eval = MiniMax(c, depth - 1, alpha, beta, false);
                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);

                if(beta <= alpha)
                {
                    break;
                }
            }

            return maxEval;
        }
        else
        {
            var minEval = int.MaxValue;

            foreach (var c in _board)
            {
                var eval = MiniMax(c, depth - 1, alpha, beta, true);
                minEval = Math.Min(minEval, eval);
                beta = Math.Min(beta, eval);

                if (beta <= alpha)
                {
                    break;
                }
            }
            return minEval;
        }
    }
}
