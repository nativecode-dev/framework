namespace NativeCode.Core.Platform
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;

    public class PrincipalFactory : IPrincipalFactory
    {
        private readonly IEnumerable<IPrincipalInflater> inflaters;

        public PrincipalFactory(IEnumerable<IPrincipalInflater> inflaters)
        {
            this.inflaters = inflaters;
        }

        public IPrincipalInflater GetInflater(PrincipalSource source)
        {
            return this.inflaters.FirstOrDefault(x => x.CanInflate(source));
        }

        public IPrincipal GetPrincipal(PrincipalSource source, string login)
        {
            var inflater = this.GetInflater(source);

            return inflater.CreatePrincipal(login);
        }
    }
}