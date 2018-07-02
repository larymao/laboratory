using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Attributes
{
    /// <summary>
    ///     Indicates fields supported by facebook server.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    internal class FacebookPropertyAttribute : Attribute
    {
        private string _name;

        /// <summary>
        ///     The name of facebook property.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FacebookPropertyAttribute"/> class.
        /// </summary>
        /// <param name="name">
        ///     The name of facebook property.
        /// </param>
        public FacebookPropertyAttribute(string name)
        {
            this._name = name;
        }
    }
}
