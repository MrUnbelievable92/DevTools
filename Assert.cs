#if DEBUG

#define CONDITION_CHECKS
#define NULL_CHECKS
#define FILE_PATH_CHECKS
#define BOUNDS_CHECKS
#define SUBARRAY_CHECKS
#define COMPARE_CHECKS
#define ARITHMETIC_LOGIC_CHECKS

#endif

using System;
using System.IO;
using System.Runtime.CompilerServices;

// CONDITIONAL ATTRIBUTE DOESN'T WORK AS EXPECTED WITH UNITY
// strings cannot be passed as arguments if the functions are to work with Unity.Burst
namespace DevTools
{
    unsafe public static class Assert
    {
        #region CONDITION_CHECKS
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsTrue(bool condition)
        {
#if CONDITION_CHECKS
            if (!condition)
            {
                throw new Exception("Expected 'true'.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsFalse(bool condition)
        {
#if CONDITION_CHECKS
            if (condition)
            {
                throw new Exception("Expected 'false'.");
            }
#endif
        }
        #endregion


        #region NULL_CHECKS
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNull(object obj)
        {
#if NULL_CHECKS
            if (obj != null)
            {
                throw new InvalidDataException("Expected null."); ;
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNull(void* ptr)
        {
#if NULL_CHECKS
            if (ptr != null)
            {
                throw new InvalidDataException("Expected null."); ;
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotNull(object obj)
        {
#if NULL_CHECKS
            if (obj == null)
            {
                throw new NullReferenceException("Expected not-null."); ;
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotNull(void* ptr)
        {
#if NULL_CHECKS
            if (ptr == null)
            {
                throw new NullReferenceException("Expected not-null."); ;
            }
#endif
        }
        #endregion


        #region FILE_PATH_CHECKS
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FileExists(string path) 
        {
#if FILE_PATH_CHECKS
            IsNotNull(path); // File.Exists only returns 'false' in case 'path' is null (no explicit throw, which is what I want)

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
#endif
        }
        #endregion


        #region BOUNDS_CHECKS
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsWithinArrayBounds(int index, int arrayLength)
        {
#if BOUNDS_CHECKS
            IsNonNegative(arrayLength);

            if ((uint)index >= (uint)arrayLength)
            {
                throw new IndexOutOfRangeException($"{ index } is out of range (length { arrayLength } - 1).");
            }
#endif
        }
        #endregion


        #region SUBARRAY_CHECKS
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsValidSubarray(int index, int NumEntries, int arrayLength)
        {
#if SUBARRAY_CHECKS
            IsWithinArrayBounds(index, arrayLength);
            IsNonNegative(NumEntries);

            if (index + NumEntries > arrayLength)
            {
                throw new IndexOutOfRangeException($"{ nameof(index) } + { nameof(NumEntries) } is { index + NumEntries }, which is larger than length { arrayLength }.");
            }
#endif
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SubarraysDoNotOverlap(int firstIndex, int secondIndex, int firstNumEntries, int secondNumEntries)
        {
#if SUBARRAY_CHECKS
            if (firstIndex < secondIndex)
            {
                if (firstIndex + firstNumEntries > secondIndex)
                {
                    throw new IndexOutOfRangeException($"Subarray from { firstIndex } to { firstIndex + firstNumEntries - 1} overlaps with subarray from { secondIndex } to { secondIndex + secondNumEntries - 1 }.");
                }
            }
            else
            {
                if (secondIndex + secondNumEntries > firstIndex)
                {
                    throw new IndexOutOfRangeException($"Subarray from { secondIndex } to { secondIndex + secondNumEntries - 1} overlaps with subarray from { firstIndex } to { firstIndex + firstNumEntries - 1 }.");
                }
            } 
#endif
        }
        #endregion


        #region COMPARE_CHECKS
        /// <summary>       Remember: Zero is neither positive nor negative.       </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(long value)
        {
#if COMPARE_CHECKS
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be positive.");
            }
#endif
        }

        /// <summary>       Remember: Zero is neither positive nor negative.       </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(float value)
        {
#if COMPARE_CHECKS
            if (value <= 0f)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be positive.");
            }
#endif
        }

        /// <summary>       Remember: Zero is neither positive nor negative.       </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(double value)
        {
#if COMPARE_CHECKS
            if (value <= 0d)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be positive.");
            }
#endif
        }

        /// <summary>       Remember: Zero is neither positive nor negative.       </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsPositive(decimal value)
        {
#if COMPARE_CHECKS
            if (value <= 0m)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be positive.");
            }
#endif
        }

        /// <summary>       Remember: Zero is neither positive nor negative.       </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(long value)
        {
#if COMPARE_CHECKS
            if (value >= 0)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be negative.");
            }
#endif
        }

        /// <summary>       Remember: Zero is neither positive nor negative.       </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(float value)
        {
#if COMPARE_CHECKS
            if (value >= 0f)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be negative.");
            }
#endif
        }

        /// <summary>       Remember: Zero is neither positive nor negative.       </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(double value)
        {
#if COMPARE_CHECKS
            if (value >= 0d)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be negative.");
            }
#endif
        }

        /// <summary>       Remember: Zero is neither positive nor negative.       </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNegative(decimal value)
        {
#if COMPARE_CHECKS
            if (value >= 0m)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be negative.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNonNegative(long value)
        {
#if COMPARE_CHECKS
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be positive or equal to zero.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNonNegative(float value)
        {
#if COMPARE_CHECKS
            if (value < 0f)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be positive or equal to zero.");
            }
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNonNegative(double value)
        {
#if COMPARE_CHECKS
            if (value < 0d)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be positive or equal to zero.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNonNegative(decimal value)
        {
#if COMPARE_CHECKS
            if (value < 0m)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be positive or equal to zero.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotPositive(long value)
        {
#if COMPARE_CHECKS
            if (value > 0)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be negative or equal to zero.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotPositive(float value)
        {
#if COMPARE_CHECKS
            if (value > 0f)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be negative or equal to zero.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotPositive(double value)
        {
#if COMPARE_CHECKS
            if (value > 0d)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be negative or equal to zero.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotPositive(decimal value)
        {
#if COMPARE_CHECKS
            if (value > 0m)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be negative or equal to zero.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AreEqual<T>(T a, T b)
            where T : IEquatable<T>
        {
#if COMPARE_CHECKS
            if (!a.Equals(b))
            {
                throw new ArgumentOutOfRangeException($"{ a } was expected to be equal to { b }.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AreNotEqual<T>(T a, T b)
            where T : IEquatable<T>
        {
#if COMPARE_CHECKS
            if (a.Equals(b))
            {
                throw new ArgumentOutOfRangeException($"{ a } was expected not to be equal to { b }.");
            }
#endif
        }

        /// <summary>    Inclusive    </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsBetween<T>(T value, T min, T max)
            where T : IComparable<T>
        {
#if COMPARE_CHECKS
            if ((value.CompareTo(min) < 0) || (value.CompareTo(max) > 0))
            {
                throw new ArgumentOutOfRangeException($"Min: { min }, Max: { max }, Value: { value }.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsSmallerOrEqual<T>(T value, T limit)
            where T : IComparable<T>
        {
#if COMPARE_CHECKS
            if (value.CompareTo(limit) == 1)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be smaller than or equal to { limit }.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsSmaller<T>(T value, T limit)
            where T : IComparable<T>
        {
#if COMPARE_CHECKS
            if (value.CompareTo(limit) != -1)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be smaller than { limit }.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsGreaterOrEqual<T>(T value, T limit)
            where T : IComparable<T>
        {
#if COMPARE_CHECKS
            if (value.CompareTo(limit) == -1)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be greater than or equal to { limit }.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsGreater<T>(T value, T limit)
            where T : IComparable<T>
        {
#if COMPARE_CHECKS
            if (value.CompareTo(limit) != 1)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected to be greater than { limit }.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotSmaller<T>(T value, T limit)
            where T : IComparable<T>
        {
#if COMPARE_CHECKS
            if (value.CompareTo(limit) == -1)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected not to be smaller than { limit }.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNotGreater<T>(T value, T limit)
            where T : IComparable<T>
        {
#if COMPARE_CHECKS
            if (value.CompareTo(limit) == 1)
            {
                throw new ArgumentOutOfRangeException($"{ value } was expected not to be greater than { limit }.");
            }
#endif
        }
        #endregion


        #region ARITHMETIC_LOGIC_CHECKS
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsSafeBoolean(bool x)
        {
#if COMPARE_CHECKS
            if (*(byte*)&x > 1)
            {
                throw new InvalidDataException($"The numerical value of the bool { nameof(x) } is { *(byte*)&x } which can lead to undefined behavior.");
            }
#endif
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsDefinedBitShift<T>(int amount)
            where T : unmanaged
        {
#if ARITHMETIC_LOGIC_CHECKS
            if ((uint)amount >= (uint)sizeof(T) * 8u)
            {
                throw new ArgumentOutOfRangeException($"Shifting a { typeof(T) } by { amount } results in undefined behavior.");
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsDefinedBitShift<T>(uint amount)
            where T : unmanaged
        {
            IsDefinedBitShift<T>((int)amount);
        }
        #endregion
    }
}
