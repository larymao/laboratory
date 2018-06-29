using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Models
{
    /// <summary>
    ///     Call to action.
    /// </summary>
    public class Call2Action
    {
        /// <summary>
        ///     The <see cref="Call2ActionType"/> of current action.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     The link address of current action.
        /// </summary>
        public string Link { get; set; }
    }

    /// <summary>
    ///     Call to action type.
    /// </summary>
    public static class Call2ActionType
    {
        /// <summary>
        ///     Action: Open link.
        /// </summary>
        public const string OPEN_LINK = "OPEN_LINK";

        /// <summary>
        ///     Action: Like page.
        /// </summary>
        public const string LIKE_PAGE = "LIKE_PAGE";

        /// <summary>
        ///     Action: Shop now.
        /// </summary>
        public const string SHOP_NOW = "SHOP_NOW";

        /// <summary>
        ///     Action: Play game.
        /// </summary>
        public const string PLAY_GAME = "PLAY_GAME";

        /// <summary>
        ///     Action: Install app.
        /// </summary>
        public const string INSTALL_APP = "INSTALL_APP";

        /// <summary>
        ///     Action: User app.
        /// </summary>
        public const string USE_APP = "USE_APP";

        /// <summary>
        ///     Action: Install mobile app.
        /// </summary>
        public const string INSTALL_MOBILE_APP = "INSTALL_MOBILE_APP";

        /// <summary>
        ///     Action: User mobile app.
        /// </summary>
        public const string USE_MOBILE_APP = "USE_MOBILE_APP";

        /// <summary>
        ///     Action: Book travel.
        /// </summary>
        public const string BOOK_TRAVEL = "BOOK_TRAVEL";

        /// <summary>
        ///     Action: Listen music.
        /// </summary>
        public const string LISTEN_MUSIC = "LISTEN_MUSIC";

        /// <summary>
        ///     Action: Learn more.
        /// </summary>
        public const string LEARN_MORE = "LEARN_MORE";

        /// <summary>
        ///     Action: Sign up.
        /// </summary>
        public const string SIGN_UP = "SIGN_UP";

        /// <summary>
        ///     Action: Download.
        /// </summary>
        public const string DOWNLOAD = "DOWNLOAD";

        /// <summary>
        ///     Action: Watch more.
        /// </summary>
        public const string WATCH_MORE = "WATCH_MORE";

        /// <summary>
        ///     No button.
        /// </summary>
        public const string NO_BUTTON = "NO_BUTTON";

        /// <summary>
        ///     Action: Call now.
        /// </summary>
        public const string CALL_NOW = "CALL_NOW";

        /// <summary>
        ///     Action: Apply now.
        /// </summary>
        public const string APPLY_NOW = "APPLY_NOW";

        /// <summary>
        ///     Action: Buy now.
        /// </summary>
        public const string BUY_NOW = "BUY_NOW";

        /// <summary>
        ///     Action: Get offer.
        /// </summary>
        public const string GET_OFFER = "GET_OFFER";

        /// <summary>
        ///     Action: Get offer view.
        /// </summary>
        public const string GET_OFFER_VIEW = "GET_OFFER_VIEW";

        /// <summary>
        ///     Action: Get directions.
        /// </summary>
        public const string GET_DIRECTIONS = "GET_DIRECTIONS";

        /// <summary>
        ///     Action: Message page.
        /// </summary>
        public const string MESSAGE_PAGE = "MESSAGE_PAGE";

        /// <summary>
        ///     Action: Message user.
        /// </summary>
        public const string MESSAGE_USER = "MESSAGE_USER";

        /// <summary>
        ///     Action: Subscribe.
        /// </summary>
        public const string SUBSCRIBE = "SUBSCRIBE";

        /// <summary>
        ///     Action: Sell now.
        /// </summary>
        public const string SELL_NOW = "SELL_NOW";

        /// <summary>
        ///     Action: Donate now.
        /// </summary>
        public const string DONATE_NOW = "DONATE_NOW";

        /// <summary>
        ///     Action: Get quote.
        /// </summary>
        public const string GET_QUOTE = "GET_QUOTE";

        /// <summary>
        ///     Action: Contact us.
        /// </summary>
        public const string CONTACT_US = "CONTACT_US";

        /// <summary>
        ///     Action: Start order.
        /// </summary>
        public const string START_ORDER = "START_ORDER";

        /// <summary>
        ///     Action: Record now.
        /// </summary>
        public const string RECORD_NOW = "RECORD_NOW";

        /// <summary>
        ///     Action: Vote now.
        /// </summary>
        public const string VOTE_NOW = "VOTE_NOW";

        /// <summary>
        ///     Action: Register now.
        /// </summary>
        public const string REGISTER_NOW = "REGISTER_NOW";

        /// <summary>
        ///     Action: Request time.
        /// </summary>
        public const string REQUEST_TIME = "REQUEST_TIME";

        /// <summary>
        ///     Action: See menu.
        /// </summary>
        public const string SEE_MENU = "SEE_MENU";

        /// <summary>
        ///     Action: Email now.
        /// </summary>
        public const string EMAIL_NOW = "EMAIL_NOW";

        /// <summary>
        ///     Action: Get showtimes.
        /// </summary>
        public const string GET_SHOWTIMES = "GET_SHOWTIMES";

        /// <summary>
        ///     Action: Try it.
        /// </summary>
        public const string TRY_IT = "TRY_IT";

        /// <summary>
        ///     Action: Listen now.
        /// </summary>
        public const string LISTEN_NOW = "LISTEN_NOW";

        /// <summary>
        ///     Action: Open movies.
        /// </summary>
        public const string OPEN_MOVIES = "OPEN_MOVIES";
    }
}
