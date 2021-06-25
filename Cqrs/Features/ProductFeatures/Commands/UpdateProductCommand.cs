using Cqrs.Context;
using Cqrs.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public Product Product { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IApplicationContext _context;

            public UpdateProductCommandHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == command.Product.Id);
                if (product == null) return default;

                product.Barcode = command.Product.Barcode;
                product.Name = command.Product.Name;
                product.BuyingPrice = command.Product.BuyingPrice;
                product.Rate = command.Product.Rate;
                product.Description = command.Product.Description;

                await _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
