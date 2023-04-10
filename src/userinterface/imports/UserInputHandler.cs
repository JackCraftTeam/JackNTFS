namespace JackNTFS
{
    internal class UserInputHandler : AbstractUserInputHandler
    {
        protected static String userInput = "";

        protected readonly String userInputRestriction;
        public UserInputHandler(Stream mImportStream, Stream mExportStream)
                : base(mImportStream, mExportStream)
        {
            /* TODO(William): Not decided yet. */
        }

        /* 既然我们传入参数 “用户输入限制格式”， 则 “操作” 在函数 “处理”
           中只可能是 “按格式回答”。 则调用函数 “处理(“输入流来源”)”。 */
        public UserInputHandler(Stream mImportStream, Stream mExportStream,
                                String userInputRestriction)
                : base(mImportStream, mExportStream)
        {
            /* William, YOU LEFT HERE */
        }

        protected override void handle(Operation oper, Stream sourceStream)
        {
            switch (oper)
            {
                case Operation.RESTRICTED:
            }
        }

        protected override void handle(Operation oper)
        {
            throw new NotImplementedException();
        }

        protected void handle(Stream sourceStream)
        {

        }
    }
}
