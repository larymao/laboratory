using System;

namespace Lary.Laboratory.Core.Math
{
    /// <summary>
    /// Specifies the digits of a decimal data.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class FractionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FractionAttribute"/> class.
        /// </summary>
        /// <param name="digits"></param>
        public FractionAttribute(int digits = 2)
        {
            Digits = digits;
        }

        /// <summary>
        /// The digits of current decimal data.
        /// </summary>
        public int Digits { get; set; }
    }
}
