namespace ServicesWeb
{
    using Glimpse.Core.Extensibility;

    public class GlimpseSecurityPolicy : IRuntimePolicy
    {
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            // You can perform a check like the one below to control Glimpse's permissions within your application.
            // More information about RuntimePolicies can be found at http://getglimpse.com/Help/Custom-Runtime-Policy
            // var httpContext = policyContext.GetHttpContext();
            // if (!httpContext.User.IsInRole("Administrator"))
            // {
            //     return RuntimePolicy.Off;
            // }

            return RuntimePolicy.On;
        }

        /// <remarks>
        /// The RuntimeEvent.ExecuteResource is only needed in case you create a security policy
        /// Have a look at http://blog.getglimpse.com/2013/12/09/protect-glimpse-axd-with-your-custom-runtime-policy/ for more details
        /// </remarks>
        public RuntimeEvent ExecuteOn => RuntimeEvent.EndRequest | RuntimeEvent.ExecuteResource;
    }
}
