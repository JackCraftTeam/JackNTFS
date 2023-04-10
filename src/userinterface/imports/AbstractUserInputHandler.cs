namespace JackNTFS
{
    internal abstract class AbstractUserInputHandler
    {
        /* 输入流来源 */
        protected readonly Stream mImportStream;
        /* 输出流来源 */
        protected readonly Stream mExportStream;

        protected enum Operation
        {
            /* 按格式回答 */
            RESTRICTED = 0,
            /* 按二元回答 */
            BOOLEAN_STYLE,
            /* 按单键回答 */
            KEY,
            /* 按任意回答 */
            EVERYTHING
        }

        public AbstractUserInputHandler(Stream mImportStream, Stream mExportStream)
        {
            this.mImportStream = mImportStream;
            this.mExportStream = mExportStream;
        }

        protected abstract void handle(Operation oper, Stream sourceStream);

        /* 不指定 “输入流来源”， 则使用默认输入流 */
        protected abstract void handle(Operation oper);

        /* 返回： 该实例的 “输入流来源” */
        public Stream getImportStream()
        {
            return this.mImportStream;
        }

        /* 返回： 该实例的 “输出流来源” */
        public Stream getExportStream()
        {
            return this.mExportStream;
        }

    }
}