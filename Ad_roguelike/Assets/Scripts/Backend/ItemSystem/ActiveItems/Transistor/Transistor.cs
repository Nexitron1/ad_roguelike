public class Transistor : ActiveItem
{
    public override void Activate()
    {
        character.SkipTime(5f, TimeSkip.DurationType.absolute, 0, TimeSkip.PlaceType.absolute, false);
    }
}