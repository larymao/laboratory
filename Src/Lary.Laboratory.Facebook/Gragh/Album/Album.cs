using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Gragh
{
    /// <summary>
    ///     <para/>Facebook gragh api photo node. Represents a photo album.
    ///     <para/>For Albums on Users and Pages:
    ///     <para/>Any valid access token if the album is public.
    ///     <para/>A user access token with user_photos permission to retrieve any albums that the session user has uploaded.
    ///     <para/>For Albums on Groups:
    ///     <para/>A User access token of an Admin of the Group with the user_photos permission.
    /// </summary>
    public partial class Album
    {
    }
}
