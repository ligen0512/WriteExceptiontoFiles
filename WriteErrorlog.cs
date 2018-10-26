using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Net;
using System.Management;

using System.Windows.Forms;

namespace WriteExceptiontoFiles
{
    /// <summary>
    /// 将程序运行过程中的异常写入错误日志
    /// </summary>
    class WriteErrorlog
    {

        #region   //初次尝试的编写的方法
        string ErrorLogPath =Directory.GetCurrentDirectory ().Split(new String[] { @"\bin" }, 
          StringSplitOptions.None)[0] + @"\Error";
         
    /// <summary>
    /// 将异常写入对应的故障日志
    /// </summary>
    /// <param name="exstr"></param>
    /// <param name="errorDetails"></param>
    public void WriteError(System .Exception exstr,string errorDetails)
        {

            if (!Directory.Exists(ErrorLogPath))
            {
                Directory.CreateDirectory(ErrorLogPath);
            }

            string date = DateTime.Now.ToString("yyyyMMdd");
          
            string filePath = ErrorLogPath+@"\"+ date  +"errorlog.txt";  //按照触发异常的日期建立错误日志，便于查看

            if (!File.Exists (filePath))
            {
                File.Create(filePath).Close();
            }
             
            File.SetAttributes(filePath, FileAttributes.Normal);//设置文件的属性
            StreamWriter sr = new StreamWriter(filePath, true);
            sr.WriteLine("========="+DateTime.Now .ToString ("HH时:mm分:ss秒")+"=============");
            sr.WriteLine("故障信息:");
            sr.WriteLine(exstr .ToString ());
            sr.WriteLine("故障详细信息:");
            sr.WriteLine(errorDetails);
            sr.Close();
        }

        #endregion
        
        /// <summary>
        /// 将异常信息写入错误日志
        /// </summary>
        /// <param name="exstr">异常描述</param>
        /// <param name="errorDetails">详细异常信息</param>
        public WriteErrorlog(System.Exception exstr, string errorDetails)
        {

            if (!Directory.Exists(ErrorLogPath))  //判断对应的文件夹是否存在
            {
                Directory.CreateDirectory(ErrorLogPath);
            }


            string date = DateTime.Now.ToString("yyyyMMdd");

            string filePath = ErrorLogPath + @"\" + date + "errorlog.txt";  //按照触发异常的日期建立错误日志，便于查看
           

            if (!File.Exists(filePath)) //判断对应的错误日志文件是否存在
            {
                File.Create(filePath).Close();
            }
            
            
                File.SetAttributes(filePath, FileAttributes.Normal);//设置文件的属性
                StreamWriter sr = new StreamWriter(filePath, true);
                sr.WriteLine("=========" + DateTime.Now.ToString("HH时:mm分:ss秒") + "=============");
                sr.WriteLine("故障信息:");
                sr.WriteLine(exstr.ToString());
                sr.WriteLine("故障详细信息:");
                sr.WriteLine(errorDetails);
                sr.Close();

            string appName = Path.GetFileName(Application.ExecutablePath);


            //以当前运行的应用程序的名称和本机的名称写一个系统日志
            EventLog errorlog = new EventLog(appName, Dns.GetHostName(),appName);
            
            errorlog.WriteEntry("异常信息:" + exstr.ToString() + errorDetails,EventLogEntryType .Error);
            errorlog.Dispose();

        }

       
    }
}
