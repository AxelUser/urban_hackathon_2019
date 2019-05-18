using System;
using System.Collections.Generic;
using System.Text;

namespace InterestMatching.Engine
{
    public static class MatchingEngine
    {
        public static Dictionary<uint, uint> GetPairs(Dictionary<string, User> matchingUsers)
        {
            var userIndexMap = FillUserIndexMap(matchingUsers);
            var matchingMap = FillMatchingMap(matchingUsers, userIndexMap);

            return null;
        }

        private static double[,] FillMatchingMap(Dictionary<string, User> matchingUsers, string[] userIndexMap)
        {
            var matchingMap = new double[matchingUsers.Count, matchingUsers.Count + 1];
            for (var i = 0; i < matchingUsers.Count - 1; i++)
            {
                var min = double.MaxValue;
                var matchingDistance = 0.0;
                matchingMap[i, i] = double.MaxValue;
                for (var j = 0; j < i; j++)
                {
                    matchingDistance = matchingMap[i, j];
                    min = GetMinimum(matchingDistance, min);
                }

                for (var j = i; j < matchingUsers.Count; j++)
                {
                    matchingDistance = matchingUsers[userIndexMap[i]]
                        .CalculateInterestsDistanse(matchingUsers[userIndexMap[j]].Interests);

                    matchingMap[i, j] = matchingDistance;
                    min = GetMinimum(matchingDistance, min);
                }

                matchingMap[i, matchingUsers.Count] = min;
            }

            return matchingMap;
        }

        private static void RecalculateMinimum(double[,] matchingMap)
        {
            var linesCount = matchingMap.GetLength(0);
            var columnCount = matchingMap.GetLength(1);
            for (var i = 0; i < linesCount; i++)
            {
                var min = 0.0;
                for (var j = 0; j < columnCount; j++)
                {
                    min = GetMinimum(matchingMap[i, j], min);
                }

                matchingMap[i, columnCount - 1] = min;
            }
        }

        private static double GetMinimum(double matchingDistance, double min)
        {
            if (matchingDistance < min)
            {
                min = matchingDistance;
            }
            return min;
        }

        private static string[] FillUserIndexMap(Dictionary<string, User> matchingUsers)
        {
            var userIndexMap = new string[matchingUsers.Count];
            var index = 0;
            foreach (var matchingUser in matchingUsers)
            {
                userIndexMap[index] = matchingUser.Value.Email;
                index++;
            }

            return userIndexMap;
        }
    }
}
