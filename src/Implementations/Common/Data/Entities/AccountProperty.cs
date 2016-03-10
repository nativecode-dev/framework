namespace Common.Data.Entities
{
    using Common.DataServices;

    public class AccountProperty : Property
    {
        public Account Account { get; set; }
    }
}