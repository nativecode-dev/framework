namespace NativeCode.Tests.Core.Authorization
{
    using NativeCode.Core.Authorization;

    using Xunit;

    public class WhenParsingAssertions
    {
        private const string ComplexExpression =
            "(#Can_Read_File | #Can_Write_File & (#Can_Scan_File & (!#Can_Save_File | #Can_Print_File) | (#Can_Email_File & #Can_Move_File)))";

        [Fact]
        public void ShouldParseStuff()
        {
            new SecurityStringParser(new SecurityEvaluator()).Evaluate(ComplexExpression, null);
        }
    }
}