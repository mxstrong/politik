using Politics.Dtos;
using Politics.Helpers;
using Politics.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Politics.Data
{
  public interface IStatementsRepository
  {
    Task<PaginatedList<StatementOutDto>> GetAllStatements(string? politicianId, List<string>? tagIds, int? pageNumber, int? pageSize);
    Task<StatementOutDto> GetStatementById(string id);
    Task<StatementOutDto> AddStatement(StatementDto statementDto, string userId);
    Task<StatementOutDto> DeleteStatementById(string id);
    Task<Statement> GetStatementEntityById(string id);
    Task<bool> LikeStatement(string statementId, string userId);
    Task<bool> UnlikeStatement(string statementId, string userId);
    Task<bool> CheckIfUserHasLiked(string statementId, string userId);
    int GetLikeCount(string statementId);
  }
}
