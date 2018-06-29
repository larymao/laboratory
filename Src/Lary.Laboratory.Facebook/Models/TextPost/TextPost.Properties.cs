using Lary.Laboratory.Core.Models;
using Lary.Laboratory.Facebook.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lary.Laboratory.Facebook.Models
{
    /// <summary>
    ///     Facebook text post.
    /// </summary>
    public partial class TextPost
    {
        /// <summary>
        ///     The text message of post.
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        ///     The schedule publish time of post.
        /// </summary>
        public DateTime ScheduledTime { get; set; }
    }
}
