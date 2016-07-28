namespace NativeCode.Core.Patterns.DataMovement
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IConveyorHandler<TPackage>
        where TPackage : IConveyorPackage
    {
        Task<TPackage> HandleAsync(TPackage package, CancellationToken cancellation);
    }
}