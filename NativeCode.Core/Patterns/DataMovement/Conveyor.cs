namespace NativeCode.Core.Patterns.DataMovement
{
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class Conveyor<TPackage>
        where TPackage : IConveyorPackage
    {
        protected Conveyor(ConveyorConfiguration<TPackage> configuration)
        {
            this.Configuration = configuration;
        }

        public Task<TPackage> Run(TPackage package, CancellationToken cancellationToken)
        {
            return Task.Run(async () => await this.MovePackageAsync(package, cancellationToken), cancellationToken);
        }

        protected ConveyorConfiguration<TPackage> Configuration { get; }

        protected async Task<TPackage> MovePackageAsync(TPackage package, CancellationToken cancellationToken)
        {
            foreach (var handler in this.Configuration.Handlers)
            {
                package = await handler.HandleAsync(package, cancellationToken);
            }

            return package;
        }
    }
}