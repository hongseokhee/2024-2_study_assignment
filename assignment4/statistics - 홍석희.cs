using System;
using System.Linq;


namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };
            // You can convert string to double by
            // double.Parse(str)

            int stdCount = data.GetLength(0) - 1;
            // ---------- TODO ----------
            int subject_num = 3;
            int student_num = 5;
            double[] subject_average = {0,0,0}; // 과목 개수에 따라 크기 조절해야됨
            double[] max_min_arr = {0,0,0,0,0,0}; // 과목 개수에 따라 크기 조절해야됨
            // 평균, 최댓값 & 최솟값
            for (int i = 0; i < subject_num; i ++) {
                double max = -1;
                double min = 101;
                for (int j = 0; j < student_num; j++) {
                    subject_average[i] += double.Parse(data[j+1, i+2]);
                    if (double.Parse(data[j+1, i+2])<min) min = double.Parse(data[j+1, i+2]);
                    if (double.Parse(data[j+1, i+2])>max) max = double.Parse(data[j+1, i+2]);
                }
                max_min_arr[2*i] = max;
                max_min_arr[2*i +1] = min;
            }
            // 학생 total 순위
            double[] student_total = {0,0,0,0,0}; // 학생 수에 따라 크기 조절해야됨
            for (int i = 0; i < student_num; i++) {
                for (int j = 0; j < subject_num; j++) {
                    student_total[i] += double.Parse(data[i+1, j+2]);
                }
            }
            var indexedValues = student_total.Select((value, index) => new {Value = value, Index = index }).ToList();
            var rankedValues = indexedValues
                .OrderByDescending(x => x.Value)
                .Select((item, rank) => new { Value = item.Value, OriginalIndex = item.Index, Rank = rank + 1 })
                .ToList();

            // 평균 출력
            Console.WriteLine("Average Scores:");
            for (int i = 0; i < 3; i++) {
                Console.WriteLine($"{data[0, i+2]}: {subject_average[i]/student_num}");
            }
            // 최댓값 & 최솟값 출력
            Console.WriteLine("Max and min Scores:");
            for (int i = 0; i < 3; i++) {
                Console.WriteLine($"{data[0, i+2]}: ({max_min_arr[2*i]}, {max_min_arr[2*i +1]})");
            }

            // 순위 결과 출력
            Console.WriteLine("Students rank by total scores:");
            int order = 1;
            foreach (var item in rankedValues.OrderBy(x => x.OriginalIndex))
            {
                Console.WriteLine($"{data[order++, 1]}: {item.Rank}th");
            }
            // --------------------
        }
    }
}

/* example output

Average Scores: 
Math: 84.40
Science: 86.80
English: 86.20

Max and min Scores: 
Math: (94, 72)
Science: (95, 76)
English: (92, 78)

Students rank by total scores:
Alice: 4th
Bob: 1st
Charlie: 5th
David: 2nd
Eve: 3rd

*/