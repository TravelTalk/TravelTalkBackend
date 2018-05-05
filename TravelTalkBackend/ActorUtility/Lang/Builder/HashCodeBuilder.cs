namespace ActorUtilityTravelTalk.ActorUtility.Lang.Builder {
    using System;
    using System.Globalization;

    public sealed class HashCodeBuilder {

        private const int BASE_10 = 10;

        private readonly int constant;

        private int total;

        public HashCodeBuilder() : this(17, 37) { }

        /// <summary>
        ///     Contructs a new <see cref="HashCodeBuilder"/> instance, using a given <paramref name="initialValue"/> and a
        ///     <paramref name="multiplier"/>. Both numbers must be randomly chosen, non-zero and odd. Prime numbers are
        ///     preferred, especially for the <paramref name="multiplier"/>.
        /// </summary>
        /// <param name="initialValue">The initial value of the hash code to build. May not be zero or even.</param>
        /// <param name="multiplier">The multiplier used when appending properties. May not be zero of even.</param>
        public HashCodeBuilder(int initialValue, int multiplier) {
            total = initialValue;
            constant = multiplier;
        }

        /// <summary>
        ///     Gets the calculated hash code.
        /// </summary>
        public int HashCodeValue => total;

        /// <summary>
        ///     Appends a hash code for the given <see cref="object"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="object"/> value to add to the hash code.</param>
        /// <returns>This <see cref="ArgumentNullException"/> instance, which may be used for chaining.</returns>
        /// <exception cref="ArgumentNullException">If the <paramref name="value"/> parameter is <c>null</c>.</exception>
        public HashCodeBuilder Append(object value) {
            return value == null ? this : Append(value.GetHashCode());

        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="double"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="double"/> value to add to the hash code.</param>
        /// <param name="precisionDigits">
        ///     The number of decimal digits that must be zero in the absolute difference between
        ///     two <see cref="double"/> values in order to be considered as equal. This parameter should be used in accordance
        ///     with the precision value used when calling the <see cref="EqualsBuilder.Append(double,double,int)"/> method of the
        ///     <see cref="HashCodeBuilder"/> class for the same property. Must be non-negative. Defaults to 0.
        /// </param>
        /// <returns>This <see cref="EqualsBuilder"/> instance, which may be used for chaining.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="precisionDigits"/> argument is negative.</exception>
        public HashCodeBuilder Append(double value, int precisionDigits = 0) {
            return precisionDigits == 0
                    ? Append(BitConverter.DoubleToInt64Bits(value))
                    : Append((int) Math.Round(value * Math.Pow(BASE_10, precisionDigits - 1)));
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="decimal"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="decimal"/> value to add to the hash code.</param>
        /// <param name="precisionDigits">
        ///     The number of decimal digits that must be zero in the absolute difference between
        ///     two <see cref="decimal"/> values in order to be considered as equal. This parameter should be used in accordance
        ///     with the precision value used when calling the <see cref="EqualsBuilder.Append(decimal,decimal,int)"/> method of
        ///     the <see cref="EqualsBuilder"/> class for the same property. Must be non-negative. Defaults to 0.
        /// </param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="precisionDigits"/> argument is negative.</exception>
        public HashCodeBuilder Append(decimal value, int precisionDigits = 0) {
            return precisionDigits == 0
                    ? Append(value.GetHashCode())
                    : Append((int) ((double) value * Math.Pow(BASE_10, precisionDigits - 1)));
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="float"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="float"/> value to add to the hash code.</param>
        /// <param name="precisionDigits">
        ///     The number of decimal digits that must be zero in the absolute difference between
        ///     two <see cref="float"/> values in order to be considered as equal. This parameter should be used in accordance
        ///     with the precision value used when calling the <see cref="EqualsBuilder.Append(float,float,int)"/> method of the
        ///     <see cref="EqualsBuilder"/> class for the same property. Must be non-negative. Defaults to 0.
        /// </param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="precisionDigits"/> argument is negative.</exception>
        public HashCodeBuilder Append(float value, int precisionDigits = 0) {
            return Append((double) value, precisionDigits);
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="long"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="long"/> value to add to the hash code.</param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        public HashCodeBuilder Append(long value) {
            total = total * constant + (int) (value ^ (value >> 32));

            return this;
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="ulong"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="ulong"/> value to add to the hash code.</param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        public HashCodeBuilder Append(ulong value) {
            return Append(Convert.ToInt64(value));
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="int"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="int"/> value to add to the hash code.</param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        public HashCodeBuilder Append(int value) {
            total = total * constant + value;

            return this;
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="uint"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="uint"/> value to add to the hash code.</param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        public HashCodeBuilder Append(uint value) {
            return Append((ulong) value);
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="short"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="short"/> value to add to the hash code.</param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        public HashCodeBuilder Append(short value) {
            return Append((int) value);
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="ushort"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="ushort"/> value to add to the hash code.</param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        public HashCodeBuilder Append(ushort value) {
            return Append((uint) value);
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="char"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="char"/> value to add to the hash code.</param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        public HashCodeBuilder Append(char value) {
            return Append((short) value);
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="byte"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="byte"/> value to add to the hash code.</param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        public HashCodeBuilder Append(byte value) {
            return Append((short) value);
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="bool"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value to add to the hash code.</param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        public HashCodeBuilder Append(bool value) {
            total = total * constant + (value ? 0 : 1);

            return this;
        }

        /// <summary>
        ///     Appends a hash code for the given <see cref="string"/>&#160;<paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to add to the hash code.</param>
        /// <param name="stringComparison">
        ///     The string comparison rule used to transform the given string <paramref name="value"/>
        ///     before appinding its hash code.
        /// </param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        public HashCodeBuilder Append(string value, StringComparison stringComparison) {
            if (value == null) {
                return this;
            }

            string valueToAppend;

            switch (stringComparison) {
                case StringComparison.InvariantCulture:
                    valueToAppend = value.ToString(CultureInfo.InvariantCulture);
                    break;
                case StringComparison.InvariantCultureIgnoreCase:
                    valueToAppend = value.ToString(CultureInfo.InvariantCulture).ToUpper();
                    break;
                case StringComparison.CurrentCulture:
                    valueToAppend = value.ToString(CultureInfo.CurrentCulture);
                    break;
                case StringComparison.CurrentCultureIgnoreCase:
                    valueToAppend = value.ToString(CultureInfo.CurrentCulture).ToUpper();
                    break;
                case StringComparison.OrdinalIgnoreCase:
                    valueToAppend = value.ToUpper();
                    break;
                case StringComparison.Ordinal:
                    valueToAppend = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stringComparison), stringComparison, null);
            }

            return Append(valueToAppend.GetHashCode());
        }

        /// <summary>
        ///     Adds the result of <c>base.GetHashCode(object)</c> to this builder.
        /// </summary>
        /// <param name="baseHashCode">The result when calling <c>base.Equals(object)</c>.</param>
        /// <returns>This <see cref="HashCodeBuilder"/> instance, which may be used for chaining.</returns>
        public HashCodeBuilder AppendBase(long baseHashCode) {
            return Append(baseHashCode);
        }
    }
}
