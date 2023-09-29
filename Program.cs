using Newtonsoft.Json;
using Simplex_Method;

try
{
    string json = File.ReadAllText("Storage.json");
    LinearProgrammingProblem problem = JsonConvert.DeserializeObject<LinearProgrammingProblem>(json);

    var solver = new SimplexSolver();
    var result = solver.Solve(problem);

    if (result.Status == SimplexSolverStatus.Optimal)
    {
        Console.WriteLine("Оптимальное решение найдено:");
        Console.WriteLine("Значение целевой функции: " + result.OptimalValue);
        Console.WriteLine("Оптимальные значения переменных:");
        for (int i = 0; i < result.OptimalSolution.Length; i++)
        {
            Console.WriteLine("X" + (i + 1) + ": " + result.OptimalSolution[i]);
        }
    }
    else if (result.Status == SimplexSolverStatus.Infeasible)
    {
        Console.WriteLine("Задача не имеет допустимых решений.");
    }
    else if (result.Status == SimplexSolverStatus.Unbounded)
    {
        Console.WriteLine("Задача имеет бесконечное количество решений.");
    }
    else
    {
        Console.WriteLine("Не удалось найти оптимальное решение.");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Произошла ошибка: " + ex.Message);
}
    