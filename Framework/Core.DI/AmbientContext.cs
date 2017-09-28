using System;
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

        public TObject ResolveObject<TObject>()
        {
            return _serviceProvider.GetService<TObject>();
        }
    }
}
