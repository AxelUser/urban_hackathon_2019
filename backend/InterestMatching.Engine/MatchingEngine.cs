using System;
using System.Collections.Generic;
using System.Text;

namespace InterestMatching.Engine
{
    public static class MatchingEngine
    {
        public static List<ValueTuple<string, string>> GetPairs(Dictionary<string, User> matchingUsers)
        {
            var userIndexMap = FillUserIndexMap(matchingUsers);
            var matchingMap = FillMatchingMap(matchingUsers, userIndexMap);
            var pairs = new List<ValueTuple<string, string>>();
            while (matchingMap.GetLength(0) > 1)
            {
                var pair = GetPair(matchingMap);
                pairs.Add(new ValueTuple<string, string>(userIndexMap[pair.Item1], userIndexMap[pair.Item2]));
                DeleteFoundedPair(matchingMap, userIndexMap, pair);
            }

            return pairs;
        }

        public static ValueTuple<string, string> GetPair(User user, User[] matchingUsers)
        {
            var minimumDistance = new ValueTuple<string, double>(matchingUsers[0].Email, user.CalculateInterestsDistanse(matchingUsers[0].Interests));
            for (var i = 1; i < matchingUsers.Length; i++)
            {
                var interestsDistanse = user.CalculateInterestsDistanse(matchingUsers[i].Interests);
                if (interestsDistanse < minimumDistance.Item2)
                {
                    minimumDistance.Item1 = matchingUsers[i].Email;
                    minimumDistance.Item2 = interestsDistanse;
                }
            }

            return (user.Email, minimumDistance.Item1);
        }

        private static void DeleteFoundedPair(double[,] matchingMap, string[] userIndexMap, ValueTuple<int, int> pairIndexes)
        {
            var linesCount = matchingMap.GetLength(0);
            var columnCount = matchingMap.GetLength(1);
            var newMatchingMap = new double[linesCount - 2, columnCount - 2];
            var newUserIndexMap = new string[linesCount - 2];
            var startIndex = 0;
            var indexOffset = 0;
            CopyMatchingMap(matchingMap, newMatchingMap, startIndex, indexOffset, pairIndexes.Item1, pairIndexes.Item1);
            CopyUserIndexMap(userIndexMap, newUserIndexMap, startIndex, indexOffset, pairIndexes.Item1);
            startIndex = pairIndexes.Item1 + 1;
            indexOffset++;
            CopyMatchingMap(matchingMap, newMatchingMap, startIndex, indexOffset, pairIndexes.Item2, pairIndexes.Item2);
            CopyUserIndexMap(userIndexMap, newUserIndexMap, startIndex, indexOffset, pairIndexes.Item2);
            startIndex = pairIndexes.Item2 + 1;
            indexOffset++;
            CopyMatchingMap(matchingMap, newMatchingMap, startIndex, indexOffset, linesCount, columnCount);
            CopyUserIndexMap(userIndexMap, newUserIndexMap, startIndex, indexOffset, linesCount);

            RecalculateMinimum(newMatchingMap);
        }

        private static void CopyMatchingMap(double[,] matchingMap, double[,] newMatchingMap, int startIndex, int indexOffset, int lineLimit, int columnLimit)
        {
            for (var i = startIndex; i < lineLimit; i++)
            {
                for (var j = startIndex; j < columnLimit; j++)
                {
                    newMatchingMap[i - indexOffset, j - indexOffset] = matchingMap[i, j];
                }
            }
        }

        private static void CopyUserIndexMap(string[] userIndexMap, string[] newUserIndexMap, int startIndex, int indexOffset, int limit)
        {
            for (var i = startIndex; i < limit; i++)
            {
                newUserIndexMap[i - indexOffset] = userIndexMap[i];
            }
        }

        private static ValueTuple<int, int> GetPair(double[,] matchingMap)
        {
            var columnCount = matchingMap.GetLength(1);
            const int currentIndex = 0;
            var minimumIndex = (int)matchingMap[currentIndex, columnCount - 1];
            return FindPair(matchingMap, currentIndex, minimumIndex, columnCount);
        }

        private static ValueTuple<int, int> FindPair(double[,] matchingMap, int currentIndex, int minimumIndex, int columnCount)
        {
            if ((int) matchingMap[minimumIndex, columnCount - 1] == currentIndex)
            {
                if (currentIndex > minimumIndex)
                {
                    return (minimumIndex, currentIndex);
                }

                return (currentIndex, minimumIndex);
            }

            currentIndex = minimumIndex;
            minimumIndex = (int)matchingMap[currentIndex, columnCount - 1];

            return FindPair(matchingMap, currentIndex, minimumIndex, columnCount);
        }

        private static double[,] FillMatchingMap(Dictionary<string, User> matchingUsers, string[] userIndexMap)
        {
            var matchingMap = new double[matchingUsers.Count, matchingUsers.Count + 1];
            for (var i = 0; i < matchingUsers.Count - 1; i++)
            {
                var min = new ValueTuple<double, int>(double.MaxValue, 0);
                matchingMap[i, i] = double.MaxValue;
                for (var j = 0; j < i; j++)
                {
                    min = GetMinimum(matchingMap[i, j], j, min);
                }

                for (var j = i; j < matchingUsers.Count; j++)
                {
                    var interestsDistanse = matchingUsers[userIndexMap[i]]
                        .CalculateInterestsDistanse(matchingUsers[userIndexMap[j]].Interests);

                    matchingMap[i, j] = interestsDistanse;
                    matchingMap[j, i] = interestsDistanse;

                    min = GetMinimum(matchingMap[i, j], j, min);
                }

                matchingMap[i, matchingUsers.Count] = min.Item2;
            }

            return matchingMap;
        }

        private static void RecalculateMinimum(double[,] matchingMap)
        {
            var linesCount = matchingMap.GetLength(0);
            var columnCount = matchingMap.GetLength(1);
            for (var i = 0; i < linesCount; i++)
            {
                var min = new ValueTuple<double, int>(double.MaxValue, 0);
                for (var j = 0; j < columnCount; j++)
                {
                    min = GetMinimum(matchingMap[i, j], j, min);
                }

                matchingMap[i, columnCount - 1] = min.Item2;
            }
        }

        private static ValueTuple<double, int> GetMinimum(double distance, int index, ValueTuple<double, int> min)
        {
            if (distance < min.Item1)
            {
                min.Item1 = distance;
                min.Item2 = index;
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
