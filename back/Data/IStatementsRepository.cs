using Politics.Dtos;
using Politics.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Politics.Data
{
  public interface IStatementsRepository
  {
    Task<PaginatedList<StatementOutDto>> GetAllStatements(string? politicianId, List<string>? tagIds, int? pageNumber, int? pageSize);
    Task<StatementOutDto> GetStatementById(string id);
    Task<StatementOutDto> AddStatement(StatementDto statementDto);
    Task<StatementOutDto> DeleteStatementById(string id);
  }
}
