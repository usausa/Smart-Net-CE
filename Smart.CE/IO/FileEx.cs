namespace Smart.IO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    ///
    /// </summary>
    public static class FileEx
    {
        //--------------------------------------------------------------------------------
        // Attribute
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileAttributes GetAttributes(string path)
        {
            var fileAttributes = NativeMethods.GetFileAttributes(path);
            if (fileAttributes != uint.MaxValue)
            {
                return (FileAttributes)fileAttributes;
            }

            var error = Marshal.GetLastWin32Error();
            if ((error != 2) && (error != 3))
            {
                throw new Win32Exception(error);
            }

            throw new FileNotFoundException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileAttributes"></param>
        public static void SetAttributes(string path, FileAttributes fileAttributes)
        {
            if (!NativeMethods.SetFileAttributes(path, (uint)fileAttributes))
            {
                var error = Marshal.GetLastWin32Error();
                if ((error != 2) && (error != 3))
                {
                    throw new Win32Exception(error);
                }

                throw new FileNotFoundException();
            }
        }

        //--------------------------------------------------------------------------------
        // Read/Write
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        public static void AppendAllLines(string path, IEnumerable<string> contents)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            using (var writer = new StreamWriter(path, true))
            {
                foreach (var str in contents)
                {
                    writer.WriteLine(str);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        /// <param name="encoding"></param>
        public static void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            using (var writer = new StreamWriter(path, true, encoding))
            {
                foreach (var str in contents)
                {
                    writer.WriteLine(str);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        public static void AppendAllText(string path, string contents)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            using (var writer = new StreamWriter(path, true))
            {
                writer.Write(contents);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        /// <param name="encoding"></param>
        public static void AppendAllText(string path, string contents, Encoding encoding)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            using (var writer = new StreamWriter(path, true, encoding))
            {
                writer.Write(contents);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            using (var fs = new FileStream(path, FileMode.Open))
            {
                var buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                return buffer;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] ReadAllLines(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            var list = new List<string>();
            using (var reader = new StreamReader(path))
            {
                for (var str = reader.ReadLine(); !string.IsNullOrEmpty(str); str = reader.ReadLine())
                {
                    list.Add(str);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string[] ReadAllLines(string path, Encoding encoding)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            var list = new List<string>();
            using (var reader = new StreamReader(path, encoding))
            {
                for (var str = reader.ReadLine(); !string.IsNullOrEmpty(str); str = reader.ReadLine())
                {
                    list.Add(str);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadAllText(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadAllText(string path, Encoding encoding)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            using (var reader = new StreamReader(path, encoding))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IEnumerable<string> ReadLines(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            using (var reader = new StreamReader(path))
            {
                string result;
                while ((result = reader.ReadLine()) != null)
                {
                    yield return result;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static IEnumerable<string> ReadLines(string path, Encoding encoding)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            using (var reader = new StreamReader(path, encoding))
            {
                string result;
                while ((result = reader.ReadLine()) != null)
                {
                    yield return result;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        public static void WriteAllBytes(string path, byte[] bytes)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            using (var fs = new FileStream(path, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        public static void WriteAllLines(string path, IEnumerable<string> contents)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            using (var writer = new StreamWriter(path, false))
            {
                foreach (var str in contents)
                {
                    writer.WriteLine(str);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        /// <param name="encoding"></param>
        public static void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            using (var writer = new StreamWriter(path, false, encoding))
            {
                foreach (var str in contents)
                {
                    writer.WriteLine(str);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        public static void WriteAllText(string path, string contents)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            using (var writer = new StreamWriter(path, false))
            {
                writer.Write(contents);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        /// <param name="encoding"></param>
        public static void WriteAllText(string path, string contents, Encoding encoding)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (contents == null)
            {
                throw new ArgumentNullException("contents");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            using (var writer = new StreamWriter(path, false, encoding))
            {
                writer.Write(contents);
            }
        }
    }
}
