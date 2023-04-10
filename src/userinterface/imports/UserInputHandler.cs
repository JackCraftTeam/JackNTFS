using System.IO;

namespace JackNTFS
{
    internal class UserInputHandler : AbstractUserInputHandler
    {
        protected static String userInput = "";

        protected readonly bool result;

        public UserInputHandler(Operation oper, String expect, Stream mImportStream, Stream mExportStream)
                : base(oper, expect, mImportStream, mExportStream)
        {
            /* TODO(William): Not decided yet. */
            userInput = new StreamReader(mImportStream).ReadLine();
            switch (oper)
            {
                case Operation.KEY:
                    this.result = handle(mImportStream, expect);
                    break;
                case Operation.BOOLEAN_STYLE:
                    this.result = handle(mExportStream);
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        /* Key */
        protected override bool handle(Stream sourceStream, String expect)
        {
            return (userInput.Equals(expect));
        }

        /* Boolean Style */
        protected override bool handle(Stream sourceStream)
        {
            return userInput.Equals(("Y".Equals(StringComparer.OrdinalIgnoreCase))
                   || ("YES".Equals(StringComparer.OrdinalIgnoreCase)));
        }
    }
}
