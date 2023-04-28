using MediatR;
using SOZ_project.Controllers.Core;
using SOZ_project.Models;

namespace SOZ_project.Queries
{
    public class UpdateReport
    {
        public class Query : IRequest<Result<ReportModel>>
        {
            public ReportModel report;

        }

        public class Handler : IRequestHandler<Query, Result<ReportModel>>
        {
            private readonly ReportsDbContext _context;
            public Handler(ReportsDbContext context)
            {
                _context = context;
            }

            public async Task<Result<ReportModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.report.Gender == "0")
                {
                    request.report.Gender = "Kobieta";
                }
                else { request.report.Gender = "Mężczyzna"; }

                if (request.report.Status == "0")
                {
                    request.report.Status = "Nowe";
                }
                else if (request.report.Status == "1")
                {
                    request.report.Status = "Poszukiwania";
                }
                else { request.report.Status = "Zakończono"; }

                _context.Update(request.report);
                int rowsAffected = await _context.SaveChangesAsync();
                if (rowsAffected > 0)
                {
                    return Result<ReportModel>.Success(request.report);
                }
                else
                {
                    return Result<ReportModel>.Failure("No rows were affected in the database.");
                }

            }
        }
    }
}
