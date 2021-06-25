using Cqrs.Context;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public decimal BuyingPrice { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IApplicationContext _context;

            public UpdateProductCommandHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == command.Id);
                if (product == null) return default;

                product.Name = command.Name;
                product.Barcode = command.Barcode;
                product.Description = command.Description;
                product.Rate = command.Rate;
                product.BuyingPrice = command.BuyingPrice;

                await _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
