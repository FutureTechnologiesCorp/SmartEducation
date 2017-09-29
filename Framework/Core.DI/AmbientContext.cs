using System;
using Core.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DI
{
    public class AmbientContext: IAmbientContext
    {
        readonly IServiceProvider _serviceProvider;

        public AmbientContext(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

        public IUnitOfWork UnitOfWork => _serviceProvider.GetService<IUnitOfWork>();

        public TObject ResolveObject<TObject>()
        {
            return _serviceProvider.GetService<TObject>();
        }
    }
}
