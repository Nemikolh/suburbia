// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //

public class Pair<T, U>
{
    public Pair ()
    {
    }
    
    public Pair (T p_key, U p_value)
    {
        this.Key = p_key;
        this.Value = p_value;
    }
    
    public T Key { get; set; }

    public U Value { get; set; }
};