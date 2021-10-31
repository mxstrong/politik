using Politics.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Politics.Data
{
  public interface IStatementsRepository
  {
    Task<List<StatementOutDto>> GetAllStatements();
    Task<StatementOutDto> GetStatementById(string id);
    Task<StatementOutDto> AddStatement(StatementDto statementDto);
  }
}
