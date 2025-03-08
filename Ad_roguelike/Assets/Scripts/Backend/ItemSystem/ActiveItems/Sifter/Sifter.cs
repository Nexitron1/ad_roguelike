public class Sifter : ActiveItem
{
    public override void Activate()
    {
        if (Random(50))
        {
            character.money += 50;
        }
    }
}
