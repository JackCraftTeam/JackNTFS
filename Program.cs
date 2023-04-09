using System.Drawing;

namespace JackNTFS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用 JackNTFS 程序！\nWelcome to the JackNTFS program!\n[—————————————————————————————]");
            Console.WriteLine("按任意键以继续(Press any key to continue)");
            Console.ReadLine();
            List<DriveInfo> DriveInfoList = new List<DriveInfo>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                DriveInfoList.Add(drive);
                Console.WriteLine("盘符(Drive) {0}", drive.Name);
                Console.WriteLine("  ┣ 卷标(Volume label): {0}", drive.VolumeLabel);
                Console.WriteLine("  ┣ 文件系统(File system): {0}", drive.DriveFormat);
                Console.WriteLine("  ┣ 当前用户的可用空间(Available space to current user):{0, 15} 字节(bytes)",
                    drive.AvailableFreeSpace);

                if (drive.IsReady == true)
                {
                    Console.WriteLine("  ┣ 总可用空间(Total available space):                  {0, 15} 字节(bytes)",
                        drive.TotalFreeSpace);

                    Console.WriteLine("  ┗ 驱动器的总大小(Total size of drive):                {0, 15} 字节(bytes)",
                        drive.TotalSize);
                }
            }
            Console.Write($"[共(Total){DriveInfoList.Count}] [");
            int drive_int = 0;
            foreach (DriveInfo drive in DriveInfoList)
            {
                drive_int++;
                Console.Write($" {drive_int}-{drive.Name} ");
            }
            Console.Write("]");
            Console.WriteLine();
            Console.Write($"要读取的盘符[请输入上方数字<1-{DriveInfoList.Count}>](Drive letter to be read): ");
            bool whileBool = true;
            DriveInfo SelectedDrive = null;
            while (whileBool)
            {
                //Console.WriteLine("test");
                try
                {
                    string OperateDriveStr = Console.ReadLine();
                    int OperateDrive = Convert.ToInt16(OperateDriveStr);
                    if (OperateDrive >= 1 && OperateDrive <= DriveInfoList.Count)
                    {
                        SelectedDrive = DriveInfoList[OperateDrive-1];
                        whileBool = false;
                    }
                    else
                    {
                        Console.WriteLine("超出范围(Out of range)");
                        Console.Write($"要读取的盘符[请输入上方数字<1-{DriveInfoList.Count}>](Drive letter to be read): ");
                    }
                }
                catch
                {
                    Console.WriteLine("超出范围(Out of range)");
                    Console.Write($"要读取的盘符[请输入上方数字<1-{DriveInfoList.Count}>](Drive letter to be read): ");
                }
            }
            Console.WriteLine($"选择的盘符信息 {SelectedDrive.RootDirectory}");

            Console.WriteLine("盘符(Drive) {0}", SelectedDrive.Name);
            Console.WriteLine("  ┣ 卷标(Volume label): {0}", SelectedDrive.VolumeLabel);
            Console.WriteLine("  ┣ 文件系统(File system): {0}", SelectedDrive.DriveFormat);
            Console.WriteLine("  ┣ 当前用户的可用空间(Available space to current user):{0, 15} 字节(bytes)",
                SelectedDrive.AvailableFreeSpace);
            if (SelectedDrive.IsReady == true)
            {
                Console.WriteLine("  ┣ 总可用空间(Total available space):                  {0, 15} 字节(bytes)",
                    SelectedDrive.TotalFreeSpace);

                Console.WriteLine("  ┗ 驱动器的总大小(Total size of drive):                {0, 15} 字节(bytes)",
                    SelectedDrive.TotalSize);
            }

        }
    }
}