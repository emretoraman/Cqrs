using Cqrs.Context;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.Features.ProductFeatures.Commands
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
        {
            private readonly IApplicationContext _context;

            public DeleteProductCommandHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == command.Id);
                if (product == null) return default;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
