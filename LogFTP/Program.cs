using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace LogFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileList = Directory.GetFiles(@"C:\Log");

            foreach (var item in fileList)
            {
                EnviarArquivoFTP(item, "", "", "").Wait();
            }           
        }

        public static async Task EnviarArquivoFTP(string arquivo, string url, string usuario, string senha)
        {
            try
            {
                FileInfo arquivoInfo = new FileInfo(arquivo);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(url));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(usuario, senha);
                request.UseBinary = true;
                request.ContentLength = arquivoInfo.Length;

                using (FileStream fs = arquivoInfo.OpenRead())
                {
                    byte[] buffer = new byte[2048];
                    int bytesSent = 0;
                    int bytes = 0;
                    using (Stream stream = request.GetRequestStream())
                    {
                        while (bytesSent < arquivoInfo.Length)
                        {
                            bytes = fs.Read(buffer, 0, buffer.Length);
                            stream.Write(buffer, 0, bytes);
                            bytesSent += bytes;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
