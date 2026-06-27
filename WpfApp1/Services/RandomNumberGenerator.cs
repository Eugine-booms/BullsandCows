using System;
using System.Collections.Generic;
using System.Linq;
using BullsAndCowsWPF.Services.Interfaces;

namespace BullsAndCowsWPF.Services
{
    public class RandomNumberGenerator : INumberGenerator
    {
        public string GenerateNumber(int length, int minDigit, int maxDigit, bool allowDuplicates)
        {
            if (length <= 0) throw new ArgumentException("Длина должна быть > 0", nameof(length));
            if (minDigit > maxDigit) throw new ArgumentException("Min > Max", nameof(minDigit));
            var range = Enumerable.Range(minDigit, maxDigit - minDigit + 1).ToList();
            if (!allowDuplicates && range.Count < length)
                throw new ArgumentException("Недостаточно уникальных цифр для заданной длины");

            var digits = new List<int>();
            for (int i = 0; i < length; i++)
            {
                int idx = Random.Shared.Next(range.Count);
                digits.Add(range[idx]);
                if (!allowDuplicates)
                    range.RemoveAt(idx);
            }
            return string.Join("", digits);
        }
    }
}
