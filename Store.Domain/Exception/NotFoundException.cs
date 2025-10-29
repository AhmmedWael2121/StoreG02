using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Exception;

public class NotFoundException(int id) : ApplicationException($"The resource with id {id} was not found.")
{
}
