namespace NativeCode.Tests
{
    public abstract class WhenTestingApplication : WhenTesting<ApplicationFixture>
    {
        protected WhenTestingApplication(ApplicationFixture fixture)
        {
            this.ApplicationFixture = fixture;
        }

        protected ApplicationFixture ApplicationFixture { get; }
    }
}