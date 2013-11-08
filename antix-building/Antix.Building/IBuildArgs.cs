//  by Anthony J. Johnston, antix.co.uk

namespace Antix.Building
{
    public interface IBuildArgs
    {
        IBuildArgs Create(int index, object properties);

        int Index { get; set; }
        object Properties { get; }
    }
}