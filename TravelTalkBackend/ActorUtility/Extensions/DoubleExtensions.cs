namespace ActorUtilityTravelTalk.ActorUtility.Extensions {
    using System;

    /// <summary>
    ///     Class providing extension methods for <see cref="double"/> values.
    /// </summary>
    public static class DoubleExtensions {

        private const int BASE_10 = 10;

        /// <summary>
        ///     Gets whether the <paramref name="first"/>&#160;<see cref="double"/> value equals the <paramref name="second"/>
        ///     &#160;<see cref="double"/> value. The two values are being considered as equal if the first 3 digits of their
        ///     difference are all zero.
        /// </summary>
        /// <param name="first">The first <see cref="double"/> value to compare.</param>
        /// <param name="second">The second <see cref="double"/> value to compare.</param>
        /// <returns>
        ///     <c>true</c> if the <paramref name="first"/> and <paramref name="second"/> values are equal (with respect
        ///     to 3 decimal places), <c>false</c> otherwise.
        /// </returns>
        public static bool Equals3DigitsPrecision(this double first, double second) {
            return EqualsNDigitsPrecision(first, second, 3);
        }

        /// <summary>
        ///     Gets whether the <paramref name="first"/>&#160;<see cref="double"/> value equals the <paramref name="second"/>
        ///     &#160;<see cref="double"/> value. The two values are being considered as equal if the first 6 digits of their
        ///     difference are all zero.
        /// </summary>
        /// <param name="first">The first <see cref="double"/> value to compare.</param>
        /// <param name="second">The second <see cref="double"/> value to compare.</param>
        /// <returns>
        ///     <c>true</c> if the <paramref name="first"/> and <paramref name="second"/> values are equal (with respect
        ///     to 6 decimal places), <c>false</c> otherwise.
        /// </returns>
        public static bool Equals6DigitsPrecision(this double first, double second) {
            return EqualsNDigitsPrecision(first, second, 6);
        }

        /// <summary>
        ///     Gets whether the <paramref name="first"/>&#160;<see cref="double"/> value equals the <paramref name="second"/>
        ///     &#160;<see cref="double"/> value. The two values are being considered as equal if the first 9 digits of their
        ///     difference are all zero.
        /// </summary>
        /// <param name="first">The first <see cref="double"/> value to compare.</param>
        /// <param name="second">The second <see cref="double"/> value to compare.</param>
        /// <returns>
        ///     <c>true</c> if the <paramref name="first"/> and <paramref name="second"/> values are equal (with respect
        ///     to 9 decimal places), <c>false</c> otherwise.
        /// </returns>
        public static bool Equals9DigitsPrecision(this double first, double second) {
            return EqualsNDigitsPrecision(first, second, 9);
        }

        /// <summary>
        ///     Gets whether the <paramref name="first"/>&#160;<see cref="double"/> value equals the <paramref name="second"/>
        ///     &#160;<see cref="double"/> value. The two values are being considered as equal if the first n digits of their
        ///     difference are all zero.
        /// </summary>
        /// <param name="first">The first <see cref="double"/> value to compare.</param>
        /// <param name="second">The second <see cref="double"/> value to compare.</param>
        /// <param name="precisionDigits">
        ///     The number of decimal digits that must be zero in the absolute difference |<paramref name="first"/> - <paramref name="second"/>|
        ///     in order to be considered as equal. If <paramref name="precisionDigits"/> is zero, the two values must match exactly to be treated
        ///     as equal. Must be non-negative. Defaults to 10.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the <paramref name="first"/> and <paramref name="second"/> values are equal (with respect
        ///     to the given <paramref name="precisionDigits"/> argument), <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="precisionDigits"/> argument is negative.</exception>
        public static bool EqualsNDigitsPrecision(this double first, double second, int precisionDigits = 10) {
            if (precisionDigits == 0) {
                return first.CompareTo(second) == 0;
            }

            return Math.Abs(first - second) < 1d / Math.Pow(BASE_10, precisionDigits);
        }
    }

}
