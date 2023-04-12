# JackNTFS

## 简介
本项目将在完成后立即用于 [Jack](https://github.com/imJack6) 的服务器修复。  
若您对此项目感兴趣，请联系 [Jack](https://github.com/imJack6) 或 [William](https://github.com/WilliamPascal) 以获取更多信息。
欢迎提交issues，也感谢点亮小星星~🖐️😃

## 功能
本项目希望能修复 [Jack](https://github.com/imJack6) 服务器中硬盘扇区数据偏移问题[^0]。  

~~*标有 null 代表数据未获得*~~  

~~1.  物理扇区大小 = null;~~  
    ~~逻辑扇区大小 = 512 Byte;~~  
~~2.  分区的起始位置 = null;~~  
~~3.  该分区在逻辑扇区的起始位置 = (分区的起始位置 / 逻辑扇区大小);~~  
~~4.  该分区在物理扇区的起始位置 = 该分区在逻辑扇区的起始位置 * 物理扇区大小;~~  
~~5.  扇区偏移量 = (实际偏移量 - 正常偏移量);~~  
~~[LINK](https://zhuanlan.zhihu.com/p/446972214)~~

Ha ha! William's here.  

Here, let me give you the simplest brief about my idea solving this
Jack-in-the-box.

As we know, Jack's SSD was formed as a GPT (GUID Partition Table).  
Thus, we could calculate the address of the very first partition.  
Since we've got a (probable) constant value of section shifting, why 
not enumerating through all the possibilities to examine?  
Look, the time complexity (A.K.A "BIG O") of this algorithm is simply
```
O(t) = (R/r)!

'R' stands for whole range of the porssibilities;
'r' stands for minimum resolution of enumerations (devision);
```
Since we have a constant range of all the possibilities:
```
[0, 1)

Unit is "per sector";
```
At the meanwhile, the minimum resolution of enumerations to this shifting is merely
```
1 Byte
```
So, suppose Jack got a GPT formed SSD, having exactly 1 partition in it.
And the size of this partition is, let's say, 256 GiB.

Then, we could clearly have the following data being wound to pop.
```
Since:   1 (GiB) = 1,024 (MiB)
                 = 1,048,576 (KiB)
                 = 1,073,741,824 (B)
Thus:  256 (GiB) = 274,877,906,944 (B)

Since: Time complexity formular is:
       O(t) = (R/r)!
Thus:  O(t) = (R/r)!
            = (274,877,906,944 (B) / 1 (B))!
            = 274,877,906,944!
            = INF
```
Well, is looks like the data is too heavy to pop up.

Let's see, that was the very first idea, by the way.  
So, there must be somewhere could be simplified.

// HERE

## 背景
~~根据对事件回忆，在对指定硬盘的唯一分区进行无擦除压缩后人为接收到程序报错[^1]，随后运行了 “chkdsk e: /x /r /f” 命令，最终导致部分文件无法被正常读取[^2]。  
目前已知该事故导致近 200 GiB 文件无法访问。~~  

[^0]: 文件系统为 NTFS  
[^1]: 据说是 File system error  
[^2]: 部分文件完全不可读，部分文件部分可读。（均为正确文字解码的情况）  
