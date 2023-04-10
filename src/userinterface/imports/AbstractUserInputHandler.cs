namespace JackNTFS
{
    internal abstract class AbstractUserInputHandler
    {
        /* 回答限制 */
        protected readonly Operation oper;
        /* Expectation */
        protected readonly string expect;
        /* 输入流来源 */
        protected readonly Stream mImportStream;
        /* 输出流来源 */
        protected readonly Stream mExportStream;

        public enum Operation
        {
            /* 按单键回答 */
            KEY = 0,
            /* 按任意回答 */
            BOOLEAN_STYLE
        }

        public AbstractUserInputHandler(Operation oper, String expect, Stream mImportStream, Stream mExportStream)
        {
            this.oper = oper;
            this.expect = expect;
            this.mImportStream = mImportStream;
            this.mExportStream = mExportStream;
        }

        protected abstract bool handle(Stream sourceStream);

        protected abstract bool handle(Stream sourceStream, String expect);

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