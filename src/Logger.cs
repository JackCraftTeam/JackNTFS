namespace JackNTFS
{
  /* 日志记录器
     将由 “日志记录管理器” 执行最终解释权 */
  internal abstract class Logger
  {
    /* 日志格式 : string
       用于格式化日志输出文本
       L: 等级 (level : Level)
       C: 内容 (content : string) */
    protected static readonly string loggingFormatLC = "[%s]: %s\n";
    /* 日志格式 : string
       用于格式化日志输出文本
       L: 等级 (level : Level)
       I: 缘由 (initiator : string)
       C: 内容 (content : string) */
    protected static readonly string loggingFormatLIC = "[%s](%s): %s\n";

    public enum Level
    {
        /* 禁用日志
           当使用本级别, Logger 将不再推送任何日志 */
        NONE = 0,
        /* 通知级别
           当使用本级别, Logger 将仅推送低于或等于通知级别的日志 */
        INFO,
        /* 警告级别
           当使用本级别, Logger 将仅推送低于或等于警告级别的日志 */
        WARN,
        /* 错误级别
           当使用本级别, Logger 将仅推送低于或等于错误级别的日志 */
        ERROR,
        /* 检修级别
           当使用本级别, Logger 将仅推送低于或等于检修级别的日志 */
        DEBUG,
        /* 我全都要
           当使用本级别, Logger 将推送所有级别的日志 */
        ALL,
        /* 跟随级别
           当使用本级别, Logger 将推送所有级别的日志以及 JackNTFS 的内部额外日志
           例如: “进入 C:\Users\Jack\Program Files (x86) 目录” */
        TRACE
    }

    protected Logger() {}

    /**
     * 记录日志
     * @para level   描述被记录日志等级
     * @para content 描述被记录日志内容
     */
    public abstract void log(Level level, string content);

    /**
     * 记录日志
     * @para level     描述被记录日志等级
     * @para initiator 描述被记录日志缘由
     * @para content   描述被记录日志内容
     */
    public abstract void log(Level level, string initiator, string content);


    protected static string levelToContent(Level level)
    {
        switch (level)
          {
            case Level.NONE:
                return " ";
            case Level.INFO:
                return "I";
            case Level.WARN:
                return "W";
            case Level.ERROR:
                return "E";
            case Level.DEBUG:
                return "D";
            case Level.ALL:
                return "A";
            case Level.TRACE:
                return "T";
            default:
                throw new InvalidDataException("找不到相关枚举元素： " + level +
                                               " 是无效数据");
          }
    }

  }
}
