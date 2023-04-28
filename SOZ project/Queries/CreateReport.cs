using MediatR;
using SOZ_project.Controllers.Core;
using SOZ_project.Models;
using System.Composition;

namespace SOZ_project.Queries
{
    public class CreateReport
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

                    request.report.Status = "Nowe";
               
                _context.Add(request.report);
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
