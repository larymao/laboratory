using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     <para/>Facebook gragh api photo node. Represents an individual photo on Facebook.
    ///     <para/>Any valid access token can read photos on a public Page.
    ///     <para/>A page access token can read all photos posted to or posted by that Page.
    ///     <para/>The current user's photos can be read if the user has granted the user_photos or user_posts permission.
    ///     <para/>A user access token may read a photo that the current user is tagged in if they have granted the 
    ///     user_photos or user_posts permission.However, in some cases the photo's owner's privacy settings may not 
    ///     allow your application to access it.
    ///     <para/>A User access token for an Admin of a Group can read Group-owned Photos.
    ///     <para/>A User access token for an Admin of an Event can read Event-owned Photos if required after April 30, 2018.
    /// </summary>
    public partial class Photo
    {
        
    }
}
