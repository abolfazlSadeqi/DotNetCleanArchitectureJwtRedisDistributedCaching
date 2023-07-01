using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto;

public class EmployeeDto
{


    [Key]
    public int Id { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string Position { set; get; }


}
