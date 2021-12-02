using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Politics.Dtos;
using Politics.Helpers;
using Politics.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Politics.Data
{
  public class ReportsRepository : IReportsRepository
  {
    private readonly PoliticsDbContext _context;
    private readonly IMapper _mapper;

    public ReportsRepository(PoliticsDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<List<ReportOutDto>> GetReports(int? pageNumber, int? pageSize, bool? isReviewed)
    {
      var reports = _context.Reports;
      var paginatedReports = await PaginatedList<Report>.CreateAsync(reports, pageNumber ?? 1, pageSize ?? 10);
      return _mapper.Map<PaginatedList<Report>, PaginatedList<ReportOutDto>>(paginatedReports);
    }

    public async Task<ReportOutDto> GenerateReport(ReportDto reportDto, string userId)
    {
      var report = _mapper.Map<ReportDto, Report>(reportDto);
      report.ReportId = Guid.NewGuid().ToString();
      report.IsReviewed = false;
      report.CreatedById = userId;
      await _context.Reports.AddAsync(report);
      await _context.SaveChangesAsync();
      return _mapper.Map<Report, ReportOutDto>(report);
    }
    public async Task<bool> ReviewReport(string reportId, string userId)
    {
      var report = await _context.Reports.FirstOrDefaultAsync(report => report.ReportId == reportId);
      var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userId);
      if (user is null || report is null)
      {
        return false;
      }
      report.IsReviewed = true;
      report.ReviewedBy = user;
      await _context.SaveChangesAsync();
      return true;
    }
  }
}
