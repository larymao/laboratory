using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     <para/>Represents an individual video on Facebook.
    ///     <para/>Any valid access token can read videos on a public Page.
    ///     <para/>A page access token can read all videos posted to or posted by that Page.
    ///     <para/>A user access token can read any video your application created on behalf of that user.
    ///     <para/>A user's videos can be read if the owner has granted the user_videos or user_posts permission.
    ///     <para/>A user access token may read a video that user is tagged in if they user granted the user_videos 
    ///     or user_posts permission. However, in some cases the video's owner's privacy settings may not allow your 
    ///     application to access it.
    ///     <para/>The source field will not be returned for Page-owned videos unless the User making the request has 
    ///     a role on the owning Page.
    /// </summary>
    public partial class Video
    {
    }
}
