namespace NativeCode.Core.Patterns.DataMovement
{
    using System.Collections.Generic;

    public class ConveyorConfiguration<TPackage>
        where TPackage : IConveyorPackage
    {
        public List<IConveyorHandler<TPackage>> Handlers { get; } = new List<IConveyorHandler<TPackage>>();
    }
}