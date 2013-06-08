namespace Antix.Building.Tests.Pocos
{
    public class ThingyParent : IThingyHasName
    {
        public string Name { get; set; }
        public Thingy[] Thingies { get; set; }
    }
}