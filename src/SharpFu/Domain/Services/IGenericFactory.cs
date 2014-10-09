using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFu.Domain.Services
{

	/// <summary>
	///		Interface for generic factories
	/// </summary>
	/// <typeparam name="TProduct">Product type</typeparam>
	public interface IGenericFactory<out TProduct>
	{

		/// <summary>
		///		Creates a new instance of the product
		/// </summary>
		TProduct Create();

	}
}
