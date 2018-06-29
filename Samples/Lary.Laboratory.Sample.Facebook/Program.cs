using Lary.Laboratory.Facebook.Interfaces;
using Lary.Laboratory.Facebook.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Lary.Laboratory.Sample.Facebook
{
    internal class Program
    {
        private static Config _config;
        private static string _data;

        private static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                PrintUsage();
            }
            else
            {
                var mode = args[0];
                _config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(args[1]));
                _data = File.ReadAllText(args[2]);
                
                switch (mode)
                {
                    case "text":
                        PublishTextPost();
                        break;
                    case "link":
                        PublishLinkPost();
                        break;
                    case "image":
                        PublishImagePost();
                        break;
                    default:
                        PrintUsage();
                        break;
                }
            }

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }


        /// <summary>
        ///     Publishes facebook page post with text only.
        /// </summary>
        private static void PublishTextPost()
        {
            var post = JsonConvert.DeserializeObject<TextPost>(_data);

            var response = post.PostAsync(_config).GetAwaiter().GetResult();
            Console.WriteLine(response.ToString(true));
        }

        /// <summary>
        ///     Publish facebook page post with link.
        /// </summary>
        private static void PublishLinkPost()
        {
            var post = JsonConvert.DeserializeObject<LinkPost>(_data);

            var response = post.PostAsync(_config).GetAwaiter().GetResult();
            Console.WriteLine(response.ToString(true));
        }

        /// <summary>
        ///     Publish facebook page post with image.
        /// </summary>
        private static void PublishImagePost()
        {
            var post = JsonConvert.DeserializeObject<ImagePost>(_data);

            var response = post.PostAsync(_config).GetAwaiter().GetResult();
            Console.WriteLine(response.ToString(true));
        }


        private static void PrintUsage()
        {
            Console.WriteLine("Usage: .. <text|link|image|video|imagead|videoad> <config file> <data file>");
        }
    }
}
