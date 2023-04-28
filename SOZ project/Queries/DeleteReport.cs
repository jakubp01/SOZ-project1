using MediatR;
using SOZ_project.Controllers.Core;
using SOZ_project.Models;

namespace SOZ_project.Queries
{
    public class DeleteReport
    {
        public class Query : IRequest<Result<ReportModel>>
        {
            public int Id { get; set; }
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
                var response = await _context.Reports.FindAsync(request.Id);

                if (response != null)
                {
                    _context.Reports.Remove(response);
                    await _context.SaveChangesAsync();
                    return Result<ReportModel>.Success(response);

                }
                return Result<ReportModel>.Failure("error");

            }
        }
    }
}
