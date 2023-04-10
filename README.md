# JackNTFS

## 简介
本项目将在完成后立即用于 [Jack](https://github.com/imJack6) 的服务器修复。  
若您对此项目感兴趣，请联系 [Jack](https://github.com/imJack6) 或 [William](https://github.com/WilliamPascal) 以获取更多信息。  

## 功能
本项目希望能修复 [Jack](https://github.com/imJack6) 服务器中硬盘扇区数据偏移问题[^0]。  

*标有 null 代表数据未获得*  

1.  物理扇区大小 = null;  
    逻辑扇区大小 = 512 Byte;  
2.  分区的起始位置 = null;  
3.  该分区在逻辑扇区的起始位置 = (分区的起始位置 / 逻辑扇区大小);  
4.  该分区在物理扇区的起始位置 = 该分区在逻辑扇区的起始位置 * 物理扇区大小;  
5.  扇区偏移量 = (实际偏移量 - 正常偏移量);  
[LINK](https://zhuanlan.zhihu.com/p/446972214)  

## 背景
根据对事件回忆，在对指定硬盘的唯一分区进行无擦除压缩后人为接收到程序报错[^1]，随后运行了 “chkdsk e: /x /r /f” 命令，最终导致部分文件无法被正常读取[^2]。  
目前已知该事故导致近 200 GiB 文件无法访问。  

[^0]: 文件系统为 NTFS  
[^1]: 据说是 File system error  
[^2]: 部分文件完全不可读，部分文件部分可读。（均为正确文字解码的情况）  
