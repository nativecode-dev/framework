namespace NativeCode.Core.Types
{
    using System.Collections.Generic;
    using System.Dynamic;

    public class ConnectionString : DynamicObject
    {
        public ConnectionString()
        {
        }

        public ConnectionString(string connectionString)
        {
        }

        private readonly Dictionary<string, string> properties = new Dictionary<string, string>();

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return this.properties.Keys;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type == typeof(string))
            {
                result = this.ToString();
                return true;
            }

            return base.TryConvert(binder, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (this.properties.ContainsKey(binder.Name))
            {
                result = this.properties[binder.Name];
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (value != null)
            {
                if (this.properties.ContainsKey(binder.Name))
                {
                    this.properties[binder.Name] = value.ToString();
                }
                else
                {
                    this.properties.Add(binder.Name, value.ToString());
                }

                return true;
            }

            return base.TrySetMember(binder, null);
        }
    }
}