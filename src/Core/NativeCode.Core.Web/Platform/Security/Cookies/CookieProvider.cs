namespace NativeCode.Core.Web.Platform.Security.Cookies
{
    using JetBrains.Annotations;

    using NativeCode.Core.Serialization;

    public abstract class CookieProvider<TRequest, TResponse, TCookie, TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CookieProvider{TRequest, TResponse, TCookie, TData}" /> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        protected CookieProvider(IStringSerializer serializer)
        {
            this.Serializer = serializer;
        }

        /// <summary>
        /// Gets the serializer.
        /// </summary>
        protected IStringSerializer Serializer { get; }

        /// <summary>
        /// Creates a cookie.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="data">The cookie.</param>
        /// <param name="persist">if set to <c>true</c> persist the cookie.</param>
        /// <param name="timeout">The timeout in seconds.</param>
        /// <param name="version">The cookie version.</param>
        /// <returns>Returns a cookie.</returns>
        public abstract TCookie CreateCookie([NotNull] string name, [NotNull] TData data, bool persist = true, int timeout = 20, int version = 1);

        /// <summary>
        /// Gets the cookie.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="name">The name.</param>
        /// <returns>Returns a cookie.</returns>
        public abstract TCookie GetCookie([NotNull] TRequest request, [NotNull] string name);

        /// <summary>
        /// Gets the cookie data.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        /// <returns>Returns a cookie data.</returns>
        public abstract TData GetCookieData([NotNull] TCookie cookie);

        /// <summary>
        /// Sets the cookie.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="cookie">The cookie.</param>
        public abstract void SetCookie([NotNull] TResponse response, [NotNull] TCookie cookie);
    }
}