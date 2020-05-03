using System;

namespace Interview.Puzzles
{
    // https://www.hackerearth.com/practice/notes/bit-manipulation/
    public class FlippingBit
    {
        /*
         Flipping bit flipps all bit to the right. for example in 
         001010 flipping 1 to 0 at index 2 will result in 000101
         000110 flipping 1 to 0 at index 3 will result in 000001
         Count how many flip required to turn all bits of given number to 0.
         for ex.

            N = 001010 
                000101      flip 3rd bit, flips rest bit to the right 
                000010      flip 4th bit, flips rest bit to the right
                000001      flip 5th bit, flips rest bit to the right
                000000      flip last bit

          Hence answer is 4 flip required to turn all bits to 0.       
        */
        public static int CountFlip(int n)
        {
            //Console.WriteLine(Convert.ToString(n, toBase: 2));
            int count = 0;
            while (n != 0)
            {
                n = FlipBitsAtIndex(n, IndexOfFirstSetBit(n));
                //Console.WriteLine(Convert.ToString(n, toBase: 2));
                count++;
            }
            return count;
        }

        public static int IndexOfFirstSetBit(int n)
        {
            int count = 0;
            while (n > 0)
            {
                n >>= 1; // Keep right shifting untill it becomes 0 (n = n>>1)
                count++;
            }
            return count;
        }

        /// <summary>
        /// Brian Kernighan’s Algorithm:
        /// Subtracting 1 from a decimal number flips all the bits after the rightmost set bit(which is 1) including the rightmost set bit.
        /// for example :
        /// 10 in binary is 00001010
        /// 9 in binary is 00001001
        /// 8 in binary is 00001000
        /// 7 in binary is 00000111
        /// So if we subtract a number by 1 and do bitwise & with itself (n & (n-1)), we unset the rightmost set bit. If we do n & (n-1) in a loop and count the no of times loop executes we get the set bit count.
        /// The beauty of this solution is the number of times it loops is equal to the number of set bits in a given integer.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int CountSetBits(int n)
        {
            int count = 0;
            while (n > 0)
            {
                n &= n - 1;
                count++;
            }
            return count;
        }

        /// <summary>
        /// Flipping bit flipps all bit to the right. for example 
        /// 001010 flipping 1 to 0 at index 2 will result in 000101
        /// 000110 flipping 1 to 0 at index 3 will result in 000001
        /// </summary>
        /// <param name="n"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int FlipBitsAtIndex(int n, int index)
        {
            int mask = (1 << index) - 1;
            return n ^ mask;
        }

        // Count the number of ones in the binary representation of the given number.
        public static int CountOnes(uint n)
        {
            Console.WriteLine(Convert.ToString(n, toBase: 2));
            int count = 0;
            while (n != 0)
            {
                n = n & n - 1;
                count++;

                Console.WriteLine(Convert.ToString(n, toBase: 2));
            }
            return count + 1;
        }

        public static bool IsIthBitSet(int number, int ith)
        {
            int result = number & (int)Math.Pow(2, ith);
            return result != 0;
        }

        /// <summary>
        /// if a number n is a power of 2 then bitwise & of n and n-1 will be zero
        /// </summary>
        public static bool IsNumberPowerOfTwo(int number)
        {
            return (number & number - 1) == 0;
        }
    }
}
