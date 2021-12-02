using Politics.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Politics.Data
{
  public interface IReportsRepository
  {
    Task<List<ReportOutDto>> GetReports(int? pageNumber, int? pageSize, bool? isReviewed);
    Task<ReportOutDto> GenerateReport(ReportDto reportDto, string userId);
    Task<bool> ReviewReport(string reportId, string userId);
  }
}
