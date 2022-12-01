using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TreeEditor;
using UnityEngine;

namespace Santa.Workshop
{
    /// <summary>
    /// Implementation of the "Day 1" challenge
    /// </summary>
    public class Day1CalorieCounting : EventSolution, IEventSolution
    {
        [Header("Input file")]
        public string inputFilePath;

        /// <summary>
        /// Implement the RetrieveSolution method
        /// </summary>
        /// <returns></returns>
        public override string RetrieveSolution()
        {
            List<Elf> elves = new List<Elf>();
            ParseElfInput(elves);
            return GetResult(elves);
        }

        /// <summary>
        /// Solve the GetMostCalories problem
        /// </summary>
        /// <returns></returns>
        private void ParseElfInput(List<Elf> elves)
        {
            if (!File.Exists(inputFilePath))
            {
                Debug.LogError($"Input file doesn't exist! {inputFilePath}! Bah, humbug!");
            }
            string[] inputDataArray = File.ReadAllLines(inputFilePath);

            // Start with our first elf
            int elfCount = 0;
            Elf currentElf = new Elf(elfCount);
            elves.Add(currentElf);

            foreach (string inputLine in inputDataArray)
            {
                // Check if we're on a new elf
                if(inputLine.Equals(string.Empty))
                {
                    // Create and add a new elf instance
                    elfCount++;
                    currentElf = new Elf(elfCount);
                    elves.Add(currentElf);
                }
                else
                {
                    // Tally up the calories
                    int calories = Int32.Parse(inputLine);
                    currentElf.AddCalories(calories);
                }
            }
        }

        private string GetResult(List<Elf> elves)
        {
            return $"{FindMostCalorieElf(elves)}\n{FindTopCalorieElves(elves, 3)}";
        }

        /// <summary>
        /// Return details of elf with most calories
        /// </summary>
        /// <param name="elves"></param>
        /// <returns></returns>
        private string FindMostCalorieElf(List<Elf> elves)
        {
            elves.Sort();
            return $"Elf number: {elves.First().ElfNumber} with: {elves.First().GetTotalCalories()} calories" ;
        }

        private string FindTopCalorieElves(List<Elf> elves, int numberOfElves)
        {
            elves.Sort();
            int totalCalories = 0;
            int currentElf = 1;
            foreach (Elf elf in elves)
            {
                Debug.Log($"Elf {currentElf} has {elf.GetTotalCalories()}");
                totalCalories += elf.GetTotalCalories();
                if(currentElf == numberOfElves)
                {
                    break;
                }
                currentElf++;
            }
            return $"Top {numberOfElves} have {totalCalories} calories";
        }

        private class Elf : IComparable
        {
            public int ElfNumber;
            private int totalCalories;

            public Elf(int elfNumber)
            {
                ElfNumber = elfNumber;
                totalCalories = 0;
            }

            public void AddCalories(int caloriesToAdd)
            {
                totalCalories += caloriesToAdd;
            }

            public int GetTotalCalories()
            {
                return totalCalories;
            }

            /// <summary>
            /// Sort implementation for our elves list
            /// </summary>
            /// <param name="objToCompare"></param>
            /// <returns></returns>
            public int CompareTo(object objToCompare)
            {
                Elf elfToCompare = (Elf)objToCompare;
                if (this.totalCalories < elfToCompare.totalCalories)
                    return 1;
                if (this.totalCalories > elfToCompare.totalCalories)
                    return -1;
                else
                    return 0;
            }
        }
    }
}