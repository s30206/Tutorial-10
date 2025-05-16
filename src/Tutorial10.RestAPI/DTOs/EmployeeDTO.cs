namespace Tutorial10.RestAPI.DTOs;

public class EmployeeDTO
{
    public EmployeeDTO(Employee employee)
    {
        Id = employee.Id;
        Name = employee.Name;
        JobId = employee.JobId;
        ManagerId = employee.ManagerId;
        HireDate = employee.HireDate;
        Salary = employee.Salary;
        Commission = employee.Commission;
        DepartmentId = employee.DepartmentId;
    }
    
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int JobId { get; set; }

    public int? ManagerId { get; set; }

    public DateTime HireDate { get; set; }

    public decimal Salary { get; set; }

    public decimal? Commission { get; set; }

    public int DepartmentId { get; set; }
}