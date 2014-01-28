// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
public sealed class MathCore
{
    private MathCore ()
    {
    }

    /// <summary>
    /// Compute the value of the spline cubic defined on the range [0,1]
    /// where :
    ///     S(0) = 0 ; S(1) = 1 ; S'(0) = tan_origin ; S'(1) = tan_final
    /// </summary>
    /// <param name="p_x">The value in the range [0, 1]</param>
    /// <param name="p_tan_origin">The tangeant at the origin (x = 0).</param>
    /// <param name="p_tan_final">The tangeant at the end (x = 1).</param>
    public static float Spline (float p_x, float p_tan_origin, float p_tan_final)
    {
        return p_x * (p_tan_origin + (3 - 2 * p_tan_origin - p_tan_final) * p_x + (p_tan_final + p_tan_origin - 2) * p_x * p_x);
    }
}

