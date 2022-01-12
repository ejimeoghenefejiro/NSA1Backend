using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSA1.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NSA1.Core.Utilities
{
    public static class CommonExtension
    {

        public static long ToTimeStamp(this DateTimeOffset date)
        {

            return date.ToUnixTimeMilliseconds();
        }

        public static DateTime ToDateTime(this long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);

            return dtDateTime.DateTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiResponse"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse<object> ToResponse(this object apiResponse, string message = "")
        {
            return new ApiResponse<object> { Data = apiResponse, Message = message };
        }


        public static string ExtractErrorString(this ModelStateDictionary model)
        {
            return string.Join(',', model.
                Values.
                SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)


                );
        }

        public static int GetNumbersFromString(string alphanumeric)
        {
            int num = 0;
            string[] numbers = Regex.Split(alphanumeric, @"\D+");
            if (numbers != null)
            {
                string longest = numbers.OrderByDescending(s => s.Length).First();
                num = int.Parse(longest);
            }
            return num;



        }
        public static byte[] ReadAllBytes(Stream source)
        {
            long originalPosition = source.Position;
            source.Position = 0;

            try
            {
                byte[] readBuffer = new byte[4096];
                int totalBytesRead = 0;
                int bytesRead;
                while ((bytesRead = source.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;
                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = source.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                source.Position = originalPosition;
            }
        }

    }
}
