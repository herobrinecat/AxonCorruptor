using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxonCorruptor
{
    public class FileUtils
    {
        public static void OverwriteBytesinFile(byte[] buffer, FileStream stream)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                stream.WriteByte(buffer[i]);
            }
        }
    }
}
