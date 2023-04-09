using System.Drawing;
using System.IO;
using System.Text;

namespace JackNTFS
{
    internal class Program
    {
        static List<FileSystemInfo> AllFilesList = new List<FileSystemInfo>();

        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用 JackNTFS 程序！\n[—————————————————————————————]");
            Console.WriteLine("按任意键以继续");
            Console.ReadLine();
            List<DriveInfo> DriveInfoList = new List<DriveInfo>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                DriveInfoList.Add(drive);
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
            Console.Write($"[共{DriveInfoList.Count}个盘符] [");
            int drive_int = 0;
            foreach (DriveInfo drive in DriveInfoList)
            {
                drive_int++;
                Console.Write($" {drive_int}-{drive.Name} ");
            }
            Console.Write("]");
            Console.WriteLine();
            Console.Write($"要读取的盘符 [请输入上方数字<1-{DriveInfoList.Count}>]: ");
            bool whileBool = true;
            DriveInfo SelectedDrive = null;
            while (whileBool)
            {
                //Console.WriteLine("test");
                try
                {
                    int OperateDrive = Convert.ToInt16(Console.ReadLine());
                    if (OperateDrive >= 1 && OperateDrive <= DriveInfoList.Count)
                    {
                        SelectedDrive = DriveInfoList[OperateDrive-1];
                        whileBool = false;
                    }
                    else
                    {
                        Console.WriteLine("超出范围(Out of range)");
                        Console.Write($"要读取的盘符[请输入上方数字<1-{DriveInfoList.Count}>]: ");
                    }
                }
                catch
                {
                    Console.WriteLine("超出范围(Out of range)");
                    Console.Write($"要读取的盘符[请输入上方数字<1-{DriveInfoList.Count}>]: ");
                }
            }
            Console.WriteLine($" ┗ 当前选择的盘符: {SelectedDrive.RootDirectory}");
            Console.Write($"要进行的操作 [ 1-全盘读取 2-单文件读取]: ");
            whileBool = true;
            int ActionTaken = 0;
            while (whileBool)
            {
                //Console.WriteLine("test");
                try
                {
                    int ActionDrive = Convert.ToInt16(Console.ReadLine());
                    if (ActionDrive >= 1 && ActionDrive <= 2)
                    {
                        ActionTaken = ActionDrive;
                        whileBool = false;
                    }
                    else
                    {
                        Console.WriteLine("超出范围(Out of range)");
                        Console.Write($"要进行的操作 [ 1-全盘读取 2-单文件读取]: ");
                    }
                }
                catch
                {
                    Console.WriteLine("超出范围(Out of range)");
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
                bool While_Bool = true;
                while (While_Bool)
                {
                    Console.Write("要保存到的文件夹或盘符: ");
                    string rawpath = Console.ReadLine();

                    if (rawpath == "" || rawpath == null || Object.Equals(rawpath, null))
                    {
                        Console.WriteLine(" ┗ 请输入路径！");
                    }
                    else
                    {
                        While_Bool = false;
                    }
                }

                DirectoryInfo directory = new DirectoryInfo(SelectedDrive.Name);

                PrintFileSystemInfo(directory, 0);

                foreach (FileSystemInfo file in AllFilesList)
                {
                    if (file.FullName != SelectedDrive.Name)
                    {
                        Console.WriteLine(file.FullName);

                    }
                    
                }

                Console.ReadLine();
            }
            else if (ActionTaken == 2)
            {
                while (true)
                {
                    bool ok1 = false;
                    Console.Write("要读取的文件路径: ");
                    string rawpath = Console.ReadLine();

                    if (rawpath == "" || rawpath == null || Object.Equals(rawpath, null))
                    {
                        Console.WriteLine(" ┗ 请输入文件路径！");
                    } else
                    {
                        ok1 = true;
                    }

                    if (ok1)
                    {
                        try
                        {
                            string path = $"{SelectedDrive.RootDirectory}{rawpath}";   //文件路径
                            FileStream fs = null;
                            try
                            {
                                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                            }
                            catch (UnauthorizedAccessException unauthExcep)
                            {
                                Console.WriteLine(" ┗ 无法访问该文件或文件夹");
                            }
                            Console.Write("请输入偏移量（单位：字节）[若无偏移则填写0<一般无偏移的文件均可以正常读取，无需使用本工具>]: ");
                            long offset = (long)Convert.ToDouble(Console.ReadLine()); //偏移量（字节）
                            int length = (int)fs.Length;  //读取长度（字节）
                            byte[] buffer = new byte[length];
                            try
                            {
                                fs.Seek(offset, SeekOrigin.Begin);  //定位到指定的偏移量处
                                //fs.Read(buffer, 0, length); //从当前位置开始读取指定长度的数据
                                fs.Read(buffer, 0, length); //从当前位置开始读取指定长度的数据
                                //Console.WriteLine(BitConverter.ToString(buffer));   //处理读取到的数据
                                //Console.WriteLine(Encoding.UTF8.GetString(buffer));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally { fs.Close(); }

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
                            if (ok1)
                            {
                                try
                                {
                                    bool ok2 = false;
                                    FileStream fs1 = null;
                                    try
                                    {
                                        fs1 = new FileStream(savepath, FileMode.Create, FileAccess.Write);
                                        fs1.Write(buffer, 0, buffer.Length);
                                        Console.WriteLine(" ┗ 保存成功");
                                    }
                                    catch (UnauthorizedAccessException unauthExcep)
                                    {
                                        Console.WriteLine(" ┗ 无法访问该文件或文件夹");
                                    }
                                    finally { fs1.Close(); }

                                } catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine(" ┗ 文件不存在！");
                        }
                    }


                }

            }
        }

        static void PrintFileSystemInfo(FileSystemInfo fileSystemInfo, int level)
        {
            string prefix = new string('\t', level);
            
            // 打印文件或文件夹名称和完整路径
            //Console.WriteLine($"{prefix}{fileSystemInfo.Name} ({fileSystemInfo.FullName})");
            AllFilesList.Add(fileSystemInfo);
            // 如果是文件夹，则继续遍历
            if (fileSystemInfo is DirectoryInfo)
            {
                try
                {
                    FileSystemInfo[] fileSystemInfos = ((DirectoryInfo)fileSystemInfo).GetFileSystemInfos();
                    foreach (FileSystemInfo childInfo in fileSystemInfos)
                    {
                        PrintFileSystemInfo(childInfo, level + 1);
                    }
                } catch (UnauthorizedAccessException unauthExcep)
                {
                    Console.WriteLine($"{fileSystemInfo.FullName} - 权限不足");
                }

            }
        }
    }
}