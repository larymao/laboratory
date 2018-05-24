using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lary.Laboratory.Core.Utils
{
    /// <summary>
    ///     Provides crypto methods.
    /// </summary>
    public static class CryptoUtil
    {
        /// <summary>
        ///     Converts an array of 8-bit unsigned integers to its equivalent string representation
        ///     that is encoded with base-64 digits.
        /// </summary>
        /// <param name="data">
        ///     Data to encode. An array of 8-bit unsigned integers.
        /// </param>
        /// <returns>
        ///     The string representation, in base 64, of the contents of data.
        /// </returns>
        public static string Base64Encode(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        /// <summary>
        ///     Converts a string to its equivalent string representation that is encoded with 
        ///     base-64 digits.
        /// </summary>
        /// <param name="data">
        ///     String data to encode.
        /// </param>
        /// <returns>
        ///     The string representation, in base 64, of the contents of data.
        /// </returns>
        public static string Base64Encode(string data)
        {
            var bytes = Encoding.Default.GetBytes(data);
            return Base64Encode(bytes);
        }

        /// <summary>
        ///     Converts the specified string, which encoded as base-64 digits, to an equivalent
        ///     normal string.
        /// </summary>
        /// <param name="data">
        ///     The string to convert.
        /// </param>
        /// <returns>
        ///     A normal string that is equivalent to data.
        /// </returns>
        public static string Base64Decode(string data)
        {
            var bytes = Convert.FromBase64String(data);
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        ///     Converts an array of 8-bit unsigned integers to its equivalent string representation
        ///     that is encoded with base-64-url digits.
        /// </summary>
        /// <param name="data">
        ///     Data to encode. An array of 8-bit unsigned integers.
        /// </param>
        /// <returns>
        ///     The string representation, in base 64 url, of the contents of data.
        /// </returns>
        public static string Base64UrlEncode(byte[] data)
        {
            var result = Base64Encode(data);
            result = result.TrimEnd('=') // Remove any trailing '='s.
                           .Replace('+', '-') // 62nd char of encoding.
                           .Replace('/', '_'); // 63rd char of encoding.

            return result;
        }

        /// <summary>
        ///     Converts a string to its equivalent string representation that is encoded with
        ///     base-64-url digits.
        /// </summary>
        /// <param name="data">
        ///     String data to encode.
        /// </param>
        /// <returns>
        ///     The string representation, in base 64 url, of the contents of data.
        /// </returns>
        public static string Base64UrlEncode(string data)
        {
            var bytes = Encoding.Default.GetBytes(data);
            return Base64UrlEncode(bytes);
        }

        /// <summary>
        ///     Converts the specified string, which encoded as base-64-url digits, to an equivalent
        ///     normal string.
        /// </summary>
        /// <param name="data">
        ///     The string to convert.
        /// </param>
        /// <returns>
        ///     A normal string that is equivalent to data.
        /// </returns>
        public static string Base64UrlDecode(string data)
        {
            var result = data.Replace('-', '+') // 62nd char of encoding.
                             .Replace('_', '/'); // 63rd char of encoding.

            // Pad with trailing '='s.
            switch (result.Length % 4) 
            {
                case 0: break; // No pad chars in this case.
                case 2: result += "=="; break; // Two pad chars.
                case 3: result += "="; break; // One pad char.
                default:
                    throw new Exception("Illegal base64url string!");
            }

            return Base64Decode(result);
        }

        /// <summary>
        ///     Computes the hash value for the specified byte array.
        /// </summary>
        /// <param name="source">
        ///     The byte array source data to compute the hash code for.
        /// </param>
        /// <param name="key">
        ///     The secret key for System.Security.Cryptography.HMACSHA1 encryption. The key
        ///     can be any length, but if it is more than 64 bytes long it is hashed (using SHA-1)
        ///     to derive a 64-byte key. Therefore, the recommended size of the secret key is
        ///     64 bytes.
        /// </param>
        /// <returns>
        ///     The computed hash code.
        /// </returns>
        public static byte[] HMACSHA1(byte[] source, byte[] key)
        {
            HMACSHA1 hma = new HMACSHA1(key);
            return hma.ComputeHash(source);
        }

        /// <summary>
        ///     Computes the hash value for the specified string source.
        /// </summary>
        /// <param name="source">
        ///     The string source data to compute the hash code for.
        /// </param>
        /// <param name="key">
        ///     The secret key for System.Security.Cryptography.HMACSHA1 encryption. The key
        ///     can be any length, but if it is more than 64 bytes long it is hashed (using SHA-1)
        ///     to derive a 64-byte key. Therefore, the recommended size of the secret key is
        ///     64 bytes.
        /// </param>
        /// <param name="encoding">
        ///     The character encoding of key.
        /// </param>
        /// <returns>
        ///     The computed hash code.
        /// </returns>
        public static byte[] HMACSHA1(string source, string key, Encoding encoding)
        {
            HMACSHA1 hma = new HMACSHA1(encoding.GetBytes(key));
            return hma.ComputeHash(encoding.GetBytes(source));
        }

        /// <summary>
        ///     Computes the md5 value for the specified string source.
        /// </summary>
        /// <param name="source">
        ///     The string source data to compute the md5 code for.
        /// </param>
        /// <param name="format">
        ///     A numeric format string.
        /// </param>
        /// <returns>
        ///     The computed md5 code.
        /// </returns>
        public static string Md5(string source, string format = "x2")
        {
            byte[] sor = Encoding.UTF8.GetBytes(source);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strbul.Append(result[i].ToString(format));
            }

            return strbul.ToString();
        }

        /// <summary>
        ///     Converts a string to its escaped representation.
        /// </summary>
        /// <param name="source">
        ///     The string to encode.
        /// </param>
        /// <returns>
        ///     An url encoded string that is equivalent to data.
        /// </returns>
        public static string UrlEncode(string source)
        {
            return Uri.EscapeDataString(source);
        }
    }
}
