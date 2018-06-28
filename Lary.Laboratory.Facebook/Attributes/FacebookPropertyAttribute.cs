using System;
using System.Collections.Generic;
using System.Text;

namespace Lary.Laboratory.Facebook.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class FacebookPropertyAttribute : Attribute
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public FacebookPropertyAttribute(string name)
        {
            this._name = name;
        }
    }
}
