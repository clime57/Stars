

using UnityEngine;
using System.Collections;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Core;

using System.IO;
using System;
/// <summary>
/// Zip压缩与解压缩 
/// </summary>
public class ZipHelper
{
    /// <summary>
    /// 压缩单个文件
    /// </summary>
    /// <param name="fileToZip">要压缩的文件</param>
    /// <param name="zipedFile">压缩后的文件</param>
    /// <param name="compressionLevel">压缩等级</param>
    /// <param name="blockSize">每次写入大小</param>
    public static void ZipFile(string fileToZip, string zipedFile, int compressionLevel, int blockSize)
    {
        string fileToZipFix = fileToZip.Replace("\\", "/");
        //如果文件没有找到，则报错
        if (!System.IO.File.Exists(fileToZipFix))
        {
            throw new System.IO.FileNotFoundException("指定要压缩的文件: " + fileToZipFix + " 不存在!");
        }
 
        using (System.IO.FileStream ZipFile = System.IO.File.Create(zipedFile))
        {
            using (ZipOutputStream ZipStream = new ZipOutputStream(ZipFile))
            {
                using (System.IO.FileStream StreamToZip = new System.IO.FileStream(fileToZipFix, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    string fileName = fileToZipFix.Substring(fileToZipFix.LastIndexOf("/") + 1);
 
                    ZipEntry ZipEntry = new ZipEntry(fileName);
 
                    ZipStream.PutNextEntry(ZipEntry);
 
                    ZipStream.SetLevel(compressionLevel);
 
                    byte[] buffer = new byte[blockSize];
 
                    int sizeRead = 0;
 
                    try
                    {
                        do
                        {
                            sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                            ZipStream.Write(buffer, 0, sizeRead);
                        }
                        while (sizeRead > 0);
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
 
                    StreamToZip.Close();
                }
 
                ZipStream.Finish();
                ZipStream.Close();
            }
 
            ZipFile.Close();
        }
    }
 
    /// <summary>
    /// 压缩单个文件
    /// </summary>
    /// <param name="fileToZip">要进行压缩的文件名</param>
    /// <param name="zipedFile">压缩后生成的压缩文件名</param>
    public static void ZipFile(string fileToZip, string zipedFile)
    {
        string fileToZipFix = fileToZip.Replace("\\", "/");
        //如果文件没有找到，则报错
        if (!File.Exists(fileToZipFix))
        {
            throw new System.IO.FileNotFoundException("指定要压缩的文件: " + fileToZipFix + " 不存在!");
        }

        using (FileStream fs = File.OpenRead(fileToZipFix))
        {
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
 
            using (FileStream ZipFile = File.Create(zipedFile))
            {
                using (ZipOutputStream ZipStream = new ZipOutputStream(ZipFile))
                {
                    string fileName = fileToZipFix.Substring(fileToZipFix.LastIndexOf("/") + 1);
                    ZipEntry ZipEntry = new ZipEntry(fileName);
                    ZipStream.PutNextEntry(ZipEntry);
                    ZipStream.SetLevel(5);
 
                    ZipStream.Write(buffer, 0, buffer.Length);
                    ZipStream.Finish();
                    ZipStream.Close();
                }
            }
        }
    }
 
    /// <summary>
    /// 压缩多层目录
    /// </summary>
    /// <param name="strDirectory">The directory.</param>
    /// <param name="zipedFile">The ziped file.</param>
    public static void ZipFileDirectory(string strDirectory, string zipedFile)
    {
        using (System.IO.FileStream ZipFile = System.IO.File.Create(zipedFile))
        {
            using (ZipOutputStream s = new ZipOutputStream(ZipFile))
            {
                string strDirectoryCopy = string.Copy(strDirectory);
                strDirectoryCopy = strDirectoryCopy.Replace("\\", "/");
                ZipSetp(strDirectoryCopy, s, "");
            }
        }
    }
 
    /// <summary>
    /// 递归遍历目录
    /// </summary>
    /// <param name="strDirectory">The directory.必须提前将所有"\\"替换成"/"</param>
    /// <param name="s">The ZipOutputStream Object.</param>
    /// <param name="parentPath">The parent path.</param>
    private static void ZipSetp(string strDirectory, ZipOutputStream s, string parentPath)
    {
        if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
        {
            strDirectory += Path.DirectorySeparatorChar;
        }           
        Crc32 crc = new Crc32();
 
        string[] filenames = Directory.GetFileSystemEntries(strDirectory);
 
        foreach (string file in filenames)// 遍历所有的文件和目录
        {
 
            if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
            {
                string pPath = parentPath;
                pPath += file.Substring(file.LastIndexOf("/") + 1);
                pPath += "/";
                ZipSetp(file, s, pPath);
            }
 
            else // 否则直接压缩文件
            {
                //打开压缩文件
                using (FileStream fs = File.OpenRead(file))
                {
 
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
 
                    string fileName = parentPath + file.Substring(file.LastIndexOf("/") + 1);
                    ZipEntry entry = new ZipEntry(fileName);
 
                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
 
                    fs.Close();
 
                    crc.Reset();
                    crc.Update(buffer);
 
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
 
                    s.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
 
    /// <summary>
    /// 解压缩一个 zip 文件。
    /// </summary>
    /// <param name="zipedFile">The ziped file.</param>
    /// <param name="strDirectory">The STR directory.</param>
    /// <param name="password">zip 文件的密码。</param>
    /// <param name="overWrite">是否覆盖已存在的文件。</param>
    public static void UnZip(string zipedFile, string strDirectory, string password, bool overWrite)
    {
        Stream compressedStream = File.OpenRead(zipedFile);
        UnZip(compressedStream, strDirectory, password, overWrite);
    }
    /// <summary>
    /// 解压缩一个存放于字节数组中的zip包。
    /// </summary>
    /// <param name="compressedData">含有Zip包的字节数组</param>
    /// <param name="strDirectory">The STR directory.</param>
    /// <param name="password">zip 文件的密码。</param>
    /// <param name="overWrite">是否覆盖已存在的文件。</param>
    public static void UnZip(byte[] compressedData, string strDirectory, string password, bool overWrite)
    {
        Stream compressedStream = new MemoryStream(compressedData);
        UnZip(compressedStream, strDirectory, password, overWrite);
    }
    /// <summary>
    /// 解压缩一个存放于流中的zip包。
    /// </summary>
    /// <param name="compressedData">含有Zip包的流</param>
    /// <param name="strDirectory">The STR directory.</param>
    /// <param name="password">zip 文件的密码。</param>
    /// <param name="overWrite">是否覆盖已存在的文件。</param>
    public static void UnZip(Stream compressedStream, string strDirectory, string password, bool overWrite)
    {
        if (strDirectory == "")
            strDirectory = Directory.GetCurrentDirectory();
        string strDirectoryCopy = string.Copy(strDirectory);
        strDirectoryCopy = strDirectoryCopy.Replace("\\", "/");
        if (!strDirectoryCopy.EndsWith("/"))
            strDirectoryCopy = strDirectoryCopy + "/";
        using (ZipInputStream s = new ZipInputStream(compressedStream))
        {
            s.Password = password;
            ZipEntry theEntry;

            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = "";
                string pathToZip = "";
                pathToZip = theEntry.Name;

                if (pathToZip != "")
                    directoryName = Path.GetDirectoryName(pathToZip);
                if (directoryName != "")
                {
                    directoryName += "/";
                }

                string fileName = Path.GetFileName(pathToZip);

                Directory.CreateDirectory(strDirectoryCopy + directoryName);

                if (fileName != "")
                {
                    if ((File.Exists(strDirectoryCopy + directoryName + fileName) && overWrite) || (!File.Exists(strDirectoryCopy + directoryName + fileName)))
                    {
                        using (FileStream streamWriter = File.Create(strDirectoryCopy + directoryName + fileName))
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);

                                if (size > 0)
                                    streamWriter.Write(data, 0, size);
                                else
                                    break;
                            }
                            streamWriter.Close();
                        }
                    }
                }
            }

            s.Close();
        }
    }
}
