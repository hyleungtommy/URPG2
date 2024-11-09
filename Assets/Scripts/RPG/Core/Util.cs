using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;
using System.Globalization;
namespace RPG
{
    public static class Util
    {
        public static void debug(string log)
        {
            Debug.Log(log);
        }

        public static string printStringArray(string[] array)
        {
            return String.Join("",
             new List<string>(array)
             .ConvertAll(i => i.ToString())
             .ToArray());
        }
        public static string printIntArray(int[] array)
        {
            return String.Join("",
             new List<int>(array)
             .ConvertAll(i => i.ToString())
             .ToArray());
        }


        public static int[] fromIntToStringArray(string[] array)
        {
            List<int> l = new List<int>();
            for (int i = 0; i < array.Length; i++)
            {
                l.Add(int.Parse(array[i]));
            }
            return l.ToArray();

        }

        public static int getRequireEXPForLevel(int level)
        {
            return Param.expRequire[level - 1];
        }

        public static int getFirstDigit(int i)
        {
            while (i >= 10)
                i /= 10;
            return i;
        }

        public static float getSumOfArray(int[] a)
        {
            float sum = 0.0f;
            for (int i = 0; i < a.Length; i++)
                sum += a[i];
            return sum;
        }

        public static int getRandomIndexFrom(int[] a)
        {
            int index = -1;
            float sum = getSumOfArray(a);
            float rnd = UnityEngine.Random.Range(0.0f, sum);
            for (int i = 0; i < a.Length; i++)
            {
                if (rnd < a[i])
                {
                    index = i;
                    break;
                }
                rnd -= a[i];
            }
            return index;
        }

        public static List<int> GetMultipleNumFromRandomList(List<int> originalList, int n)
        {
            System.Random rnd = new System.Random();
            return originalList.OrderBy(x => rnd.Next()).Take(n).ToList();
        }

        public static float calculateSum(int[] a)
        {
            float sum = 0.0f;
            for (int i = 0; i < a.Length; i++)
                sum += a[i];
            return sum;
        }

        public static int getRandomIndexFrom(int[] a, float sum)
        {
            int index = -1;
            float rnd = UnityEngine.Random.Range(0.0f, sum);
            for (int i = 0; i < a.Length; i++)
            {
                if (rnd < a[i])
                {
                    index = i;
                    break;
                }
                rnd -= a[i];
            }
            return index;
        }

        // public static int calculateCraftSkillEXPNeed(SkillCraft.Type skilltype, int level)
        // {
        //     if (skilltype == SkillCraft.Type.brewing)
        //     {
        //         return level * 100;
        //     }
        //     return level * 10;
        // }

        public static float ceilTo10(int n)
        {
            return (n / 10) * 10 + 10;
        }

        public static string ToRomanNum(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRomanNum(number - 1000);
            if (number >= 900) return "CM" + ToRomanNum(number - 900);
            if (number >= 500) return "D" + ToRomanNum(number - 500);
            if (number >= 400) return "CD" + ToRomanNum(number - 400);
            if (number >= 100) return "C" + ToRomanNum(number - 100);
            if (number >= 90) return "XC" + ToRomanNum(number - 90);
            if (number >= 50) return "L" + ToRomanNum(number - 50);
            if (number >= 40) return "XL" + ToRomanNum(number - 40);
            if (number >= 10) return "X" + ToRomanNum(number - 10);
            if (number >= 9) return "IX" + ToRomanNum(number - 9);
            if (number >= 5) return "V" + ToRomanNum(number - 5);
            if (number >= 4) return "IV" + ToRomanNum(number - 4);
            if (number >= 1) return "I" + ToRomanNum(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

        public static string getThreeDigitNumber(int num)
        {
            if (num < 10) return "00" + num;
            else if (num > 10 && num < 100) return "0" + num;
            else return num.ToString();
        }

        public static string printArray<T>(T[] array)
        {
            string s = "[";
            foreach (T item in array)
            {
                s += item.ToString();
            }
            return s + "]";
        }

        public static string printList<T>(List<T> array)
        {
            string s = "[";
            foreach (T item in array)
            {
                s += item.ToString();
            }
            return s + "]";
        }
        public static void Shuffle<T>(List<T> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // public static void RemoveCraftItem(List<Requirement> requirements, int requireMoney, int qty)
        // {
        //     if (!Param.noCraftRequirement)
        //     {
        //         foreach (Requirement requirement in requirements)
        //         {
        //             Item item = requirement.requireItem as Item;
        //             Game.inventory.smartDelete(item, requirement.requireQty * qty);
        //         }
        //         Game.money -= requireMoney;
        //     }
        // }

        // public static int CalculateElementalDamage(ElementalTemplate elementalDamage, ElementalTemplate elementalResistance, float atkPower)
        // {
        //     if (elementalDamage != null && elementalResistance != null)
        //     {
        //         int[] elementalDamageFlatten = FlattenElementalMatrix(elementalDamage);
        //         int[] elementalResistanceFlatten = FlattenElementalMatrix(elementalResistance);
        //         int elementAttackPower = 0;
        //         for (int i = 0; i < 7; i++)
        //         {
        //             if (elementalDamageFlatten[i] > 0)
        //             {
        //                 double elementAttackUnresisted = elementalDamageFlatten[i] * atkPower * 0.01;
        //                 double elementAttackResisted = elementAttackUnresisted * (100 - elementalResistanceFlatten[i]) * 0.01;
        //                 elementAttackPower += (int)elementAttackResisted;
        //                 //Debug.Log("elementAttackUnresisted" + elementAttackUnresisted + " elementAttackResisted=" + elementAttackResisted);
        //             }
        //         }
        //         //Debug.Log("atkPower=" + atkPower + " elementAttackPower=" + elementAttackPower);
        //         return elementAttackPower;
        //     }
        //     else
        //     {
        //         return 0;
        //     }
        // }

        // public static int[] FlattenElementalMatrix(ElementalTemplate elementalTemplate)
        // {
        //     int[] elementValues = new int[7];
        //     elementValues[0] = elementalTemplate.fire;
        //     elementValues[1] = elementalTemplate.ice;
        //     elementValues[2] = elementalTemplate.lighting;
        //     elementValues[3] = elementalTemplate.wind;
        //     elementValues[4] = elementalTemplate.earth;
        //     elementValues[5] = elementalTemplate.light;
        //     elementValues[6] = elementalTemplate.dark;
        //     return elementValues;
        // }

        public static string FormatTime(int taskTime)
        {
            TimeSpan time = TimeSpan.FromSeconds(taskTime);
            return time.ToString(@"hh\:mm\:ss");
        }

        public static DateTime GetDateTimeFromLoadSave(string saveStr)
        {
            return DateTime.ParseExact(saveStr, "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
        }

        public static string ToDateTimeSaveString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd-HH-mm-ss");
        }


    }
}