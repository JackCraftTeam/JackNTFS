namespace JackNTFS
{
    /* 日志记录器 */
    internal class Logger : AbstractLogger
    {
        public Logger() {}

        /**
         * 记录日志
         * @para level   描述被记录日志等级
         * @para content 描述被记录日志内容
         */
        public override void log(Level level, string content)
        {
            /*
                TODO: Jack, 我们需要允许该方法及其所有重载方法得以传入并重定向日志输出至指定文件。
            */
            /* 我们没有在这个方法中传入参数“缘由”， 因此使用 loggingFormatLC。 */
            Console.Write(loggingFormatLC, levelToContent(level), content);
        }

        /**
         * 记录日志
         * @para level     描述被记录日志等级
         * @para initiator 描述被记录日志缘由
         * @para content   描述被记录日志内容
         */
        public override void log(Level level, string initiator, string content)
        {
            /* 我们在这个方法中传入了参数“缘由”， 因此使用 loggingFormatLIC。 */
            Console.Write(loggingFormatLIC, levelToContent(level), initiator,
                          content);
        }
    }
}