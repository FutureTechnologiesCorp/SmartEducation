using System;
using Microsoft.Extensions.DependencyInjection;
         
namespace Core.Common
{
    public class AmbientContext: IAmbientContext
    {
        IServiceProvider _serviceProvider;
        
        public AmbientContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TQuery ResolveQuery<TQuery>()
        {
            return _serviceProvider.GetService<TQuery>();
        }

		public TCommand ResolveCommand<TCommand>()
		{
			return _serviceProvider.GetService<TCommand>();
		}
    }
}
