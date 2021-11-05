using Politics.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Politics.Data
{
  public interface IStatementsRepository
  {
    Task<List<StatementOutDto>> GetAllStatements(string? politicianId, List<string>? tagIds);
    Task<StatementOutDto> GetStatementById(string id);
    Task<StatementOutDto> AddStatement(StatementDto statementDto);
    Task<StatementOutDto> DeleteStatementById(string id);
  }
}
