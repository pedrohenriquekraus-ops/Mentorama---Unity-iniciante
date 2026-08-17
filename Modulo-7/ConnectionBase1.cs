public class Connection
{

    public Dot Dota { get; set; }
    public Dot DotB { get; set; }
    public float Length { get; }




    public Connection(Dot dota, Dot dotB, float length)
    {
        Dota = dota;
        DotB = dotB;
        Length = length;
    }


    public Connection(Dot dota, Dot dotB)
    {
        Dota = dota;
        DotB = dotB;
        Length = (dota.CurrentPosition - dotB.CurrentPosition).magnitude;
    }



    public Dot other(Dot dot) => dot == Dota ? DotB : Dota;

}