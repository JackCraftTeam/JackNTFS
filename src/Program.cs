using log4net;
using log4net.Config;
using ShellProgressBar;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace JackNTFS
{
    internal class Program
    {
        private static List<FileSystemInfo> allFilesList = new List<FileSystemInfo>();  //所有文件与文件夹
        private static List<FileSystemInfo> allDirList = new List<FileSystemInfo>();    //所有文件夹
        private static List<FileSystemInfo> allFileList = new List<FileSystemInfo>();   //所有文件

        private static readonly ILog log = LogManager.GetLogger(typeof(Program)); //声明Logger对象

        //public static void Main(string[] args)
        public static async Task Main(string[] args)
        {
            XmlConfigurator.Configure();
            Console.WriteLine("欢迎使用 JackNTFS 程序！\n[—————————————————————————————]");
            Console.WriteLine("按任意键以继续");
            Console.ReadLine();
            List<DriveInfo> driveInfoList = new List<DriveInfo>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                driveInfoList.Add(drive);
                Console.WriteLine("盘符 {0}", drive.Name);
                Console.WriteLine("  ┣ 卷标: {0}", drive.VolumeLabel);
                Console.WriteLine("  ┣ 文件系统: {0}", drive.DriveFormat);
                Console.WriteLine("  ┣ 当前用户的可用空间:{0, 15} 字节",
                    drive.AvailableFreeSpace);

                if (drive.IsReady == true)
                {
                    Console.WriteLine("  ┣ 总可用空间:        {0, 15} 字节",
                        drive.TotalFreeSpace);

                    Console.WriteLine("  ┗ 驱动器的总大小:    {0, 15} 字节",
                        drive.TotalSize);
                }
            }   //遍历所有盘
            Console.Write($"[共{driveInfoList.Count}个盘符] [");
            log.Info($"[共{driveInfoList.Count}个盘符]");
            int drive_int = 0;
            foreach (DriveInfo drive in driveInfoList)
            {
                drive_int++;
                Console.Write($" {drive_int}-{drive.Name} ");
            }
            Console.Write("]\n");
            Console.Write($"要读取的盘符 [请输入上方数字<1-{driveInfoList.Count}>]: ");
            log.Info($"要读取的盘符 [请输入上方数字<1-{driveInfoList.Count}>]: ");
            DriveInfo SelectedDrive = null;
            while (true)
            {
                //Console.WriteLine("test");
                try
                {
                    int OperateDrive = Convert.ToInt16(Console.ReadLine());
                    if (OperateDrive >= 1 && OperateDrive <= driveInfoList.Count)
                    {
                        SelectedDrive = driveInfoList[OperateDrive-1];
                        //log.Info()
                        break;
                    }
                    else
                    {
                        Console.WriteLine("超出范围(Out of range)");
                        /*log.Warn("超出范围(Out of range): " + OperateDrive + $" [1, {driveInfoList.Count}]");*/
                        log.Warn(outOfRangeExpression(OperateDrive, 1, driveInfoList.Count, true, true));
                        /*logMgr.log(AbstractLogger.Level.ERROR, "超出范围(Out of range)");*/
                        Console.Write($"要读取的盘符[请输入上方数字<1-{driveInfoList.Count}>]: ");
                        log.Info($"要读取的盘符[请输入上方数字<1-{driveInfoList.Count}>]: ");
                    }
                }
                catch (IndexOutOfRangeException outOfRangeExcep)
                {
                    Console.WriteLine("超出范围(Out of range)");
                    // Mark1
                    Console.Write($"要读取的盘符[请输入上方数字<1-{driveInfoList.Count}>]: ");
                }
            }
            Console.WriteLine($" ┗ 当前选择的盘符: {SelectedDrive.RootDirectory}");
            Console.Write($"要进行的操作 [ 1-全盘读取 2-单文件读取]: ");
            int ActionTaken = 0;
            while (true)
            {
                //Console.WriteLine("test");
                try
                {
                    int ActionDrive = Convert.ToInt16(Console.ReadLine());
                    if (ActionDrive >= 1 && ActionDrive <= 2)
                    {
                        ActionTaken = ActionDrive;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("超出范围(Out of range)");
                        log.Warn("超出范围(Out of range)");
                        log.Warn(outOfRangeExpression(ActionDrive, 1, 2, true, true));
                        Console.Write($"要进行的操作 [ 1-全盘读取 2-单文件读取]: ");
                    }
                }
                catch (IndexOutOfRangeException outOfRangeExcep)
                {
                    Console.WriteLine("超出范围(Out of range)");
                    /*logMgr.log(AbstractLogger.Level.ERROR, "超出范围(Out of range)");*/
                    // Mark 2
                    Console.Write($"要进行的操作 [ 1-全盘读取 2-单文件读取]: ");
                }
            }
            string ActionName = "";
            if (ActionTaken == 1)
                ActionName = "全盘读取";
            else if (ActionTaken == 2)
                ActionName = "单文件读取";
            Console.WriteLine($" ┗ 进行的操作: {ActionName}");
            if (ActionTaken == 1)
            {
                Console.Write("请输入偏移量（单位：字节）[若无偏移则填写0<一般无偏移的文件均可以正常读取，无需使用本工具>]: ");
                long offset = 0;
                bool While_Bool = true;
                while (While_Bool)
                {
                    try
                    {
                        offset = (long)Convert.ToDouble(Console.ReadLine()); //偏移量（字节）
                        While_Bool = false;
                    }
                    catch (Exception) // FIXME: OutOfRangeException
                    {
                        Console.WriteLine("超出范围(Out of range)");
                        // Mark 3
                        Console.Write("请输入偏移量（单位：字节）[若无偏移则填写0<一般无偏移的文件均可以正常读取，无需使用本工具>]: ");
                    }
                }

                
                string savePath = "";
                while (true)
                {
                    Console.Write("要保存到的文件夹或盘符: ");
                    savePath = Console.ReadLine();

                    if (savePath == "" || savePath == null || Object.Equals(savePath, null))
                    {
                        Console.WriteLine(" ┗ 请输入路径！");
                    }
                    else { break; }
                }
                DirectoryInfo directory = new DirectoryInfo(SelectedDrive.Name);
                PrintFileSystemInfo(directory, 0);

                var Dir_options = new ProgressBarOptions
                {
                    ProgressCharacter = '─',
                    ProgressBarOnBottom = true
                };
                int Dir_ProgressBarValue = 0;
                int Dir_TotalProgressBarValue = allDirList.Count - 1;  //减去根目录
                string Dir_ProgressBarMessage = "正在处理中...";
                using (var Dir_progressBar = new ProgressBar(Dir_TotalProgressBarValue, Dir_ProgressBarMessage, Dir_options))
                {
                    await Task.Delay(500);
                    foreach (FileSystemInfo file in allDirList)
                    {
                        if (file.FullName != SelectedDrive.Name)    //不输出根目录
                        {
                            string fixname = file.FullName.Replace(SelectedDrive.Name, "");  //移除根目录
                            string targetPath = $"{savePath}/{fixname}";
                            Dir_ProgressBarValue++;
                            //Dir_progressBar.Tick($"[{Dir_ProgressBarValue} / {Dir_TotalProgressBarValue}] [文件夹] [创建] - {file.FullName} -> {targetPath}");
                            Dir_progressBar.Tick($"[{Dir_ProgressBarValue} / {Dir_TotalProgressBarValue}]");
                            log.Info($"[{Dir_ProgressBarValue} / {Dir_TotalProgressBarValue}] [文件夹] [创建] - {file.FullName} -> {targetPath}");
                            try
                            {
                                Directory.CreateDirectory($"{targetPath}");
   
                                DirectoryInfo targetInfo = new DirectoryInfo(targetPath);
                                FileAttributes attributes = file.Attributes;  //获取源文件属性
                                targetInfo.Attributes = attributes; //将新文件属性替换为源文件属性
                                //Console.WriteLine($"[文件夹] [创建] - {file.FullName} -> {savePath}/{fixname}");
                                //_ProgressBarMessage = $"[文件夹] [创建] - {file.FullName} -> {savePath}/{fixname}";
                            }
                            catch (Exception ex)
                            {
                                Console.Write($"[错误] - [文件夹创建失败] - [{ex.Message}] - {file.FullName} - <是否继续操作[Y/N]>");
                                log.Error($"[错误] - [文件夹创建失败] - [{ex.Message}] - {file.FullName}");
                                char input;
                                while (!char.TryParse(Console.ReadKey().KeyChar.ToString(), out input) || (input != 'Y' && input == 'y' && input != 'N' && input == 'n'))
                                {
                                    Console.Write("\n<是否继续操作[Y/N]>");
                                }
                                if (input == 'N' && input == 'n')
                                {
                                    Console.Write("按任意键以结束");
                                    Console.ReadLine();
                                    return;
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("\n[文件夹复制完毕，即将遍历文件]\n");
                log.Info("[文件夹复制完毕，即将遍历文件]");

                var File_options = new ProgressBarOptions
                {
                    ProgressCharacter = '─',
                    ProgressBarOnBottom = true
                };
                int File_ProgressBarValue = 0;
                int File_TotalProgressBarValue = allFileList.Count;
                string File_ProgressBarMessage = "正在处理中...";
                using (var File_progressBar = new ProgressBar(File_TotalProgressBarValue, File_ProgressBarMessage, File_options))
                {
                    await Task.Delay(500);
                    foreach (FileSystemInfo file in allFileList)
                    {
                        try
                        {
                            FileStream fs = null;
                            try
                            {
                                fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                            }
                            catch (UnauthorizedAccessException unauthExcep)
                            {
                                Console.Write($"[错误] - [无法访问该文件或文件夹] - {file.FullName} - <是否继续操作[Y/N]>");
                                log.Error($"[错误] - [无法访问该文件或文件夹] - {file.FullName}");
                                char input;
                                while (!char.TryParse(Console.ReadKey().KeyChar.ToString(), out input) || (input != 'Y' && input == 'y' && input != 'N' && input == 'n'))
                                {
                                    Console.Write("\n<是否继续操作[Y/N]>");
                                }
                                if (input == 'N' && input == 'n')
                                {
                                    Console.Write("按任意键以结束");
                                    Console.ReadLine();
                                    return;
                                }
                            }
                            int length = (int)fs.Length;  //读取长度（字节）
                            byte[] buffer = new byte[length];
                            try
                            {
                                fs.Seek(offset, SeekOrigin.Begin);  //定位到指定的偏移量处
                                fs.Read(buffer, 0, length); //从当前位置开始读取指定长度的数据
                                string fixname = file.FullName.Replace(SelectedDrive.Name, "");  //移除根目录
                                string targetPath = $"{savePath}/{fixname}";
                                try
                                {
                                    FileStream fs1 = null;
                                    try
                                    {
                                        File_ProgressBarValue++;
                                        //File_progressBar.Tick($"[{File_ProgressBarValue} / {File_TotalProgressBarValue}] [文件] [复制] - {file.FullName} -> {targetPath}");
                                        File_progressBar.Tick($"[{File_ProgressBarValue} / {File_TotalProgressBarValue}]");
                                        log.Info($"[{File_ProgressBarValue} / {File_TotalProgressBarValue}] [文件] [复制] - {file.FullName} -> {targetPath}");

                                        fs1 = new FileStream(targetPath, FileMode.Create, FileAccess.Write);
                                        fs1.Write(buffer, 0, buffer.Length);
                                        File.SetAttributes(targetPath, File.GetAttributes(file.FullName));
                                    }
                                    catch (UnauthorizedAccessException unauthExcep)
                                    {
                                        Console.Write($"[错误] - [无法访问该文件或文件夹] - {file.FullName} - <是否继续操作[Y/N]>");
                                        log.Error($"[错误] - [无法访问该文件或文件夹] - {file.FullName}");
                                        char input;
                                        while (!char.TryParse(Console.ReadKey().KeyChar.ToString(), out input) || (input != 'Y' && input == 'y' && input != 'N' && input == 'n'))
                                        {
                                            Console.Write("\n<是否继续操作[Y/N]>");
                                        }
                                        if (input == 'N' && input == 'n')
                                        {
                                            Console.Write("按任意键以结束");
                                            Console.ReadLine();
                                            return;
                                        }
                                    }
                                    finally { fs1.Close(); }

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally { fs.Close(); }
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.Write($"[错误] - [文件不存在] - {file.FullName} - <是否继续操作[Y/N]>");
                            log.Error($"[错误] - [文件不存在] - {file.FullName}");
                            char input;
                            while (!char.TryParse(Console.ReadKey().KeyChar.ToString(), out input) || (input != 'Y' && input == 'y' && input != 'N' && input == 'n'))
                            {
                                Console.Write("\n<是否继续操作[Y/N]>");
                            }
                            if (input == 'N' && input == 'n')
                            {
                                Console.Write("按任意键以结束");
                                Console.ReadLine();
                                return;
                            }
                        }
                    }

                }
            }
            else if (ActionTaken == 2)
            {
                while (true)
                {
                    bool ok1 = false;
                    Console.Write("要读取的文件路径: ");
                    var rawPath = Console.ReadLine();

                    /* 输入为空。 需要重新输入。 */
                    if (rawPath == "" || rawPath == null || Object.Equals(rawPath, null))
                    {
                        Console.WriteLine(" ┗ 请输入文件路径！");
                    }
                    else
                    {
                        ok1 = true;
                    }

                    if (ok1)
                    {
                        try
                        {
                            string path = $"{SelectedDrive.RootDirectory}{rawPath}";   //文件路径
                            FileStream fs = null;
                            try
                            {
                                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                            }
                            catch (UnauthorizedAccessException unauthExcep)
                            {
                                Console.WriteLine(" ┗ 无法访问该文件或文件夹");
                                log.Error("无法访问文件或文件夹 " + unauthExcep.Message);
                            }
                            Console.Write("请输入偏移量（单位：字节）[若无偏移则填写0<一般无偏移的文件均可以正常读取，无需使用本工具>]: ");
                            long offset = 0;
                            bool While_Bool = true;
                            while (While_Bool)
                            {
                                try
                                {
                                    offset = (long)Convert.ToDouble(Console.ReadLine()); //偏移量（字节）
                                    While_Bool = false;
                                }
                                catch (Exception) // FIXME: OutOfRangeException
                                {
                                    Console.WriteLine("超出范围(Out of range)");
                                    // Mark 4
                                    Console.Write("请输入偏移量（单位：字节）[若无偏移则填写0<一般无偏移的文件均可以正常读取，无需使用本工具>]: ");
                                }
                            }
                            int length = (int)fs.Length;  //读取长度（字节）
                            byte[] buffer = new byte[length];
                            try
                            {
                                fs.Seek(offset, SeekOrigin.Begin);  //定位到指定的偏移量处
                                //fs.Read(buffer, 0, length); //从当前位置开始读取指定长度的数据
                                fs.Read(buffer, 0, length); //从当前位置开始读取指定长度的数据
                                                            //Console.WriteLine(BitConverter.ToString(buffer));   //处理读取到的数据
                                                            //Console.WriteLine(Encoding.UTF8.GetString(buffer));

                                Console.Write("要保存的目录: ");
                                string savepath = Console.ReadLine();
                                if (savepath == "" || savepath == null || Object.Equals(savepath, null))
                                {
                                    Console.WriteLine(" ┗ 请输入文件夹路径！");
                                }
                                else
                                {
                                    ok1 = true;
                                }
                                string rawsavepath = $"{savepath}/{fs.Name}";
                                if (ok1)
                                {
                                    try
                                    {
                                        bool ok2 = false;
                                        FileStream fs1 = null;
                                        try
                                        {
                                            fs1 = new FileStream(rawsavepath, FileMode.Create, FileAccess.Write);
                                            fs1.Write(buffer, 0, buffer.Length);
                                            File.SetAttributes(rawsavepath, File.GetAttributes(path));
                                            Console.WriteLine(" ┗ 保存成功");
                                            log.Info("[文件] [保存] " + path + " -> " + rawsavepath);
                                        }
                                        catch (UnauthorizedAccessException unauthExcep)
                                        {
                                            Console.WriteLine(" ┗ 无法访问该文件或文件夹");
                                            log.Error("无法访问文件或文件夹 " + unauthExcep.Message);
                                        }
                                        finally { fs1.Close(); }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        log.Error(ex.Message);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                log.Error(ex.Message);
                            }
                            finally { fs.Close(); }
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine(" ┗ 文件不存在！");
                            log.Error("文件不存在 " + ex.Message);
                        }
                    }
                }
            }

            Console.WriteLine("所有操作已完成，按任意键以退出");
            Console.ReadKey();
        }

        static void PrintFileSystemInfo(FileSystemInfo fileSystemInfo, int level)
        {
            string prefix = new string('\t', level);

            // 打印文件或文件夹名称和完整路径
            //Console.WriteLine($"{prefix}{fileSystemInfo.Name} ({fileSystemInfo.FullName})");
            allFilesList.Add(fileSystemInfo);
            // 如果是文件夹，则继续遍历
            if (fileSystemInfo is DirectoryInfo)
            {
                allDirList.Add(fileSystemInfo);
                try
                {
                    FileSystemInfo[] fileSystemInfos = ((DirectoryInfo)fileSystemInfo).GetFileSystemInfos();
                    foreach (FileSystemInfo childInfo in fileSystemInfos)
                    {
                        PrintFileSystemInfo(childInfo, level + 1);
                    }
                } catch (UnauthorizedAccessException unauthExcep) {
                    Console.WriteLine($"{fileSystemInfo.FullName} - 权限不足");
                    log.Error($"{fileSystemInfo.FullName} - 权限不足"); // :)
                }
            } 
            else if (fileSystemInfo is FileInfo)
            {   
                allFileList.Add(fileSystemInfo);
            }
            
        }

        private static string outOfRangeExpression(long userInput, long left, long right, bool isLeftIncluded, bool isRightIncluded)
        {
            string precontent = "超出范围(Out of range)";
            return (precontent + $" {userInput} " + (isLeftIncluded ? "[" : "(") + $"{left}, {right}" + (isRightIncluded ? "]" : ")"));
        }
    }
}